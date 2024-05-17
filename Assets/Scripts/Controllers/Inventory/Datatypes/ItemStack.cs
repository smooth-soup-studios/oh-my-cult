using System;

[Serializable]
public struct ItemStack {
	public InventoryItem Item;
	public int Amount;

	public ItemStack(InventoryItem item, int amount) {
		Item = item;
		Amount = amount;
	}
}