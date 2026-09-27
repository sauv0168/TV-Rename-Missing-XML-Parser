# Project Charter

## Overview

TV Rename Missing XML Tool is a Windows desktop application that helps users manage TV shows with missing episodes by parsing XML files and displaying them in an organized, searchable interface. Users can track missing episodes, manage show metadata, and integrate with torrent sites for easy discovery.

## Mission

Provide a simple, efficient tool for TV show enthusiasts to identify and track missing episodes across their collection.

## Business Objectives

- Enable quick identification of missing episodes
- Reduce manual work in tracking show metadata
- Provide searchable, sortable interface for episode discovery
- Support integration with popular torrent sites

## Project Scope

### User Interface Pages

#### Main Window
- Load XML Button
  - Opens file picker and loads new XML file
- Reload XML Button
  - Refreshes tree from current XML file
- TreeView Display
  - Shows displayed alphabetically by title
  - Each show expandable to reveal episodes
  - Episode display format: "S##E## Name (N days)" example: "S01E05 Pilot (3)"
  - Displays message "Load XML file to view results" when empty
- Search Textbox
  - Searches using show title
  - If Clean Title is set, searches both Clean Title and original Title
  - Case-insensitive matching
  - Empty search shows all shows
- Age Textbox
  - Numeric input for maximum episode age in days
  - Up/Down spinner buttons to increment/decrement
  - Empty or 0 shows all episodes
  - If any episode within age range, shows all episodes for that show
- Ignored Button
  - Text shows "Show Ignored" when ignored episodes are hidden
  - Text shows "Hide Ignored" when ignored episodes are shown
  - Toggles visibility of ignored episodes
- Specials Button
  - Text shows "Show Specials" when special episodes are hidden
  - Text shows "Hide Specials" when special episodes are shown
  - Toggles visibility of special episodes (S00)
- Log Button
  - Displays startup warnings and diagnostics
- Help Button
  - Displays keyboard shortcuts guide

#### Episode Ignore Feature
- Toggle via D key on selected episode in TreeView
- Ignored episodes removed from tree display
- Show Ignored filter reveals ignored episodes marked with "*"
- Ignore flag persisted to JSON with episode season/number
- Un-ignoring removes episode from JSON if no other show settings

#### Show Settings Form
- Clean Title textbox: custom display name override
- Torrent Site dropdown: per-show site selection
- Changes persist to JSON when form closes

#### Log Form
- Displays validation warnings from startup
- Displays diagnostic messages
- Scrollable list of entries

#### Keyboard Shortcuts
- SHIFT: Open Show Settings for selected show
- ALT: Launch torrent site search for selected item
- D: Toggle ignore flag on selected episode
- CTRL+C: Copy show/episode text to clipboard

### Core Features

#### General
- Parse TV Rename XML format files (including network paths)
- Display shows and episodes in hierarchical TreeView
- Allow add/remove/edit of show information (IMDB ID, Clean Title)
- Persist settings to JSON file with smart backup on schema changes
- Support custom browser configuration with fallback to system default
- Track activity and auto-set search defaults

#### Technical
- .NET Framework 4.8.1 with WinForms
- System.Text.Json for settings serialization
- System.Xml.Linq for XML parsing
- Proper parameter validation and error handling

### Out of Scope

- Automatic episode download
- Database backend (JSON file only)
- Web interface
- Background search threading (known: UI thread blocks during generateResults, causing maxage control to freeze while typing; future enhancement would move search to background thread)

## Resources

Solo developer project. Windows 11 development environment.
