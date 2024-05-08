﻿using System;
using UnityEngine;

[Serializable]
public class GameData {
	public PlayerData PlayerData = new();
	public SceneData SceneData = new();
	public ActorData ActorData = new();
}


[Serializable]
public class PlayerData {
	public Vector3 PlayerPosition = Vector3.zero;
	public string SceneName = "";
	public bool BossDefeated = false;
	public int LatestDoor = -1;
	public bool HasDoorKey = false;
	public Dict<string, InvData> InvItemVals = new();

}

[Serializable]
public class SceneData {
	public Dict<string, bool> ArbitraryTriggers = new();
	public Dict<string, bool> InteractionData = new();
}

[Serializable]
public class ActorData {
	public Dict<string, float> HealthValues = new();
	public Dict<string, Vector3> PositionValues = new();
}
