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

## Major Deliverables & Milestones

| Deliverable | Status | Description |
|---|---|---|
| Core XML Parser | ✓ Complete | Parse TV Rename XML output files and extract episode data |
| TreeView Display | ✓ Complete | Alphabetically sorted, hierarchical display of shows and episodes |
| Settings Management | ✓ Complete | Persist user preferences and show configuration to JSON |
| Search & Filter | ✓ Complete | Filter shows by title (with optional Clean Title) and episodes by age |
| Torrent Integration | ✓ Complete | Launch torrent site searches in configured browser directly from the app |
| Show All Episodes Logic | ✓ Complete | If any episode within search age, show all episodes for that show |
| Activity Tracking | ✓ Complete | Track last usage time and auto-set search defaults based on it |
| Custom Browser Support | ✓ Complete | Allow users to specify custom browser path with fallback to system default |
| Network Path Support | ✓ Complete | Load XML files from local or network paths |
| Episode-Level Ignore | ✓ Complete | Toggle ignore flag on episodes (D key); ignored episodes are removed from display and persisted to JSON |

## Project Scope

### Objectives

#### General
- ✓ Parse TV Rename XML format files (including network paths)
- ✓ Display shows and episodes in hierarchical TreeView (alphabetically sorted)
- ✓ Allow add/remove/edit of show information (IMDB ID, Clean Title)
- ✓ Persist settings to JSON file with smart backup on schema changes
- ✓ Support custom browser configuration with fallback to system default
- ✓ Track activity and auto-set search defaults

#### UI/UX
- ✓ Keyboard shortcuts (SHIFT for show settings, ALT for browser, CTRL+C for copy)
- ✓ Search filtering by show title (with optional Clean Title) and episode age
- ✓ Simple, intuitive form layouts and show settings dialog
- ✓ Smart episode filtering (show all episodes if any are recent)
- ✓ Validation warnings on startup (e.g., invalid browser path)

#### Technical
- ✓ .NET Framework 4.8.1 with WinForms
- ✓ System.Text.Json for settings serialization
- ✓ System.Xml.Linq for XML parsing
- ✓ Proper parameter validation and error handling

### Out of Scope

- Automatic episode download
- Database backend (JSON file only)
- Web interface
- Background search threading (known: UI thread blocks during generateResults, causing maxage control to freeze while typing; future enhancement would move search to background thread)

## Resources

Solo developer project. Windows 11 development environment.
