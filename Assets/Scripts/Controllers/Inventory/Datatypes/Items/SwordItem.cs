using UnityEngine;

public class SwordItem : InteractableItem {
	[SerializeField] private WeaponStats _weaponStats;

	public override void PrimaryAction(StateMachine source) {
		source.WeaponHitbox.GetObjectsInCollider().ForEach(obj => {
			if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy)) {
				enemy.TakeDamage(_weaponStats.Damage);
			}
		});
	}

	public override void SecondaryAction(StateMachine source) {
		source.WeaponHitbox.GetObjectsInCollider().ForEach(obj => {
			if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy)) {
				enemy.TakeDamage(_weaponStats.Damage);
			}
		});
	}
}