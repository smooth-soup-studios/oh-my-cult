using UnityEngine;
using UnityEngine.Events;

public class EventIneractable : BaseInteractable {
	[Header("Event Settings")]
	[SerializeField] private UnityEvent<GameObject> _event = new();


	public override void Interact(GameObject interactor) {
		_event.Invoke(interactor);
		base.Interact(interactor);

	}

	public override void OnDeselect() {
	}

	public override void OnSelect() {
	}
}