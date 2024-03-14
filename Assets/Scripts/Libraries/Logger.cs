using UnityEngine;

public static class Logger {
    public static void Log(string name, string message) {
        Debug.Log($"<color=silver>[  <color=lime>{name}</color>  ]: " + message + "</color>");
    }

    public static void LogWarning(string name, string message) {
        Debug.Log($"<color=silver>[  <color=orange>{name}</color>  ]: " + message + "</color>");
    }

    public static void LogError(string name, string message) {
        Debug.Log($"<color=silver>[  <color=red>{name}</color>  ]: " + message + "</color>");
    }
}