# TV Rename Missing XML Tool

A Windows desktop application for managing TV shows with missing episodes. Parse XML files from TV Rename, view episodes in a searchable TreeView, and manage show metadata.

## Features

- Parse TV Rename XML format files from local or network paths
- Alphabetically sorted show and episode display
- Search by show title (uses "Clean Title" if configured) and filter by episode age
- Smart search defaults based on last activity time (shows episodes from last activity day minus 1)
- Show all episodes for a show if any episode is within the search age filter
- Manage show settings and metadata (IMDB ID, Clean Title for search)
- Direct integration with torrent sites (1337x)
- Copy show/episode information via Ctrl+C
- Custom browser support (configure browser path in settings)
- Activity tracking (records when shows are copied or opened in browser)
- Persistent settings in JSON format with validation on startup

## Requirements

- Windows 7 or later
- .NET Framework 4.8.1

## Setup

1. Clone or download the project
2. Open `tv-rename-missing-xml-tool.sln` in Visual Studio
3. Build the solution (Release build automatically creates a zip package)
4. Run `TV Rename Missing XML Parser.exe`

## Building a Release

When you build in Release mode, a Post-Build Event automatically creates a release package:
- Location: `Release/tv-rename-missing-xml-tool.zip`
- Contains: executable, DLLs, and default settings file
- Ready to distribute or run on another machine

## Usage

1. **Load XML file**: Click "Load XML" to browse for your TV Rename `missing.xml` file (supports network paths)
2. **View shows**: Shows appear alphabetically in the main TreeView
3. **Filter episodes**: 
   - Use the search box to filter shows by title (or by Clean Title if configured)
   - Adjust max age to filter episodes; set to 0 to show all episodes for any show with recent missing episodes
4. **Manage shows**: Press SHIFT on a show to open settings (edit IMDB ID, set Clean Title for search)
5. **Copy info**: Press Ctrl+C on any show or episode to copy to clipboard
6. **Search torrent site**: Press ALT on any show or episode to open torrent site search in your configured browser
7. **Smart defaults**: First launch uses default max age (14 days); after using the app, new sessions default to "last activity time + 1 day" to show recently missing episodes
8. **Help**: Click the Help button to view keyboard shortcuts and control reference

## Configuration

Settings are stored in `settings.json`. The file only shows fields with values when `omitNullsInJson` is enabled, making it easy to hand-edit like a traditional config file.

### General Settings

- `xmlFilePath`: Path to your TV Rename XML file (local or network path)
- `torrentSites`: List of torrent search sites with URL patterns
- `searchDelay`: Debounce delay in milliseconds for search filtering (default: 300ms)
- `browserPath`: Optional custom browser executable path (leave empty to use system default)
- `defaultMaxSearchAge`: Default episode age filter in days (default: 14)
- `omitNullsInJson`: When true, only non-null fields appear in the JSON file (default: false)

### Activity Tracking

- `lastActivityTime`: Timestamp of last copy/browser action (auto-tracked, ISO 8601 format). Used to automatically set search age on startup to "last activity + 1 day"

### Shows and Settings

Settings are stored in `settings.json` under the `shows` array. Only shows with custom settings are persisted.

Properties marked with `@save_trigger` in the code (indicated by `/// <save_trigger/>` XML tag) will cause a show to be saved:
- `cleanTitle`: Alternative title used for search filtering (triggers save if set)
- `torrentSiteName`: Custom torrent site name for this show (triggers save if set)
- `episodes`: Custom episode settings like "ignore" flags (triggers save if any exist)

If a show has none of these custom settings, it won't appear in `settings.json`.

**Example minimal shows entry with OmitNullsInJson enabled:**
```json
{
  "id": "396390",
  "cleanTitle": "Yellowstone Prequel"
}
```

**Default values** are defined in `settings.default.json`. If a setting is missing or invalid in `settings.json`, the app loads the default from this file. Never hardcode configuration values in code; they belong in `settings.default.json`.
