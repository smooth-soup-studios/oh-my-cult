using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponPickupPoint : BaseInteractable {
	[SerializeField] private InventoryItem _item;
	private SpriteRenderer _spriteRenderer;


	private void Start() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
		UpdateSprite();
	}




	public override void Interact(GameObject interactor) {
		if (interactor.TryGetComponent(out Inventory inventory)) {
			HotbarManager.AddItems(_item);
			InventoryItem switchedItem = inventory.AddItem(_item);
			_item = switchedItem;
			UpdateSprite();
		}

	}

	private void UpdateSprite() {
		if (_item == null) {
			_spriteRenderer.sprite = null;
		}
		else {
			_spriteRenderer.sprite = _item.ItemIcon;
		}
	}

	public override void OnDeselect() {
	}

	public override void OnSelect() {
	}
}