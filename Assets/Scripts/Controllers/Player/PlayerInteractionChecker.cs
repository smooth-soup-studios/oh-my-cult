using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteractionChecker : MonoBehaviour {
	private const string _logName = "PlayerInteractionChecker";
	private BaseInteractable _currentInteractable;
	private bool _isInteractionEnabled = true;

	private void Awake() {
		EventBus.Instance.Subscribe<bool>(EventType.INTERACT_TOGGLE, e => {
			string status = e ? "On" : "Off";
			Logger.LogWarning(_logName, $"Interaction toggled to {status}");
			_isInteractionEnabled = e;
		});
	}

	private void Update() {
		List<BaseInteractable> interactables = FindObjectsOfType<BaseInteractable>().ToList();
		BaseInteractable closestInteractable = FindClosestInteractable(interactables);
		if (closestInteractable != _currentInteractable) {
			_currentInteractable = closestInteractable;
			interactables.ForEach(obj => {
				if (obj != _currentInteractable) {
					obj.OnDeselect();
				}
			});

			if (closestInteractable != null) {
				if (!_isInteractionEnabled) {
					closestInteractable.OnSelectWhileDisabled();
					return;
				}
				closestInteractable.OnSelect();
			}
		}
	}

	public BaseInteractable GetCurrentInteractable() {
		return _isInteractionEnabled ? _currentInteractable : null;
	}

	private BaseInteractable FindClosestInteractable(List<BaseInteractable> interactables) {
		float closestDistance = 100f;
		BaseInteractable closest = null;
		interactables.ForEach(obj => {
			float distance = Vector2.Distance(transform.position, obj.transform.position);
			if (distance < closestDistance && distance <= obj.InteractionRange) {
				closest = obj;
				closestDistance = distance;
			}
		});
		return closest;
	}
}