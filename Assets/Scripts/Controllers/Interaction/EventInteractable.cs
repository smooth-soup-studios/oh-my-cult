using UnityEngine;
using UnityEngine.Events;

public class EventInteractable : BaseInteractable {
	[Header("Event Settings")]
	[SerializeField] private UnityEvent<GameObject> _event = new();
	[SerializeField] SpriteRenderer _spriteRenderer;

	public override void Interact(GameObject interactor) {
		_event.Invoke(interactor);
		base.Interact(interactor);

	}

	public override void OnDeselect() {
		if (_spriteRenderer) {
			_spriteRenderer.color = Color.white;

		}
	}

	public override void OnSelect() {
		if (_spriteRenderer) {
			_spriteRenderer.color = Color.green;
		}
	}
}