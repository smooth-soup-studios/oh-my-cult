using System;
using System.IO;
using System.Collections.Generic;

using UnityEngine;

public class FileDataManager : IDataManager {
	private static string _logname = "FileDataManager";
	private readonly static string _superSecretKey = "Are_We_Human?-0r_aRe-W3-d4Nc3R"; // Should probably generate a key somehow but I am way too lazy to do this

	private string _saveDirectory;
	private string _fileName;
	private bool _encrypt = false;
	private bool _enableLogging;


	public FileDataManager(string saveDirectory, string fileName, bool encrypt = true) {
		_saveDirectory = saveDirectory;
		_fileName = fileName;
		_encrypt = encrypt;
	}

	public FileDataManager(string saveDirectory, string fileName, bool encrypt, bool enableLogging) : this(saveDirectory, fileName, encrypt) {
		_enableLogging = enableLogging;
	}



	public GameData Load(string profileId) {

		string savePath = Path.Combine(_saveDirectory, profileId, _fileName);
		try {
			if (File.Exists(savePath)) {
				SendToLogger($"Save file found at {savePath}");
				using FileStream stream = new(savePath, FileMode.Open);
				using StreamReader reader = new(stream);
				string json = reader.ReadToEnd();
				if (_encrypt) {
					json = Scramble(json); // Uncramble
				}
				return JsonUtility.FromJson<GameData>(json);
			}
			else {
				SendToLogger($"No save file found at {savePath}");
			}
		}
		catch (Exception e) {
			Logger.LogError(_logname, $"Error loading from {savePath}!\n{e.Message}");
		}

		return null;
	}

	public void Save(GameData data, string profileId) {

		string savePath = Path.Combine(_saveDirectory, profileId, _fileName);

		try {
			Directory.CreateDirectory(Path.GetDirectoryName(savePath));

			string json;

			if (_encrypt) {
				json = Scramble(JsonUtility.ToJson(data, false)); // Scramble the data to make editing saves more difficult
			}
			else {
				json = JsonUtility.ToJson(data, true);
			}

			using FileStream stream = new(savePath, FileMode.Create);
			using StreamWriter writer = new(stream);
			writer.Write(json);
			SendToLogger($"Saved data to: {savePath}");
		}
		catch (Exception e) {
			Logger.LogError(_logname, $"Error saving to {savePath}!\n{e.Message}");
		}
	}

	private string Scramble(string data) {
		string result = "";
		for (int i = 0; i < data.Length; i++) {
			result += (char)(data[i] ^ _superSecretKey[i % _superSecretKey.Length]);
		}
		return result;
	}


	// <ProfileId, profileData>
	public Dictionary<string, GameData> LoadAllSaveSlots() {
		Dictionary<string, GameData> saveSlotDict = new();

		IEnumerable<DirectoryInfo> dirInfoList = new DirectoryInfo(_saveDirectory).EnumerateDirectories();
		foreach (DirectoryInfo dirInfo in dirInfoList) {
			string profileId = dirInfo.Name;
			string fullPath = Path.Combine(_saveDirectory, profileId, _fileName);
			if (!File.Exists(fullPath)) {
				SendToLogger($"Directory {profileId} skipped becouse it contains no savedata");
				continue;
			}
			GameData profileData = Load(profileId);

			if (profileData != null) {
				saveSlotDict.Add(profileId, profileData);
			}
			else {
				Logger.LogError(_logname, $"Couldn't load profile {profileId}! The save might be corrupted");
			}
		}


		return saveSlotDict;
	}

	private void SendToLogger(string text) {
		if (_enableLogging) {
			Logger.Log(_logname, text);
		}
	}
}