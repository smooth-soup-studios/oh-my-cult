using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushInteractable : BaseInteractable, IInteractablePopulatorConfigurable {
	public int Hello { get; set; } = 0;
	public InteractableControllerProperties ComponentProperties { get; set; }

	void Awake() {
		// ComponentProperties = new BushInteractableProperties();
	}

	public override void Interact(GameObject interactor) {
		base.Interact(interactor);
	}

	public override void OnDeselect() {
	}

	public override void OnSelect() {
	}
}
