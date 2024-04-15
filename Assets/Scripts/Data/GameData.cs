using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData {
	public PlayerData PlayerData = new();
	public SceneData SceneData = new();
}


[Serializable]
public class PlayerData {
	public Vector3 PlayerPosition = Vector3.zero;
	public string SceneName = "";
	public bool BossDefeated = false;
	public int LatestDoor = -1;
	public bool HasDoorKey = false;

}

[Serializable]
public class SceneData {
	public Dict<string, bool> ArbitraryTriggers = new();
	public Dict<string, float> HealthValues = new();


}
