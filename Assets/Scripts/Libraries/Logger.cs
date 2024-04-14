using UnityEngine;

public static class Logger {
    public static void Log(string name, object message) {
		#if UNITY_EDITOR
			Debug.Log($"<color=silver>[  <color=lime>{name}</color>  ]: " + message + "</color>");
		#else
			Debug.Log($"[{name}]: {message}");
		#endif
    }

    public static void LogWarning(string name, object message) {
		#if UNITY_EDITOR
			Debug.Log($"<color=silver>[  <color=orange>{name}</color>  ]: " + message + "</color>");
		#else
			Debug.Log($"[{name}]: {message}");
		#endif
    }

    public static void LogError(string name, object message) {
		#if UNITY_EDITOR
			Debug.LogError($"<color=silver>[  <color=red>{name}</color>  ]: " + message + "</color>");
		#else
			Debug.LogError($"[{name}]: {message}");
		#endif
    }
}