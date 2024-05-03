using System;
using UnityEngine;

[Serializable]
public class GameData {
	public PlayerData PlayerData = new();
}


[Serializable]
public class PlayerData {
	public Vector3 PlayerPosition = Vector3.zero;
	public string SceneName = "";
	public bool BossDefeated = false;

}
