## Current Tasks

- Refactor controller responsibilities — LogController mixing UI, SearchController state unclear, MainController orchestrating too much
- Fix search to check both Clean Title and original Title (currently checks only one or the other)

## Blockers / Known Issues

- **Log form appears before main form** — When startup validation errors are found, LogForm pops up as modal before MainForm is fully shown, causing visual confusion. Should defer log display until MainForm is visible.

- **TreeView selection bug with specific shows** — When ignoring episodes, the selection sometimes jumps to a specific show (e.g., show ID 393159 "Dark Matter 2024") instead of staying nearby. The ignore functionality itself works perfectly (episodes are removed, saved, and can be un-ignored); only the post-removal selection is affected. Does not occur if that show is removed from XML. Needs investigation into TreeView structure or node reference handling.

## Next Priorities

- Auto-update cleanTitle on load: check if title changed; if so, generate new cleanTitle by stripping special chars (e.g., "(2024)" from "Show Name (2024)"). If new cleanTitle differs from current, prompt user to choose between old and new cleanTitle.
- Add shortcut key combo for copy with/without episode name (maybe Ctrl+Shift+C vs Ctrl+C, or separate keys)
- Refactor keyboard shortcuts: create KeyboardController to centralize key bindings (separate TODO, not urgent)
- See CHARTER.md for feature completion status
- All completed features marked with ✓ in CHARTER.md
