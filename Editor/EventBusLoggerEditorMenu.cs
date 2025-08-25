#if UNITY_EDITOR
using SimpleBus;
using UnityEditor;

[InitializeOnLoad]
public static class EventBusLoggerEditorMenu
{
    // Editor menu for toggling logging
    [MenuItem("SimpleBus/Toggle EventBus Logging %#l")]
    static void ToggleLoggingMenu()
    {
        EventBusLogger.SetLogging(!EventBusLogger.IsLoggingEnabled);
        Menu.SetChecked("SimpleBus/Toggle EventBus Logging %#l", EventBusLogger.IsLoggingEnabled);
    }

    [MenuItem("SimpleBus/Toggle EventBus Logging %#l", true)]
    static bool ToggleLoggingValidate()
    {
        Menu.SetChecked("SimpleBus/Toggle EventBus Logging %#l", EventBusLogger.IsLoggingEnabled);
        return true;
    }
}
#endif