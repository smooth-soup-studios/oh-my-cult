using UnityEditor;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using System;

public class UnityMenu {
    [MenuItem("Tools/Open Savedata folder")]
    private static void OpenSaveData() {
        OpenFileManager(Application.persistentDataPath);
    }

    private static void OpenFileManager(string folderPath) {
        folderPath = Path.GetFullPath(folderPath);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {

            // Windows
            Process.Start(new ProcessStartInfo {
                FileName = $"explorer",
                Arguments = $"{folderPath.Replace("/", "\\")}", // WHY THE FUCK DOES EVERYTHING IN WINDOWS RECOGNIZE BOTH SLASHES AS PATHS EXEPT THE ONE APPLICATION THAT SHOULD!@
                UseShellExecute = true
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
            // macOS
            Process.Start(new ProcessStartInfo {
                FileName = "open",
                Arguments = folderPath
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
            // Linux
            Process.Start(new ProcessStartInfo {
                FileName = "xdg-open",
                Arguments = folderPath
            });
        }
    }

    [MenuItem("Tools/Clear Savedata")]
    private static void ClearSaveData() {
        DeleteSaveFiles(Application.persistentDataPath);
    }

    private static void DeleteSaveFiles(string directoryPath) {
        try {
            if (Directory.Exists(directoryPath)) {
                string[] wdfFiles = Directory.GetFiles(directoryPath, "*.bin");

                foreach (string filePath in wdfFiles) {
                    File.Delete(filePath);
                    Logger.Log("EditorTools", $"Deleted: {filePath}");
                }

                Logger.Log("EditorTools", "Deletion complete.");
            }
            else {
                Logger.LogError("EditorTools", $"Directory not found: {directoryPath}");
            }
        }
        catch (Exception ex) {
            Logger.LogError("EditorTools", $"An error occurred: {ex.Message}");
        }
    }
}