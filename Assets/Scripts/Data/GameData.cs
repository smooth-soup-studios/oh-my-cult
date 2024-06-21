using System;
using UnityEngine;

[Serializable]
public class GameData {
	public PlayerData PlayerData = new();
	public SceneData SceneData = new();
	public ActorData ActorData = new();
	public PlayerSettings PlayerSettings = new();
}


[Serializable]
public class PlayerData {
	public Vector3 PlayerPosition = Vector3.zero;
	public string SceneName = "";
	public bool BossDefeated = false;
	public int LatestDoor = -1;
	public bool HasDoorKey = false;
	public bool HasTorch = false;
	public Dict<string, InvData> InvItemVals = new();
	public SerializableList<ItemDataStack> Inventory = new();
	public int SelectedInvSlot = 0;
	public float Health = 100;
	public bool IsInvulnerable = false;
}

[Serializable]
public class SceneData {
	public Dict<string, bool> ArbitraryTriggers = new();
	public Dict<string, bool> InteractionData = new();
	public Dict<string, ItemDataStack> InteractionItems = new();

}

[Serializable]
public class ActorData {
	public Dict<string, float> HealthValues = new();
	public Dict<string, Vector3> PositionValues = new();
	public Dict<string, bool> Arbitraryvalues = new();

}


[Serializable]
public class PlayerSettings {
	public Dict<string, float> VolumeValues = new();
	public int QualityIndex = -1;
}
