using System;
using UnityEngine;

[Serializable]
public struct ItemStack {
	public InventoryItem Item;
	public int Amount;

	public ItemStack(InventoryItem item, int amount) {
		Item = item;
		Amount = amount;
	}
}

[Serializable]
public struct ItemDataStack {
	public InvData Data;
	public int Amount;

	public ItemDataStack(InvData data, int amount) {
		Data = data;
		Amount = amount;
	}
}

public static class StackExtentions {
	public static ItemDataStack ToSerializable(this ItemStack stack) {
		if (stack.Item == null) {
			InvData emptyItem = new() {
				ItemType = InventoryItemType.Null
			};

			return new ItemDataStack(emptyItem, stack.Amount);
		}
		return new ItemDataStack(stack.Item.InvData, stack.Amount);
	}

	public static ItemStack ToRegular(this ItemDataStack stack) {
		InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
		if (stack.Data.ItemType == InventoryItemType.Null) {
			newItem = null;
		}
		else {
			newItem.name = stack.Data.ItemName;
			newItem.InvData = stack.Data;
		}
		return new ItemStack(newItem, stack.Amount);
	}
}