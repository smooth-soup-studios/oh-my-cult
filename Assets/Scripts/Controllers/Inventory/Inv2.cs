using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inv2 : MonoBehaviour {
	private string _logname = "InventorySystem";

	private List<ItemStack> _currentInventory = new();
	private int _maxInventorySize = 5;
	private int _selectedItemIndex = 0;


	private void Awake() {
		// Need to initialize the list with the max size. Should be overwritten by LoadData if we ever add expanding inventory space.
		_currentInventory = new(new ItemStack[_maxInventorySize]);
		EventBus.Instance.Subscribe<int>(EventType.HOTBAR_SELECT, SelectSlot);
		EventBus.Instance.Subscribe<int>(EventType.HOTBAR_SWITCH, e => {
			if (e > 0) {
				SelectNextSlot();
			}
			else if (e < 0) {
				SelectPrevSlot();
			}
		});
		EventBus.Instance.Subscribe<InventoryItem>(EventType.INV_ADD, TempAdd);
		EventBus.Instance.Subscribe<InventoryItem>(EventType.INV_REMOVE, TempRemove);
	}

	public void TempAdd(InventoryItem item) {
		AddItem(item);
	}

	public void TempRemove(InventoryItem item) {
		RemoveItem(item);
	}



	public InventoryItem AddItem(InventoryItem item) {
		return AddItem(new ItemStack(item, 1)).Item;
	}

	public ItemStack AddItem(InventoryItem item, int amount) {
		return AddItem(new ItemStack(item, amount));
	}

	public ItemStack AddItem(ItemStack stack) {
		ItemStack returnStack;
		// Check if item already exists in inventory
		if (_currentInventory.Any(e => e.Item == stack.Item && stack.Item != null)) {
			ItemStack existingStack = GetStackOf(stack.Item);
			existingStack.Amount += stack.Amount;
			_currentInventory[_currentInventory.IndexOf(GetStackOf(stack.Item))] = existingStack;
			returnStack = new ItemStack(null, 0);
		}
		// Check if there is an empty slot in the inventory
		else if (_currentInventory.Any(e => e.Amount == 0) && stack.Item != null) {
			_currentInventory[_currentInventory.FindIndex(x => x.Amount == 0)] = stack;
			returnStack = new ItemStack(null, 0);
		}
		//overwrite the item if enabled
		else {
			ItemStack oldStack = _currentInventory[_selectedItemIndex];
			_currentInventory[_selectedItemIndex] = stack;
			returnStack = oldStack;
		}
		CleanInventory();
		return returnStack;
	}




	public void RemoveItem(InventoryItem item) {
		RemoveItem(new ItemStack(item, 1));
		CleanInventory();
	}

	public void RemoveItem(InventoryItem item, int amount) {
		RemoveItem(new ItemStack(item, amount));
	}

	public void RemoveItem(ItemStack stack) {
		// find the stack containing the item
		ItemStack existingStack = GetStackOf(stack.Item);
		// update item value
		existingStack.Amount -= stack.Amount;
		// Load item back into the inventory
		_currentInventory[_currentInventory.IndexOf(GetStackOf(stack.Item))] = existingStack;

		// if the stack is empty, remove the item (CleanInv probably takes care of it but just to be sure)
		if (existingStack.Amount <= 0) {
			RemoveItemByIndex(_currentInventory.IndexOf(GetStackOf(stack.Item)));
		}
		CleanInventory();
	}

	public void RemoveItemByIndex(int index) {
		if (index >= 0 && index < _maxInventorySize && _currentInventory.Count > index) {
			_currentInventory[index] = new(null, 0);
		}
		CleanInventory();
	}



	// Slot Selection
	public void SelectNextSlot() {
		if (_selectedItemIndex + 1 >= _maxInventorySize) {
			_selectedItemIndex = 0;
		}
		else {
			_selectedItemIndex++;
		}
	}

	public void SelectPrevSlot() {
		if (_selectedItemIndex - 1 < 0) {
			_selectedItemIndex = _maxInventorySize - 1;
		}
		else {
			_selectedItemIndex--;
		}
	}

	public void SelectSlot(int index) {
		if (index >= 0 && index < _maxInventorySize) {
			_selectedItemIndex = index;
		}
	}



	// Getters
	public InventoryItem GetSelectedItem() {
		if (_currentInventory.Count > _selectedItemIndex)
			return _currentInventory[_selectedItemIndex].Item;
		return null;
	}

	public ItemStack GetSelectedStack() {
		if (_currentInventory.Count > _selectedItemIndex)
			return _currentInventory[_selectedItemIndex];
		return new ItemStack(null, 0);
	}

	public InventoryItem GetItemByIndex(int index) {
		if (index >= 0 && index < _maxInventorySize && _currentInventory.Count > index)
			return _currentInventory[index].Item;
		throw new ArgumentOutOfRangeException("Index is not within inventory range!");
	}

	public ItemStack GetStackByIndex(int index) {
		if (index >= 0 && index < _maxInventorySize && _currentInventory.Count > index)
			return _currentInventory[index];
		throw new ArgumentOutOfRangeException("Index is not within inventory range!");
	}

	public int GetCurrentIndex() {
		return _selectedItemIndex;
	}

	public int GetInventoryMaxSize() {
		return _maxInventorySize;
	}

	public bool IsInventoryFull() {
		return !_currentInventory.Any(e => e.Amount == 0);
	}



	// Helpers
	public void CleanInventory() {
		for (int i = 0; i < _currentInventory.Count; i++) {
			ItemStack stack = _currentInventory[i];
			if (stack.Item == null) {
				stack.Amount = 0;
			}
			if (stack.Amount == 0) {
				stack.Item = null;
			}
			_currentInventory[i] = stack;
		}
	}

	private ItemStack GetStackOf(InventoryItem item) => _currentInventory.First(e => e.Item == item);

	// public void LoadData(GameData data) {
	// 	List<InventoryItem> newInv = new();
	// 	data.PlayerData.InvItemVals.Keys.ToList().ForEach(key => {
	// 		InvData storedData = data.PlayerData.InvItemVals[key];
	// 		if (storedData.ItemType == InventoryItemType.Null) {
	// 			newInv.Add(null);
	// 		}
	// 		else {
	// 			InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
	// 			newItem.name = storedData.ItemName;
	// 			newItem.InvData = storedData;
	// 			newInv.Add(newItem);
	// 		}
	// 	});
	// 	if (newInv.Count > 0) {
	// 		_currentInventory = newInv;
	// 	}
	// 	_selectedItemIndex = data.PlayerData.SelectedInvSlot;
	// }

	// public void SaveData(GameData data) {
	// 	// Hacky conversion between "true null" and object marked as nulltype for use in serialization
	// 	// itemtype should be checked in loadData and converted back to true null
	// 	for (int i = 0; i < _currentInventory.Count; i++) {
	// 		InventoryItem selectedItem = _currentInventory[i];
	// 		if (selectedItem == null) {
	// 			InvData emptyItem = new() {
	// 				ItemType = InventoryItemType.Null
	// 			};
	// 			data.PlayerData.InvItemVals[i.ToString()] = emptyItem;
	// 		}
	// 		else {
	// 			data.PlayerData.InvItemVals[i.ToString()] = selectedItem.InvData;
	// 		}
	// 	}
	// 	data.PlayerData.SelectedInvSlot = _selectedItemIndex;
	// }

}