using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new InventoryItem", menuName = "OhMyCult/Items/new InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject {
	public InvData InvData;
}

[Serializable]
public class InvData {
	[ScriptableObjectId]
	public string Id;
	[Header("Inventroy information")]
	public string ItemName;
	public Sprite ItemIcon;
	public GameObject ItemPrefab;
	public int MaxStackSize = 1;
	public InventoryItemType ItemType;
	public string AnimationSet;

	[TextArea]
	public string Description;
}


[Serializable]
public enum InventoryItemType {
	Consumable,
	Weapon,
	Null, // Here because I need a way to check if synced items are empty and can't use nulls becouse they fuck with the save system
}