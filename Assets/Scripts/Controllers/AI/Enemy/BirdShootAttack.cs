using System.Collections.Generic;
using UnityEngine;

public class BirdShootAttack : WeaponItem {
	public GameObject Projectile;
	public GameObject Origin;

	private new void Start() {
		base.Start();
		// Overwrite layermask
		IgnoreThisMask = ~(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
	}

	protected override List<GameObject> GetTargets(GameObject source) {
		List<GameObject> targets = base.GetTargets(source);
		targets.Remove(Origin);
		return targets;
	}


	public override void SecondaryAction(GameObject source) {
		GameObject newProjectile = Instantiate(Projectile, source.transform.position, Quaternion.identity);
		if (newProjectile.TryGetComponent<Projectile>(out Projectile projectile)) {
			projectile.Origin = Origin;
		}
	}
}
