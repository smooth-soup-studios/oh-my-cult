using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class KeyPickupPoint : BaseInteractable {
	public bool Exists = true;
	private SpriteRenderer _spriteRenderer;

	void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public override void Interact(GameObject interactor) {
		Exists = false;
		interactor.GetComponent<StateMachine>().HasDoorKey = true;
		Destroy(gameObject);
	}

	public override void OnDeselect() {
		_spriteRenderer.color = Color.white;
	}

	public override void OnSelect() {
		_spriteRenderer.color = Color.green;
	}
}