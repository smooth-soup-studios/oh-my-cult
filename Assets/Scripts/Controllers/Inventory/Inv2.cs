using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inv2 : MonoBehaviour {
	private string _logname = "InventorySystem";
	[Header("Settings")]
	[SerializeField] private bool _allowItemOverwrite = false;


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
	}



	public InventoryItem AddItem(InventoryItem item) {
		// Check if item already exists in inventory
		if (_currentInventory.Any(e => e.Item == item)) {
			ItemStack stack = _currentInventory.First(e => e.Item == item);
			stack.Amount++;
			return null;
		}
		// Check if there is an empty slot in the inventory
		else if (_currentInventory.Any(e => e.Amount == 0) && item != null) {
			_currentInventory[_currentInventory.FindIndex(x => x.Amount == 0)] = new ItemStack(item, 1);
			return null;
		}
		//overwrite the item if enabled
		else {
			if (!_allowItemOverwrite) {
				return item;
			}
			InventoryItem oldItem = _currentInventory[_selectedItemIndex].Item;
			_currentInventory[_selectedItemIndex] = new ItemStack(item, 1);
			return oldItem;
		}
	}

	public ItemStack AddItem(ItemStack stack) {
		// Check if item already exists in inventory
		if (_currentInventory.Any(e => e.Item == stack.Item)) {
			ItemStack existingStack = _currentInventory.First(e => e.Item == stack.Item);
			existingStack.Amount += stack.Amount;
			return new ItemStack(null, 0);
		}
		// Check if there is an empty slot in the inventory
		else if (_currentInventory.Any(e => e.Amount == 0) && stack.Item != null) {
			_currentInventory[_currentInventory.FindIndex(x => x.Amount == 0)] = stack;
			return new ItemStack(null, 0);
		}
		//overwrite the item if enabled
		else {
			if (!_allowItemOverwrite) {
				return stack;
			}
			ItemStack oldStack = _currentInventory[_selectedItemIndex];
			_currentInventory[_selectedItemIndex] = stack;
			return oldStack;
		}
	}

	public void RemoveItem(InventoryItem item) {
		RemoveItemByIndex(_currentInventory.FindIndex(e => e.Item == item));
	}
	public void RemoveItem(InventoryItem item, int amount) {
		ItemStack stack = _currentInventory.First(e => e.Item == item);
		stack.Amount -= amount;
		if (stack.Amount <= 0) {
			RemoveItemByIndex(_currentInventory.FindIndex(e => e.Item == item));
		}
	}
	public void RemoveItem(ItemStack stack) {
		ItemStack existingStack = _currentInventory.First(e => e.Item == stack.Item);
		existingStack.Amount -= stack.Amount;
		if (existingStack.Amount <= 0) {
			RemoveItemByIndex(_currentInventory.FindIndex(e => e.Item == stack.Item));
		}
	}

	public void RemoveItemByIndex(int index) {
		if (index >= 0 && index < _maxInventorySize && _currentInventory.Count > index) {
			_currentInventory[index] = new(null, 0);
		}
	}

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
		return null;
	}

	public ItemStack GetStackByIndex(int index) {
		if (index >= 0 && index < _maxInventorySize && _currentInventory.Count > index)
			return _currentInventory[index];
		return new ItemStack(null, 0);
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