using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponPickupPoint : BaseInteractable {
	[Header("Item settings")]
	[SerializeField] private InventoryItem _item;
	private SpriteRenderer _spriteRenderer;
	private bool _enabled = true;


	private void Start() {
		if (!_enabled) {
			gameObject.SetActive(false);
		}
		_spriteRenderer = GetComponent<SpriteRenderer>();
		UpdateSprite();
	}




	public override void Interact(GameObject interactor) {
		if (interactor.TryGetComponent(out Inventory inventory)) {
			InventoryItem switchedItem = inventory.AddItem(_item);
			_item = switchedItem;
			UpdateSprite();
			_enabled = false;
		}

	}

	private void UpdateSprite() {
		if (_item == null) {
			_spriteRenderer.sprite = null;
		}
		else {
			_spriteRenderer.sprite = _item.InvData.ItemIcon;

		}
	}

	public override void OnDeselect() {
	}

	public override void OnSelect() {
	}

	public override void LoadData(GameData data) {
		if (data.SceneData.InteractionData.ContainsKey(ObjectId + "SwordExists")) {
			data.SceneData.InteractionData.TryGetValue(ObjectId + "SwordExists", out _enabled);
		}
	}

	public override void SaveData(GameData data) {
		data.SceneData.InteractionData[ObjectId + "SwordExists"] = _enabled;
	}
}