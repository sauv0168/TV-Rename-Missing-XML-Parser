## Current Tasks

- [x] Fix TV shows sorting — shows now appear alphabetically in TreeView
- [x] Migrate from fastJSON to System.Text.Json
- [x] Remove unused Newtonsoft.Json dependency
- [x] Test sorting on app load — fixed sorting on initial load
- [x] Fix circular JSON serialization (Episode.Show marked with JsonIgnore)
- [x] Support loading from network paths
- [x] Fix backup logic to only backup on schema changes
- [x] Add CleanTitle for show search/display
- [x] Fix Ctrl+C to work on show names
- [x] Add custom browser path support
- [x] Add activity tracking (LastActivityTime)
- [x] Use LastActivityTime + 1 day as default search age
- [x] Show all episodes if any are recent (based on search age)
- [x] Add LogController for startup validation warnings
- [x] Parameter validation in TreeViewController.AddEpisodeNodes
- [x] Refactor JSON schema (ShowSettings separate from Show entities)
- [x] Add OmitNullsInJson setting for clean config files
- [x] Help button with keyboard shortcuts guide
- [x] Application icon support
- [x] Automated release build script (zip exe/dlls/config)
- [x] Comprehensive markdown documentation
- [x] Add browser path validation to AppSettingsForm
- [x] Implement Restore Defaults button
- [x] Implement Cancel Changes button  
- [x] Fix log button to open LogForm
- [x] Fix JSON case-insensitive deserialization
- [x] Fix max age search to trigger on keystroke with delay timer
- [x] Add code documentation standards (block comments for methods)
- [x] Refactor InitMaxSearchAge to use try-catch pattern
- [x] Episode-level ignore toggle (D key) with JSON persistence
- [x] Auto-cleanup of stale shows and episodes on startup
- [x] JSON property reordering for readability (general settings first, Shows last)
- [x] Improved backup strategy with timestamp naming (settings.YYYY-MM-DD_HHMMSS.json)
- [ ] Refactor controller responsibilities — LogController mixing UI, SearchController state unclear, MainController orchestrating too much

## Blockers / Questions

- **Known Issue: TreeView selection bug with specific shows** — When ignoring episodes from shows in the XML, the selection sometimes jumps to a specific show (e.g., show ID 393159 "Dark Matter 2024") instead of staying nearby. The ignore functionality itself works perfectly (episodes are removed, saved, and can be un-ignored); only the post-removal selection is affected. Does not occur if that show is removed from XML. Needs investigation into TreeView structure or node reference handling.
