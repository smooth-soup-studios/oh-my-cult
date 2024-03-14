using System.IO;
using Data;
using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

namespace Managers {
public class DataManager : MonoBehaviour {
	public static DataManager Instance;
	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
		InitMessagePackResolver();
	}

	void InitMessagePackResolver() {
		MessagePackSerializer.DefaultOptions = MessagePackSerializer.DefaultOptions.WithResolver(GeneratedResolver.Instance);
	}

	void SaveGame(GameData data) {
		var bytes = MessagePackSerializer.Serialize(data);
		var path = Application.persistentDataPath + "/game-data.bin";
		File.WriteAllBytes(path, bytes);
		Debug.Log($"Data saved to: {path}");
	}

	GameData LoadGame() {
		var path = Application.persistentDataPath + "/game-data.bin";
		if (!File.Exists(path)) {
			Debug.LogError("Save file not found.");
			return null;
		}

		var bytes = File.ReadAllBytes(path);
		var loadedGameData = MessagePackSerializer.Deserialize<GameData>(bytes);
		Debug.Log($"Data loaded from: {path}");
		return loadedGameData;
	}
}
}