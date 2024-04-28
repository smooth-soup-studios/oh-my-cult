using UnityEngine;

public class SwordItem : InteractableItem {
	public WeaponStats WeaponStats;

	public override void PrimaryAction(StateMachine source) {
		try {
			source.WeaponHitbox.GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<HealthController>(out HealthController enemy)) {
					enemy.TakeDamage(WeaponStats.WeaponData.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
	}

	public override void SecondaryAction(StateMachine source) {
		try {
			source.WeaponHitbox.GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<HealthController>(out HealthController enemy)) {
					enemy.TakeDamage(WeaponStats.WeaponData.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
	}
}