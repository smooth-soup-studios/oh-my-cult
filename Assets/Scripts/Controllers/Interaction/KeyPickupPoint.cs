using Managers;
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

		UIManager.Instance.ShowDialogBox("You picked up a key!", "Use this to unlock the church door.", 5f);
	}

	public override void OnDeselect() {
		_spriteRenderer.color = Color.white;
	}

	public override void OnSelect() {
		_spriteRenderer.color = Color.green;
	}
}