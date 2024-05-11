using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FoodPickupInteractable : BaseItemPickupInteractable {
	[Header("Item settings")]
	[SerializeField]
	private InventoryItem m_Item;
	public override InventoryItem Item { get { return m_Item; } set { m_Item = value; } }

	private SpriteRenderer _spriteRenderer;

	private new void Start() {
		base.Start();

		_spriteRenderer = GetComponent<SpriteRenderer>();
		UpdateSprite();
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
		if (Item == null) {
			_spriteRenderer.sprite = null;
		}
		else {
			_spriteRenderer.sprite = Item.InvData.ItemIcon;

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