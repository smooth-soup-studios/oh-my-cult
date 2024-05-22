using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public abstract class BaseItemPickupInteractable : BaseInteractable {
	[Header("Item settings")]
	public ItemStack PickupStack;

	protected SpriteRenderer SpriteRenderer;

	protected new void Start() {
		base.Start();
		SpriteRenderer = GetComponent<SpriteRenderer>();
		UpdateSprite();
	}


	public override void Interact(GameObject interactor) {
		if (interactor.TryGetComponent(out Inventory inventory) && !inventory.IsInventoryFull()) {
			DoPickupInteraction(inventory);
			base.Interact(interactor);
		}
	}

	// Split this into a different method for easier overriding of Interact behaviour
	protected virtual void DoPickupInteraction(Inventory inventory) {
		ItemStack switchedItem = inventory.AddItem(PickupStack);
		PickupStack = switchedItem;
		UpdateSprite();
	}



	protected void UpdateSprite() {
		OnValidate(); // Yea it's not how you're supposed to use it but IDC.
	}

	public override void OnDeselect() {
		SpriteRenderer.color = Color.white;
	}

	public override void OnSelect() {
		SpriteRenderer.color = Color.green;
	}

	protected new void OnValidate() {
		base.OnValidate();

		// Can be called before the renderer is initialized
		SpriteRenderer renderer;
		if (SpriteRenderer) {
			renderer = SpriteRenderer;
		}
		else {
			renderer = GetComponent<SpriteRenderer>();
		}


		if (PickupStack.Item == null) {
			renderer.sprite = null;
		}
		else {
			// Change the rendering sprite of the object to the either the item's in-game icon if present or the inventory icon if not
			// If no sprite is assigned it defaults to null
			Sprite itemSprite;
			if (PickupStack.Item.InvData.ItemPrefab.TryGetComponent<SpriteRenderer>(out SpriteRenderer srenderer)) {
				itemSprite = srenderer.sprite;
			}
			else {
				itemSprite = PickupStack.Item.InvData.ItemIcon;
			}
			renderer.sprite = itemSprite;
		}
	}

	public override void LoadData(GameData data) {
		base.LoadData(data);
		if (data.SceneData.InteractionItems.ContainsKey($"{ObjectId}-Item")) {
			data.SceneData.InteractionItems.TryGetValue($"{ObjectId}-Item", out ItemDataStack datastack);
			PickupStack = datastack.ToRegular();
		}

	}

	public override void SaveData(GameData data) {
		base.SaveData(data);
		data.SceneData.InteractionItems[$"{ObjectId}-Item"] = PickupStack.ToSerializable();
	}
}