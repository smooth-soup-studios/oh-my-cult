using System;
using UnityEngine;

[Serializable]
// Disable warnings about not overriding .Equals(). This is intentionally left out to allow for direct object comparison
#pragma warning disable CS0660
#pragma warning disable CS0661
public struct ItemStack {
	public InventoryItem Item;
	public int Amount;

	public ItemStack(InventoryItem item, int amount) {
		Item = item;
		Amount = amount;
	}
	public static bool operator ==(ItemStack lhs, ItemStack rhs) { return lhs.Item.InvData.Id == rhs.Item.InvData.Id; }
	public static bool operator !=(ItemStack lhs, ItemStack rhs) { return lhs.Item.InvData.Id == rhs.Item.InvData.Id; }

}

[Serializable]
// Companion class to ItemStack, used for serialization as ScriptableObjects are annoying to serialize directly.
public struct ItemDataStack {
	public InvData Data;
	public int Amount;

	public ItemDataStack(InvData data, int amount) {
		Data = data;
		Amount = amount;
	}
}

public static class StackExtentions {
	/// <summary>
	/// Converts an ItemStack to an easier serializable ItemDataStack
	/// </summary>
	/// <param name="stack"></param>
	/// <returns></returns>
	public static ItemDataStack ToSerializable(this ItemStack stack) {
		// Hacky conversion between "true null" and object marked as nulltype for use in serialization
		// itemtype should be checked in loadData and converted back to true null
		if (stack.Item == null) {
			InvData emptyItem = new() {
				ItemType = InventoryItemType.Null
			};
			return new ItemDataStack(emptyItem, stack.Amount);
		}
		return new ItemDataStack(stack.Item.InvData, stack.Amount);
	}

	/// <summary>
	/// Converts an ItemDataStack to a more usable ItemStack
	/// </summary>
	/// <param name="stack"></param>
	/// <returns></returns>
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