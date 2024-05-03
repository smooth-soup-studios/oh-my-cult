using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushInteractable : BaseInteractable, IInteractablePopulatorConfigurable {
	private ParticleSystem _particleSystem;

	private readonly float _interactCooldown = 1.0f;
	private float _lastInteractTime = -1.0f;
	public bool CanInteract {
		get {
			return Time.time - _lastInteractTime > _interactCooldown;
		}
	}

	public InteractableControllerProperties ComponentProperties { get; set; }

	void Awake() {
		_particleSystem = GetComponent<ParticleSystem>();
		_lastInteractTime = -_interactCooldown;
	}

	public override void Interact(GameObject interactor) {
		base.Interact(interactor);

		if (CanInteract) {
			_lastInteractTime = Time.time;
			_particleSystem.Play();
		}
	}

	public override void OnDeselect() {
	}

	public override void OnSelect() {
	}
}
