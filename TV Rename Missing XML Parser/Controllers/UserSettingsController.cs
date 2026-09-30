using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TV_Rename_Missing_XML_Parser.Entities;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    public class UserSettingsController
    {
        private const string JSON_PATH = "Config/settings.json";
        private const string DEFAULT_JSON_PATH = "Data/settings.default.json";

        private GeneralSettings jsonSettings;
        private readonly LogController logController;

        public Dictionary<string, ShowSettings> ShowSettingsLookup
        {
            get
            {
                if (this.jsonSettings.Shows == null)
                    return new Dictionary<string, ShowSettings>();

                return this.jsonSettings.Shows.ToDictionary(s => s.Id, s => s);
            }
        }

        public List<TorrentSiteSettings> TorrentSites
        {
            get
            {
                return this.jsonSettings.TorrentSites;
            }
        }

        public string XmlFilePath
        {
            get
            {
                return jsonSettings.XmlFilePath;
            }
            set
            {
                jsonSettings.XmlFilePath = value;
            }
        }

        public int SearchDelay
        {
            get
            {
                return jsonSettings.SearchDelay;
            }
            set
            {
                jsonSettings.SearchDelay = value;
            }
        }

        public string BrowserPath
        {
            get
            {
                return jsonSettings.BrowserPath;
            }
            set
            {
                jsonSettings.BrowserPath = value;
            }
        }

        public string LastActivityTime
        {
            get
            {
                return jsonSettings.LastActivityTime;
            }
            set
            {
                jsonSettings.LastActivityTime = value;
            }
        }

        public int DefaultMaxSearchAge
        {
            get
            {
                return jsonSettings.DefaultMaxSearchAge;
            }
            set
            {
                jsonSettings.DefaultMaxSearchAge = value;
            }
        }

        public bool DefaultShowIgnore
        {
            get
            {
                return jsonSettings.DefaultShowIgnore;
            }
            set
            {
                jsonSettings.DefaultShowIgnore = value;
            }
        }

        public bool DefaultShowSpecials
        {
            get
            {
                return jsonSettings.DefaultShowSpecials;
            }
            set
            {
                jsonSettings.DefaultShowSpecials = value;
            }
        }

        public string DefaultSite
        {
            get
            {
                return jsonSettings.DefaultSite;
            }
            set
            {
                jsonSettings.DefaultSite = value;
            }
        }

        /// <summary>
        /// Initialize with logger for validation.
        /// </summary>
        public UserSettingsController(LogController logController)
        {
            if (logController == null)
                throw new ArgumentNullException(nameof(logController));
            this.logController = logController;
            this.Load();
        }

        //public Show AddOrUpdateTVShowById(Show showData)
        //{
        //    Show match = this.jsonSettings.Shows.First<Show>(s => s.Id == showData.Id);

        //    if (match != null)
        //    {
        //        match.updateAttributesIfNotNull(showData);
        //        return match;
        //    }

        //    this.jsonSettings.Shows.Add(showData);
        //    return showData;
        //}

        public void Load()
        {
            GeneralSettings defaults = this.GetDefaultSettingsFromFile();

            if (!File.Exists(JSON_PATH))
            {
                this.jsonSettings = defaults;
                this.Save();
                return;
            }

            string jsonText = File.ReadAllText(JSON_PATH);
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            GeneralSettings loaded = JsonSerializer.Deserialize<GeneralSettings>(jsonText, options);
            this.jsonSettings = loaded;

            bool settingsChanged = this.MergeWithDefaults();
            bool showsWillBeRemoved = this.jsonSettings.Shows?.Count != loaded.Shows?.Count;

            if (settingsChanged || showsWillBeRemoved)
            {
                this.BackupSettingsFileWithTimestamp();
                this.Save();
            }
        }

        /// <summary>
        /// Get the effective default site: match DefaultSite name, or first site if not found.
        /// </summary>
        public TorrentSiteSettings GetDefaultSite()
        {
            if (this.jsonSettings.TorrentSites == null || this.jsonSettings.TorrentSites.Count == 0)
                return null;

            if (!string.IsNullOrEmpty(this.jsonSettings.DefaultSite))
            {
                TorrentSiteSettings match = this.jsonSettings.TorrentSites.FirstOrDefault(s => s.Name == this.jsonSettings.DefaultSite);
                if (match != null)
                    return match;
            }

            return this.jsonSettings.TorrentSites.First();
        }

        /// <summary>
        /// Get a torrent site by name, or default site if not found.
        /// </summary>
        public TorrentSiteSettings GetSiteByName(string siteName)
        {
            if (string.IsNullOrEmpty(siteName))
                return this.GetDefaultSite();

            return this.jsonSettings.TorrentSites?.FirstOrDefault(s => s.Name == siteName) ?? this.GetDefaultSite();
        }

        /// <summary>
        /// Validate settings and log any issues found.
        /// </summary>
        public void Validate()
        {
            GeneralSettings defaults = this.GetDefaultSettingsFromFile();
            UserSettingsValidator validator = new UserSettingsValidator(this.jsonSettings, defaults, this.logController);
            validator.Validate();
        }

        private bool SchemaMatches(GeneralSettings original)
        {
            return (original.Shows != null && original.TorrentSites != null && original.SearchDelay > 0);
        }

        public void Reset()
        {
            GeneralSettings defaults = this.GetDefaultSettingsFromFile();
            this.BackupSettingsFileWithTimestamp();
            this.jsonSettings = defaults;
            this.Save();
        }

        private void BackupSettingsFileWithTimestamp()
        {
            if (!File.Exists(JSON_PATH))
                return;

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
            string backupPath = Path.Combine(
                Path.GetDirectoryName(JSON_PATH),
                $"settings.backup.{timestamp}.json"
            );
            File.Copy(JSON_PATH, backupPath);
        }

        /// <summary>
        /// Merge user settings with defaults: starts with all defaults, then overlays user values.
        /// This automatically adds new fields with defaults when app updates, and removes old fields
        /// that are no longer in defaults. Preserves all existing user settings.
        /// </summary>
        private bool MergeWithDefaults()
        {
            GeneralSettings defaults = this.GetDefaultSettingsFromFile();
            GeneralSettings merged = new GeneralSettings();
            bool changed = false;

            // Start with defaults for all properties
            foreach (var property in typeof(GeneralSettings).GetProperties())
            {
                object defaultValue = property.GetValue(defaults);
                property.SetValue(merged, defaultValue);
            }

            // Overlay user values: for each property, if user has explicitly set it, preserve their value
            // For booleans, only overlay if the field was present in the user's JSON
            foreach (var property in typeof(GeneralSettings).GetProperties())
            {
                object userValue = property.GetValue(this.jsonSettings);
                bool userHasValue = userValue != null &&
                                   (property.PropertyType != typeof(string) ||
                                    !string.IsNullOrEmpty((string)userValue));

                // For booleans, also check if the field was actually in the JSON file
                if (property.PropertyType == typeof(bool) && userHasValue)
                {
                    if (this.IsFieldMissingFromUserJson(property.Name))
                    {
                        userHasValue = false; // Field wasn't in JSON, use default
                    }
                }

                if (userHasValue)
                {
                    property.SetValue(merged, userValue);
                }
            }

            changed = this.DetectSettingsChanged(this.jsonSettings, merged);
            this.jsonSettings = merged;

            UserSettingsValidator validator = new UserSettingsValidator(this.jsonSettings, defaults, this.logController);

            if (!validator.IsValidSearchDelay(this.jsonSettings.SearchDelay))
            {
                this.jsonSettings.SearchDelay = defaults.SearchDelay;
                changed = true;
            }

            if (!validator.IsValidDefaultMaxSearchAge(this.jsonSettings.DefaultMaxSearchAge))
            {
                this.jsonSettings.DefaultMaxSearchAge = defaults.DefaultMaxSearchAge;
                changed = true;
            }

            if (this.jsonSettings.TorrentSites != null && this.jsonSettings.TorrentSites.Any(s => string.IsNullOrEmpty(s.Name)))
            {
                this.jsonSettings.TorrentSites = defaults.TorrentSites;
                changed = true;
            }

            if (this.jsonSettings.Shows == null)
            {
                this.jsonSettings.Shows = new List<ShowSettings>();
                changed = true;
            }

            if (validator.RemoveStaleShows())
            {
                changed = true;
            }

            return changed;
        }

        /// <summary>
        /// Detect if settings changed by checking for null/empty values in original user settings.
        /// If found, parse the actual JSON to see if the field was missing (new field added).
        /// </summary>
        private bool DetectSettingsChanged(GeneralSettings original, GeneralSettings merged)
        {
            // Check if any null/empty values exist in original
            foreach (var property in typeof(GeneralSettings).GetProperties())
            {
                object userValue = property.GetValue(original);
                bool isNullOrEmpty = userValue == null ||
                                    (property.PropertyType == typeof(string) && string.IsNullOrEmpty((string)userValue));

                if (isNullOrEmpty)
                {
                    // Found a null/empty value; check if it was missing from user's JSON
                    if (this.IsFieldMissingFromUserJson(property.Name))
                    {
                        return true; // Field was added
                    }
                }
            }

            // Check if any boolean fields were missing from user's JSON
            foreach (var property in typeof(GeneralSettings).GetProperties())
            {
                if (property.PropertyType == typeof(bool))
                {
                    if (this.IsFieldMissingFromUserJson(property.Name))
                    {
                        return true; // Boolean field was added
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a property exists in the user's JSON file.
        /// Returns true if the property is missing from the JSON (new field added).
        /// </summary>
        private bool IsFieldMissingFromUserJson(string propertyName)
        {
            if (!File.Exists(JSON_PATH))
                return false;

            try
            {
                string jsonText = File.ReadAllText(JSON_PATH);
                using (JsonDocument doc = JsonDocument.Parse(jsonText))
                {
                    JsonElement root = doc.RootElement;
                    return !root.TryGetProperty(propertyName, out _);
                }
            }
            catch
            {
                return false;
            }
        }

        public GeneralSettings GetDefaultSettingsFromFile()
        {
            string defaultJsonText = File.ReadAllText(DEFAULT_JSON_PATH);
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<GeneralSettings>(defaultJsonText, options);
        }

        public void ResetToDefaults()
        {
            this.jsonSettings = this.GetDefaultSettingsFromFile();
        }

        public void RestoreDefaultGeneralSettings()
        {
            this.jsonSettings.XmlFilePath = "";
        }

        public void RestoreDefaults()
        {
            this.RestoreDefaultGeneralSettings();
            this.RestoreDefaultTorrentSites();
            this.RestoreDefaultShows();
        }

        public void RestoreDefaultShows()
        {
            this.jsonSettings.Shows.Clear();
        }

        /// <summary>
        /// Add a show setting if it doesn't already exist.
        /// </summary>
        public void Add(ShowSettings showSettings)
        {
            if (showSettings == null)
                throw new ArgumentNullException(nameof(showSettings));

            if (this.jsonSettings.Shows == null)
            {
                this.jsonSettings.Shows = new List<ShowSettings>();
            }

            if (!this.jsonSettings.Shows.Any(s => s.Id == showSettings.Id))
            {
                this.jsonSettings.Shows.Add(showSettings);
            }
        }

        /// <summary>
        /// Remove a show setting.
        /// </summary>
        public void Remove(ShowSettings showSettings)
        {
            if (showSettings == null)
                throw new ArgumentNullException(nameof(showSettings));

            if (this.jsonSettings.Shows != null)
            {
                this.jsonSettings.Shows.RemoveAll(s => s.Id == showSettings.Id);
            }
        }

        public void RestoreDefaultTorrentSites()
        {
            GeneralSettings defaults = this.GetDefaultSettingsFromFile();
            this.jsonSettings.TorrentSites.Clear();
            foreach (TorrentSiteSettings site in defaults.TorrentSites)
            {
                this.jsonSettings.TorrentSites.Add(site);
            }
        }

        /// <summary>
        /// Save settings, keeping only shows with custom settings applied.
        /// </summary>
        public void Save()
        {
            GeneralSettings settingsToSave = new GeneralSettings
            {
                XmlFilePath = this.jsonSettings.XmlFilePath,
                TorrentSites = this.jsonSettings.TorrentSites,
                Shows = this.jsonSettings.Shows.Where(s => HasCustomSettings(s)).ToList(),
                SearchDelay = this.jsonSettings.SearchDelay,
                BrowserPath = this.jsonSettings.BrowserPath,
                LastActivityTime = this.jsonSettings.LastActivityTime,
                DefaultMaxSearchAge = this.jsonSettings.DefaultMaxSearchAge,
                DefaultSite = this.jsonSettings.DefaultSite,
                DefaultShowIgnore = this.jsonSettings.DefaultShowIgnore,
                DefaultShowSpecials = this.jsonSettings.DefaultShowSpecials,
                OmitNullsInJson = this.jsonSettings.OmitNullsInJson
            };

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            if (this.jsonSettings.OmitNullsInJson)
            {
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            }

            string jsonText = JsonSerializer.Serialize(settingsToSave, options);

            string configDir = Path.GetDirectoryName(JSON_PATH);
            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }

            File.WriteAllText(JSON_PATH, jsonText);
        }

        /// <summary>
        /// Check if a show has any custom settings applied.
        /// </summary>
        private bool HasCustomSettings(ShowSettings show)
        {
            return !string.IsNullOrEmpty(show.CleanTitle) ||
                   !string.IsNullOrEmpty(show.TorrentSiteName) ||
                   (show.Episodes != null && show.Episodes.Count > 0);
        }
    }
}
