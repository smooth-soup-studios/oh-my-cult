using UnityEngine;

public class BowItem : WeaponItem {
	public override void PrimaryAction(GameObject source) {
		try {
			source.GetComponent<WeaponHitbox>().GetUniqueObjectsInCollider().ForEach(obj => {
				if (IsTargetUnObstructed(source, obj)) {
					EventBus.Instance.TriggerEvent(EventType.HIT, (obj, source));
					if (obj.TryGetComponent<HealthController>(out HealthController enemy)) {
						DoPrimaryDamage(enemy);
					}
					if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {
						rb.AddForce(rb.mass * WeaponStats.WeaponData.Knockback * (obj.transform.position - source.transform.position).normalized, ForceMode2D.Impulse);
					}
				}
			});
		}
		catch (System.Exception) {
		}
	}

}