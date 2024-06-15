using UnityEngine;

public class BearSmackAttack : WeaponItem {

	private new void Start() {
		base.Start();
		// Overwrite layermask
		IgnoreThisMask = ~(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
	}

	protected override void DoPrimaryDamage(HealthController enemy) {
		base.DoPrimaryDamage(enemy);
		if (VibrationManager.Instance) {
			VibrationManager.Instance.GetOrAddLayer(VibrationLayerNames.ReceivePrimaryDamage, true).SetShakeThenStop(.75f, 1f, .3f);
		}
	}
}
