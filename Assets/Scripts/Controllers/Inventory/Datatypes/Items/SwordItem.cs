using UnityEngine;

public class SwordItem : InteractableItem {
	public WeaponStats WeaponStats;

	public override void PrimaryAction(StateMachine source) {
		try {
			source.WeaponHitbox.GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy)) {
					enemy.TakeDamage(WeaponStats.WeaponData.Damage);

					if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {
						rb.AddForce((obj.transform.position - source.transform.position).normalized * WeaponStats.WeaponData.Knockback * rb.mass, ForceMode2D.Impulse);
					}
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
					enemy.TakeDamage(WeaponStats.WeaponData.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
	}
}