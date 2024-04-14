using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HotbarManager : MonoBehaviour
{
    //[SerializeField] private GameObject _swordUI;
    //[SerializeField] private InventoryItem _item;
    bool _interact = false;
    static VisualElement _root;
    static int _hotbarIndex = 0;
    
    private void OnEnable(){
        EventBus.Instance.Subscribe<bool>(EventType.INTERACT, OnInteract);
        _root = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnInteract(bool value) {
		_interact = value;
        //AddItems();
	}

    public static void AddItems(InventoryItem item){
		Image itemslot = new Image {
			name = "item1",
			sprite = item.ItemIcon
		};
		VisualElement hotbar = _root.Q<VisualElement>("Overview").Q<VisualElement>("Footer").Q<VisualElement>("Inventory").Q<VisualElement>("Row1");
        List<VisualElement> list = hotbar.Query<VisualElement>("ItemSlot").ToList();
        //Set in the correct index slot in the hotbar
        if(_hotbarIndex <= list.Count){
            list.ElementAt(_hotbarIndex).Add(itemslot);
        }
        _hotbarIndex++;
    }
}
