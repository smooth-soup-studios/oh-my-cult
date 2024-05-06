using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushInteractable : BaseInteractable {
	private ParticleSystem _particleSystem;

	private readonly float _interactCooldown = 1.0f;
	private float _lastInteractTime = -1.0f;
	private bool CanInteract {
		get {
			return Time.time - _lastInteractTime > _interactCooldown;
		}
	}

	void Awake() {
		_particleSystem = GetComponent<ParticleSystem>();
		_lastInteractTime = -_interactCooldown;
		EventBus.Instance.Subscribe<(GameObject target, GameObject hitter)>(EventType.HIT, e => { if (e.target == gameObject) Interact(e.hitter); });
		if (TryGetComponent<CircleCollider2D>(out CircleCollider2D _collider)) {
			_collider.radius = InteractionRange;
		}
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
