using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveable {
	private List<InventoryItem> _currentInventory = new();
	[SerializeField] private int _maxInventorySize = 1;
	[SerializeField] private int _selectedItemIndex = 0;


	private void Awake() {
		// Need to initialize the list with the max size. Should be overwritten by LoadData if we ever add expanding inventory space.
		_currentInventory = new List<InventoryItem>(new InventoryItem[_maxInventorySize]);
	}



	public InventoryItem AddItem(InventoryItem item) {
		if (_currentInventory.Count < _maxInventorySize) {
			_currentInventory.Add(item);
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
		if (_selectedItemIndex + 1 > _maxInventorySize) {
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





	public void LoadData(GameData data) {
		List<InventoryItem> newInv = new();
		data.SceneData.InvItemVals.Keys.ToList().ForEach(key => {
			InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
			newItem.InvData = data.SceneData.InvItemVals[key];
			newInv.Add(newItem);
		});
		if (newInv.Count > 0) {
			_currentInventory = newInv;
		}
	}

	public void SaveData(GameData data) {
		_currentInventory.ForEach(x => {
			data.SceneData.InvItemVals[x.name] = x.InvData;
		});
	}

}