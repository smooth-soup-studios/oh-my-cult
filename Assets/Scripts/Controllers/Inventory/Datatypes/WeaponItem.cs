using UnityEngine;

public class WeaponItem : InteractableItem {
	public WeaponStats WeaponStats;

	public override void PrimaryAction(StateMachine source) {
		try {
			source.WeaponHitbox.GetObjectsInCollider().ForEach(obj => {
				if (IsTargetUnObstructed(source.gameObject, obj)) {
					EventBus.Instance.TriggerEvent(EventType.HIT, (obj, source.gameObject));
					if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy)) {
						enemy.TakeDamage(WeaponStats.WeaponData.Damage);
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
				if (IsTargetUnObstructed(source.gameObject, obj)) {
					EventBus.Instance.TriggerEvent(EventType.HIT, (obj, source.gameObject));
					if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy)) {
						enemy.TakeDamage(WeaponStats.WeaponData.Damage);
					}
				}
			});
		}
		catch (System.Exception) {
		}
	}

	protected bool IsTargetUnObstructed(GameObject source, GameObject target) {
		Debug.DrawRay(source.transform.position, target.transform.position - source.transform.position, Color.red, 10f);

		// Layermask is a bitmask so bitshift the layers to get the mask, use | to combine them and ~ to invert the result for blacklisting.
		// Unity's Physics2D.IgnoreRaycastLayer is set to 4 which is in fact not the ignore raycast layer but the water layer. -_-
		LayerMask layerMask = ~(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
		RaycastHit2D hit = Physics2D.Raycast(source.transform.position, target.transform.position - source.transform.position, Vector3.Distance(target.transform.position, source.transform.position), layerMask);
		if (hit) {
			return hit.collider.gameObject == target;
		}
		return true;
	}
}