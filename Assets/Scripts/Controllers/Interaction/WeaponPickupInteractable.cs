using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponPickupPoint : BaseItemPickupInteractable {

	private SpriteRenderer _spriteRenderer;
	private ItemPickupGlowController _glowController;

	private new void Start() {
		base.Start();

		_spriteRenderer = GetComponent<SpriteRenderer>();
		UpdateSprite();

		_glowController = GetComponent<ItemPickupGlowController>();
	}


	public override void Interact(GameObject interactor) {
		if (interactor.TryGetComponent(out Inventory inventory)) {
			InventoryItem switchedItem = inventory.AddItem(Item);
			Item = switchedItem;
			UpdateSprite();
		}
		base.Interact(interactor);
	}

	private void UpdateSprite() {
		OnValidate(); // Yea it's not how you're supposed to use it but IDC.
	}

	public override void OnDeselect() {
		_spriteRenderer.color = Color.white;
		_glowController.StopGlow();
	}

	public override void OnSelect() {
		_spriteRenderer.color = Color.green;
		_glowController.StartGlow();
	}

	private void OnValidate() {
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

	public override void LoadData(GameData data) {
		base.LoadData(data);
		if (data.SceneData.InteractionData.ContainsKey($"{ObjectId}-SwordExists")) {
			data.SceneData.InteractionData.TryGetValue($"{ObjectId}-SwordExists", out HasBeenUsed);
		}
		if (data.SceneData.InteractionData.ContainsKey($"{ObjectId}-ItemIsNull")) {
			data.SceneData.InteractionData.TryGetValue($"{ObjectId}-ItemIsNull", out bool ItemNull);
			if (ItemNull) {
				Item = null;
			}
		}
	}

	public override void SaveData(GameData data) {
		base.SaveData(data);
		data.SceneData.InteractionData[$"{ObjectId}-SwordExists"] = HasBeenUsed;
		data.SceneData.InteractionData[$"{ObjectId}-ItemIsNull"] = Item == null;
	}
}