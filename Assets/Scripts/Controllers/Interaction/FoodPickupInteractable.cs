using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FoodPickupInteractable : BaseItemPickupInteractable {
	private SpriteRenderer _spriteRenderer;

	private new void Start() {
		base.Start();

		// SingleUse = true;

		_spriteRenderer = GetComponent<SpriteRenderer>();
		UpdateSprite();
	}

	public override void Interact(GameObject interactor) {
		if (interactor.TryGetComponent(out Inventory inventory) && !inventory.IsInventoryFull()) {
			InventoryItem switchedItem = inventory.AddItem(Item);
			Item = switchedItem;
			UpdateSprite();
			base.Interact(interactor);
		}
	}

	private void UpdateSprite() {
		OnValidate(); // Yea it's not how you're supposed to use it but IDC.
	}

	private new void OnValidate() {
		base.OnValidate();
		SpriteRenderer renderer;
		if (_spriteRenderer) {
			renderer = _spriteRenderer;
		}
		else {
			renderer = GetComponent<SpriteRenderer>();
		}

		if (Item == null) {
			renderer.sprite = null;
		}
		else {
			Sprite itemSprite;
			if (Item.InvData.ItemPrefab.TryGetComponent<SpriteRenderer>(out SpriteRenderer srenderer)) {
				itemSprite = srenderer.sprite;
			}
			else {
				itemSprite = Item.InvData.ItemIcon;
			}
			renderer.sprite = itemSprite;
		}
	}

	public override void OnDeselect() {
		_spriteRenderer.color = Color.white;
	}

	public override void OnSelect() {
		_spriteRenderer.color = Color.green;
	}

	public override void LoadData(GameData data) {
		base.LoadData(data);
		if (data.SceneData.InteractionData.ContainsKey($"{ObjectId}-FoodExists")) {
			data.SceneData.InteractionData.TryGetValue($"{ObjectId}-FoodExists", out HasBeenUsed);
		}
	}

	public override void SaveData(GameData data) {
		base.SaveData(data);
		data.SceneData.InteractionData[$"{ObjectId}-FoodExists"] = HasBeenUsed;
	}
}