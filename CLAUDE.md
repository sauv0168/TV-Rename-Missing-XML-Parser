# TV Rename Missing XML Tool

## UI & Designer

- You handle all designer/form work in Visual Studio (forms, controls, layouts)
- Only write C# code for event handlers and logic; never modify .Designer.cs files
- Event handlers should be created in the Designer, then code filled in the code-behind

## Code Patterns

### Parameter Validation

Validate all method/constructor parameters at the top before any logic. Unless a parameter is clearly optional, require it and validate it once—don't check for null/validity repeatedly throughout the method:

```csharp
public AppSettingsForm(UserSettingsController userSettingsController, LogController logController)
{
    if (logController == null)
        throw new ArgumentNullException(nameof(logController));
    
    this.logController = logController;
    // Now use logController freely without null checks
}

private void ValidateBrowserPath()
{
    string browserPath = this.txtBrowserPath.Text.Trim();
    if (string.IsNullOrEmpty(browserPath))
        return;
    
    // No need to check this.logController != null—validated in constructor
    this.logController.Add("Warning message");
}
```

### Code Documentation

All methods must have a block comment summary:

```csharp
/// <summary>
/// Fires when a key is released and the character is added to the max age field.
/// </summary>
private void numMaxAge_KeyUp(object sender, KeyEventArgs e)
{
    this.controller.DoNumMaxAgeValueChanged(sender, e);
}
```

Not line comments (`//`). Block comments enable IntelliSense tooltips and maintain consistency.

### Variable Naming

Variable names must accurately reflect what they contain. Don't hide calculations in initialization if it makes the name misleading:

**Bad:**
```csharp
int daysSinceActivity = (int)(DateTime.Now - lastActivity).TotalDays + 1;
```

**Good:**
```csharp
int daysSinceActivity = (int)(DateTime.Now - lastActivity).TotalDays;
int maxSearchAge = daysSinceActivity + 1;
```

### Exception Handling

Use try-catch as a control structure, not defensive if-statements. Let exceptions do the validation work:

**Bad:**
```csharp
if (!string.IsNullOrEmpty(lastActivityTime) && DateTime.TryParse(lastActivityTime, out var lastActivity))
{
    // process
}
```

**Good:**
```csharp
try
{
    DateTime lastActivity = DateTime.Parse(lastActivityTime);
    // process
}
catch
{
    // handle failure
}
```

### Exception Handling

Use try-catch when the logic flow naturally fits a "try primary, fallback to default" pattern. This expresses intent more clearly than setting a default then conditionally overriding it:

**Less clear (default-then-override):**
```csharp
int maxSearchAge = 14; // default
if (!string.IsNullOrEmpty(lastActivityTime) && DateTime.TryParse(lastActivityTime, out var lastActivity))
{
    maxSearchAge = (int)(DateTime.Now - lastActivity).TotalDays + 1;
}
```

**Clearer (try-then-fallback):**
```csharp
try
{
    DateTime lastActivity = DateTime.Parse(lastActivityTime);
    int maxSearchAge = (int)(DateTime.Now - lastActivity).TotalDays + 1;
}
catch
{
    int maxSearchAge = 14; // default
}
```

Normal if-statements for control flow and logic remain standard.

### Reusable Logic

Extract repeated logic patterns to methods. Example: `ShowController.GetSearchTitle()` is used in both search filtering and copy-to-clipboard to get cleanTitle or fallback to title.

### Controllers

- **XMLController**: XML parsing and data transformation
- **UserSettingsController**: Settings persistence (JSON)
- **ShowController**: Show operations (add, sort, search title logic)
- **MainController**: Orchestrates workflow, event handlers
- **WebController**: URL/browser opening
- **TreeViewController**: TreeView display and filtering
- **LogController**: Logging/warnings at startup

## Configuration

All configuration belongs in `settings.default.json`. Never hardcode values. Use the following pattern:
1. Add property to `JsonSettings` class
2. Add accessor property to `UserSettingsController`
3. Add default value to `settings.default.json`
