using UnityEngine;

public class SwordItem : InteractableItem {
	[SerializeField] private WeaponStats _weaponStats;

	public override void PrimaryAction(StateMachine source) {
		try {
			source.WeaponHitbox.GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy)) {
					enemy.TakeDamage(_weaponStats.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
	}

	public override void SecondaryAction(StateMachine source) {
		try {
			source.WeaponHitbox.GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy)) {
					enemy.TakeDamage(_weaponStats.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
	}
}