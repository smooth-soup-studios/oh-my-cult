using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ShatterController))]
public class BarrelInteractable : BaseInteractable {
	private static readonly string _logName = "BarrelInteractable";

	public GameObject DroppingItemPrefab;
	public GameObject PickupPointInteractable;
	public InventoryItem ItemToDrop;

	private ShatterController _shatterController;

	private void Awake() {
		if (DroppingItemPrefab != null && !DroppingItemPrefab.TryGetComponent(out DroppingItemController _)) {
			Logger.LogError(_logName, "DroppingItemPrefab must have a DroppingItemController component attached to it.");
		}

		_shatterController = GetComponent<ShatterController>();
		EventBus.Instance.Subscribe<(GameObject target, GameObject hitter)>(EventType.HIT, e => { if (e.target == gameObject) OnAttack(e.hitter); });
	}

	public override void OnSelect() {
	}

	public override void OnDeselect() {
	}

	private void OnAttack(GameObject interactor) {
		base.Interact(interactor);
		_shatterController.Shatter(interactor);
		GetComponent<SpriteRenderer>().enabled = false;

		if (DroppingItemPrefab != null && PickupPointInteractable != null && ItemToDrop != null) {
			GameObject dip = Instantiate(DroppingItemPrefab, transform.position, Quaternion.identity);
			dip.GetComponent<DroppingItemController>().PickupPointInteractable = PickupPointInteractable;
			dip.GetComponent<DroppingItemController>().ItemToDrop = ItemToDrop;
		}

		if (SingleUse) {
			Destroy(gameObject, _shatterController.ShatterLifetime);
		}
	}

	private void OnValidate() {
		SingleUse = true;
	}
}