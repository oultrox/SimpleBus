using UnityEditor;
using UnityEngine;

namespace SimpleBus.Editor
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class EventBusLoggingToggle
    {
        private const string EditorPrefsKey = "SimpleBus_EnableLogging";

        // Current logging state
        public static bool EnableLogging
        {
            get
            {
#if UNITY_EDITOR
                return EditorPrefs.GetBool(EditorPrefsKey, true); // editor default
#else
                // runtime default if EditorPrefs not available
                return true;
#endif
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetBool(EditorPrefsKey, value);
#endif
            }
        }

#if UNITY_EDITOR
        [MenuItem("SimpleBus/Toggle EventBus Logging %#l")] // Ctrl/Cmd + Shift + L
        private static void ToggleLogging()
        {
            EnableLogging = !EnableLogging;
            Debug.Log($"[SimpleBus] EventBus logging is now {(EnableLogging ? "ENABLED" : "DISABLED")}");
        }

        [MenuItem("SimpleBus/Toggle EventBus Logging %#l", true)]
        private static bool ToggleLoggingValidate()
        {
            Menu.SetChecked("SimpleBus/Toggle EventBus Logging %#l", EnableLogging);
            return true;
        }
#endif
    }
}
