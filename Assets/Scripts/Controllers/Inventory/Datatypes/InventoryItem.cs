using UnityEngine;

[CreateAssetMenu(fileName = "new InventoryItem", menuName = "OhMyCult/Items/new InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject {
	[Header("Inventroy information")]
	public string ItemName;
	public Sprite ItemIcon;
	public GameObject ItemPrefab;
	public InventoryItemType ItemType;
	public string AnimationSet;

	[TextArea]
	public string Description;
}


public enum InventoryItemType {
	Consumable,
	Weapon,
}