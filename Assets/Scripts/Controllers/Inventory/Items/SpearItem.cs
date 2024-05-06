public class SpearItem : InteractableItem {
	public WeaponStats WeaponStats;

	public override void PrimaryAction(StateMachine source) {
		try {
			source.WeaponHitbox.GetObjectsInCollider().ForEach(obj => {
				EventBus.Instance.TriggerEvent(EventType.HIT, (obj, source.gameObject));
				if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy)) {
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
				EventBus.Instance.TriggerEvent(EventType.HIT, (obj, source.gameObject));
				if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy)) {
					enemy.TakeDamage(WeaponStats.WeaponData.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
	}
}