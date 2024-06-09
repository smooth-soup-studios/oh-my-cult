using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ItemPickupGlowController))]
public class ChestInteractable : BaseInteractable {
	private ItemPickupGlowController _glowController;
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Sprite _openChestSprite;
	public GameObject DroppingItemPrefab;
	public GameObject PickupPointInteractable;
	public ItemStack ItemToDrop;

	private new void Start() {
		base.Start();
		_glowController = GetComponent<ItemPickupGlowController>();
		if (HasBeenUsed) {
			_spriteRenderer.sprite = _openChestSprite;
		}
	}

	public override void OnSelect() {

		if (HasBeenUsed) {
			_spriteRenderer.color = Color.white;
			_glowController.StopGlow();
			return;
		}
		_glowController.StartGlow();
		_spriteRenderer.color = Color.green;
	}

	public override void OnDeselect() {
		_glowController.StopGlow();
		_spriteRenderer.color = Color.white;
	}

	public override void Interact(GameObject interactor) {
		if (!HasBeenUsed) {
			OpenChest();
			_spriteRenderer.color = Color.white;
			_glowController.StopGlow();
			base.Interact(interactor);
		}
	}

	private void OpenChest() {
		_spriteRenderer.sprite = _openChestSprite;
		EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, "ChestOpen");


		if (DroppingItemPrefab != null && PickupPointInteractable != null && ItemToDrop != null) {
			GameObject dip = Instantiate(DroppingItemPrefab, transform.position, Quaternion.identity);
			dip.GetComponent<DroppingItemController>().PickupPointInteractable = PickupPointInteractable;
			dip.GetComponent<DroppingItemController>().ItemToDrop = ItemToDrop;
		}
	}
}
