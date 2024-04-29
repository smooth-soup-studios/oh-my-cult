using UnityEditor;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using System;

public static class UnityMenu {
	private static string _logname = "EditorTools";

    [MenuItem("Tools/Open Savedata folder ^PGDN")]
    private static void OpenSaveData() {
        OpenFileManager(Application.persistentDataPath);
    }

    private static void OpenFileManager(string folderPath) {
        folderPath = Path.GetFullPath(folderPath);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            // Windows
            Process.Start(new ProcessStartInfo {
                FileName = $"explorer",
                Arguments = $"\"{folderPath.Replace("/", "\\")}\"", // WHY THE FUCK DOES EVERYTHING IN WINDOWS RECOGNIZE BOTH SLASHES AS PATHS EXEPT THE ONE APPLICATION THAT SHOULD!@
                UseShellExecute = true
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
            // macOS
            Process.Start(new ProcessStartInfo {
                FileName = "open",
                Arguments = $"\"{folderPath}\""
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
            // Linux
            Process.Start(new ProcessStartInfo {
                FileName = "xdg-open",
                Arguments = $"\"{folderPath}\""
            });
        }
    }

    [MenuItem("Tools/Clear Savedata ^END")]
    private static void ClearSaveData() {
        DeleteSaveFiles(Application.persistentDataPath);
    }

    private static void DeleteSaveFiles(string directoryPath) {
        try {
            if (Directory.Exists(directoryPath)) {
                string[] dataFiles = Directory.GetFiles(directoryPath, "*.WDF", SearchOption.AllDirectories);

                foreach (string filePath in dataFiles) {
                    File.Delete(filePath);
                    Logger.Log(_logname, $"Deleted: {filePath}");
                }

                Logger.Log(_logname, "Deletion complete.");
            }
            else {
                Logger.LogError(_logname, $"Directory not found: {directoryPath}");
            }
        }
        catch (Exception ex) {
            Logger.LogError(_logname, $"An error occurred: {ex.Message}");
        }
    }
}