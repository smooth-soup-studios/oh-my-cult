using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new InventoryItem", menuName = "OhMyCult/Items/new InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject {
	public InvData InvData;
}

[Serializable]
public class InvData {
	[Header("Inventroy information")]
	public string ItemName;
	public Sprite ItemIcon;
	public GameObject ItemPrefab;
	public InventoryItemType ItemType;
	public string AnimationSet;

	[TextArea]
	public string Description;
}


[Serializable]
public enum InventoryItemType {
	Consumable,
	Weapon,
}