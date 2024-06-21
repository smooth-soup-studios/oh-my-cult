using UnityEngine;


[RequireComponent(typeof(SpriteRenderer), typeof(TooltipController))]
public abstract class BaseItemPickupInteractable : BaseInteractable {
	[Header("Item settings")]
	public ItemStack PickupStack;
	public bool RespawnItem = false;

	protected SpriteRenderer RendererOfSprites;
	protected TooltipController TooltipController;

	protected new void Start() {
		base.Start();
		RendererOfSprites = GetComponent<SpriteRenderer>();
		TooltipController = GetComponent<TooltipController>();
		UpdateSprite();
	}


	public override void Interact(GameObject interactor) {
		// Check if the target has an inventory and if so, check if the inventory is empty or the item stackable
		if (interactor.TryGetComponent(out Inventory inventory) && (!inventory.IsInventoryFull() | inventory.IsItemInInventoryAndStackable(PickupStack))) {
			DoPickupInteraction(inventory);
			base.Interact(interactor);
		}
	}

	// Split this into a different method for easier overriding of Interact behaviour
	protected virtual void DoPickupInteraction(Inventory inventory) {
		ItemStack switchedItem = inventory.AddItem(PickupStack);
		if (!RespawnItem) {
			PickupStack = switchedItem;
		}
		UpdateSprite();
		TooltipController.Select();
		TooltipController.HideTooltip();
	}



	protected void UpdateSprite() {
		OnValidate(); // Yea it's not how you're supposed to use it but IDC.
	}

	public override void OnDeselect() {
		RendererOfSprites.color = Color.white;
		TooltipController.HideTooltip();
	}

	public override void OnSelect() {
		if (PickupStack.Item != null) {
			RendererOfSprites.color = Color.green;
			TooltipController.ShowTooltip();
		}
	}

	protected new void OnValidate() {
		base.OnValidate();

		// Can be called before the renderer is initialized
		if (!RendererOfSprites) {
			RendererOfSprites = GetComponent<SpriteRenderer>();
		}

		if (PickupStack.Item == null) {
			RendererOfSprites.sprite = null;
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
			RendererOfSprites.sprite = itemSprite;
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