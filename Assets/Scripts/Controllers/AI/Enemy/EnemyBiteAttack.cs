using UnityEngine;

public class EnemyBiteAttack : WeaponItem {

	private new void Start() {
		base.Start();
		// Overwrite layermask
		IgnoreThisMask = ~(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
	}
}
