using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ItemPickupGlowController))]
public class ChestInteractable : BaseInteractable {
	private ItemPickupGlowController _glowController;
	[SerializeField] private UnityEvent<GameObject> _event = new();
	[SerializeField] SpriteRenderer _spriteRenderer;
	public ChestController ChestController;

	private new void Start() {
		base.Start();
		_glowController = GetComponent<ItemPickupGlowController>();
	}


	public override void OnDeselect() {
		_glowController.StopGlow();
		_spriteRenderer.color = Color.white;
	}

	public override void OnSelect() {

		if (ChestController.SpriteRenderer.sprite == ChestController.ChestOpen) {
			_spriteRenderer.color = Color.white;
			_glowController.StopGlow();
			return; 
		}
		_glowController.StartGlow();
		_spriteRenderer.color = Color.green;
		Logger.Log("Chest", "Interact");
	}

	public override void Interact(GameObject interactor) {
		_event.Invoke(interactor);
		base.Interact(interactor);

	}


}
