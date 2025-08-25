using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SimpleBus
{
    /// <summary>
    /// Runtime-friendly EventBus logging toggle with optional editor persistence.
    /// Can be changed in code or via the editor menu.
    /// </summary>
    public static class EventBusLogger
    {
        const string EditorPrefsKey = "SimpleBus_EnableLogging";

        // Static backing field, default value set to False.
        static bool _enableLogging = false;
        static bool _initialized = false;

        /// <summary>
        /// Returns whether EventBus logging is enabled.
        /// Use SetLogging(bool) to change the value.
        /// </summary>
        public static bool IsLoggingEnabled
        {
            get
            {
                InitializeIfNeeded();
                return _enableLogging;
            }
        }

        /// <summary>
        /// Change the logging state.
        /// This is the single public method for toggling logging.
        /// </summary>
        public static void SetLogging(bool enabled)
        {
            _enableLogging = enabled;
#if UNITY_EDITOR
            EditorPrefs.SetBool(EditorPrefsKey, enabled);
#endif
            Debug.Log($"[SimpleBus] EventBus logging {(enabled ? "ENABLED" : "DISABLED")}");
        }

        static void InitializeIfNeeded()
        {
            if (_initialized) return;
#if UNITY_EDITOR
            // Load last editor preference
            _enableLogging = EditorPrefs.GetBool(EditorPrefsKey, _enableLogging);
#endif
            _initialized = true;
        }
        
    }
}
