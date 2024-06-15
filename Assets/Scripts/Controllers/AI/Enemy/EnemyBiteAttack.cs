using UnityEngine;

public class EnemyBiteAttack : WeaponItem {

	private new void Start() {
		base.Start();
		// Overwrite layermask
		IgnoreThisMask = ~(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
	}

	protected override void DoPrimaryDamage(HealthController enemy) {
		base.DoPrimaryDamage(enemy);
		if (VibrationManager.Instance) {
			VibrationManager.Instance.GetOrAddLayer(VibrationLayerNames.ReceivePrimaryDamage, true).SetShakeThenStop(.25f, .5f, .25f);
		}
	}
}
