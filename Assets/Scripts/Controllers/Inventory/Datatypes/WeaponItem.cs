using UnityEngine;

public class WeaponItem : InteractableItem {
	public WeaponStats WeaponStats;
	protected LayerMask IgnoreThisMask;

	protected void Start() {
		// Layermask is a bitmask so bitshift the layers to get the mask, use | to combine them and ~ to invert the result for blacklisting.
		// Unity's Physics2D.IgnoreRaycastLayer is set to 4 which is in fact not the ignore raycast layer but the water layer. -_-
		IgnoreThisMask = ~(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
	}

	public override void PrimaryAction(GameObject source) {
		try {
			source.GetComponentInChildren<WeaponHitbox>().GetObjectsInCollider().ForEach(obj => {
				if (IsTargetUnObstructed(source, obj)) {
					EventBus.Instance.TriggerEvent(EventType.HIT, (obj, source));
					if (obj.TryGetComponent<HealthController>(out HealthController enemy)) {
						enemy.TakeDamage(WeaponStats.WeaponData.Damage);
					}
					if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {
						rb.AddForce((obj.transform.position - transform.position).normalized * _weaponData.WeaponData.Knockback * rb.mass, ForceMode2D.Impulse);
					}
				}
			});
		}
		catch (System.Exception) {
		}
	}

	public override void SecondaryAction(GameObject source) {
		try {
			source.GetComponentInChildren<WeaponHitbox>().GetObjectsInCollider().ForEach(obj => {
				if (IsTargetUnObstructed(source, obj)) {
					EventBus.Instance.TriggerEvent(EventType.HIT, (obj, source));
					if (obj.TryGetComponent<HealthController>(out HealthController enemy)) {
						enemy.TakeDamage(WeaponStats.WeaponData.Damage);
					}
					if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {
						rb.AddForce((obj.transform.position - transform.position).normalized * _weaponData.WeaponData.Knockback * rb.mass, ForceMode2D.Impulse);
					}
				}
			});
		}
		catch (System.Exception) {
		}
	}

	protected bool IsTargetUnObstructed(GameObject source, GameObject target) {
		Debug.DrawRay(source.transform.position, target.transform.position - source.transform.position, Color.red, 10f);

		RaycastHit2D hit = Physics2D.Raycast(source.transform.position, target.transform.position - source.transform.position, Vector3.Distance(target.transform.position, source.transform.position), IgnoreThisMask);
		if (hit) {
			return hit.collider.gameObject == target;
		}
		return true;
	}
}