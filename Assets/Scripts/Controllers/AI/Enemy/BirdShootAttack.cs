using UnityEngine;

public class BirdShootAttack : WeaponItem {
	public GameObject Projectile;

	private new void Start() {
		base.Start();
		// Overwrite layermask
		IgnoreThisMask = ~(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
	}


	public override void SecondaryAction(GameObject source) {
		Instantiate(Projectile, source.transform.position, Quaternion.identity);
	}
}
