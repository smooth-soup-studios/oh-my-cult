using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveable {
	private string _logname = "InventorySystem";
	private List<InventoryItem> _currentInventory = new();
	private int _maxInventorySize = 5;
	private int _selectedItemIndex = 0;


	private void Awake() {
		// Need to initialize the list with the max size. Should be overwritten by LoadData if we ever add expanding inventory space.
		_currentInventory = new List<InventoryItem>(new InventoryItem[_maxInventorySize]);
		EventBus.Instance.Subscribe<int>(EventType.HOTBAR_SELECT, SelectSlot);
		EventBus.Instance.Subscribe<int>(EventType.HOTBAR_SWITCH, e => {
			Logger.Log(_logname, "Work");
			if (e > 0) {
				SelectNextSlot();
			}
			else {
				SelectPrevSlot();
			}
		});
	}



	public InventoryItem AddItem(InventoryItem item) {
		if (_currentInventory.Contains(null)) {
			_currentInventory[_currentInventory.FindIndex(x => x==null)] = item;
			return null;
		}
		else {
			InventoryItem oldItem = _currentInventory[_selectedItemIndex];
			_currentInventory[_selectedItemIndex] = item;
			return oldItem;
		}

	}

	public void RemoveItem(InventoryItem item) {
		_currentInventory.Remove(item);
	}

	public void RemoveItemByIndex(int index) {
		if (index >= 0 && index < _maxInventorySize && _currentInventory.Count > index) {
			_currentInventory.RemoveAt(index);
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
			return _currentInventory[_selectedItemIndex];
		return null;
	}

	public InventoryItem GetItemByIndex(int index) {
		if (index >= 0 && index < _maxInventorySize && _currentInventory.Count > index)
			return _currentInventory[index];
		return null;
	}

	public int GetCurrentIndex() {
		return _selectedItemIndex;
	}

	public int GetInventoryMaxSize() {
		return _maxInventorySize;
	}





	public void LoadData(GameData data) {
		List<InventoryItem> newInv = new();
		data.PlayerData.InvItemVals.Keys.ToList().ForEach(key => {
			InvData storedData = data.PlayerData.InvItemVals[key];
			if (storedData.ItemType == InventoryItemType.Null) {
				newInv.Add(null);
			}
			else {
				InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
				newItem.name = storedData.ItemName;
				newItem.InvData = storedData;
				newInv.Add(newItem);
			}
		});
		if (newInv.Count > 0) {
			_currentInventory = newInv;
		}
	}

	public void SaveData(GameData data) {
		// Hacky conversion between "true null" and object marked as nulltype for use in serialization
		// itemtype should be checked in loadData and converted back to true null
		for (int i = 0; i < _currentInventory.Count; i++) {
			InventoryItem selectedItem = _currentInventory[i];
			if (selectedItem == null) {
				InvData emptyItem = new() {
					ItemType = InventoryItemType.Null
				};
				data.PlayerData.InvItemVals[i.ToString()] = emptyItem;
			}
			else {
				data.PlayerData.InvItemVals[i.ToString()] = selectedItem.InvData;
			}
		}
	}

}