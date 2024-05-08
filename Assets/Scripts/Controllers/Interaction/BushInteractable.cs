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
	}

	public override void Interact(GameObject interactor) {
		base.Interact(interactor);

		if (CanInteract) {
			_lastInteractTime = Time.time;
			_particleSystem.Play();
		}
	}

	private void OnValidate() {
		if (TryGetComponent<CircleCollider2D>(out CircleCollider2D _collider)) {
			_collider.radius = InteractionRange;
		}
	}

	// Disable default behavior of these methods
	public override void OnDeselect() {
	}

	public override void OnSelect() {
	}

	public override void SaveData(GameData data) {
	}
	public override void LoadData(GameData data) {
	}
}