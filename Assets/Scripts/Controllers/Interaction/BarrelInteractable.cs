using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ShatterController))]
public class BarrelInteractable : BaseInteractable {
	public InventoryItem DropItem;
	private ShatterController _shatterController;

	private void Awake() {
		_shatterController = GetComponent<ShatterController>();
		EventBus.Instance.Subscribe<(GameObject target, GameObject hitter)>(EventType.HIT, e => { if (e.target == gameObject) OnAttack(e.hitter); });
	}

	public override void OnSelect() {
	}

	public override void OnDeselect() {
	}

	private void OnAttack(GameObject interactor) {
		base.Interact(interactor);
		_shatterController.Shatter();
	}

	private void OnValidate() {
		if (TryGetComponent(out CircleCollider2D _collider)) {
			_collider.radius = InteractionRange;
		}
	}
}