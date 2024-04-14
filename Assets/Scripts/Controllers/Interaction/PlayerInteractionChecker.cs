using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteractionChecker : MonoBehaviour {
	private BaseInteractable _currentInteractable;

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
		}
	}

	public BaseInteractable GetCurrentInteractable() {
		return _currentInteractable;
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