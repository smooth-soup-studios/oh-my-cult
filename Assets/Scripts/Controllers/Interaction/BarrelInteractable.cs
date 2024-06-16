using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ShatterController))]
[RequireComponent(typeof(TooltipController))]
public class BarrelInteractable : BaseInteractable {
	private static readonly string _logName = "BarrelInteractable";

	public GameObject DroppingItemPrefab;
	public GameObject PickupPointInteractable;
	public ItemStack ItemToDrop;

	private ShatterController _shatterController;
	private TooltipController _tooltipController;


	private void Awake() {
		if (DroppingItemPrefab != null && !DroppingItemPrefab.TryGetComponent(out DroppingItemController _)) {
			Logger.LogError(_logName, "DroppingItemPrefab must have a DroppingItemController component attached to it.");
		}

		_shatterController = GetComponent<ShatterController>();
		_tooltipController = GetComponent<TooltipController>();

		EventBus.Instance.Subscribe<(GameObject target, GameObject hitter)>(EventType.HIT, e => { if (e.target == gameObject) OnAttack(e.hitter); });
	}

	public override void Interact(GameObject interactor) { }
	public override void OnSelect() {
		if (_tooltipController) {
			_tooltipController.ShowTooltip();
		}
	}
	public override void OnDeselect() {
		if (_tooltipController) {
			_tooltipController.HideTooltip();
		}
	}
	public override void OnSelectWhileDisabled() {
	}


	private void OnAttack(GameObject interactor) {
		base.Interact(interactor);
		_shatterController.Shatter(interactor);
		GetComponent<SpriteRenderer>().enabled = false;

		if (VibrationManager.Instance) {
			VibrationManager.Instance.GetOrAddLayer(VibrationLayerNames.ReceivePrimaryDamage, true).SetShakeThenStop(.1f, .1f, .2f);
		}

		if (DroppingItemPrefab != null && PickupPointInteractable != null && ItemToDrop != null) {
			GameObject dip = Instantiate(DroppingItemPrefab, transform.position, Quaternion.identity);
			dip.GetComponent<DroppingItemController>().PickupPointInteractable = PickupPointInteractable;
			dip.GetComponent<DroppingItemController>().ItemToDrop = ItemToDrop;
		}

		if (SingleUse) {
			Destroy(gameObject, _shatterController.ShatterLifetime);
		}
	}

	private new void OnValidate() {
		base.OnValidate();
		SingleUse = true;
	}
}