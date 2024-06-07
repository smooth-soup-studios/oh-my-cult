using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ItemPickupGlowController))]
public class WeaponPickupPoint : BaseItemPickupInteractable {

	private ItemPickupGlowController _glowController;

	private new void Start() {
		base.Start();
		_glowController = GetComponent<ItemPickupGlowController>();
	}


	protected override void DoPickupInteraction(Inventory inventory) {
		base.DoPickupInteraction(inventory);
		_glowController.StopGlow();

	}


	public override void OnDeselect() {
		base.OnDeselect();
		_glowController.StopGlow();
	}

	public override void OnSelect() {
		base.OnSelect();
		if (PickupStack.Item != null) {
			_glowController.StartGlow();
		}
	}
}