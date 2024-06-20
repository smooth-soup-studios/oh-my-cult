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
			source.GetComponentInChildren<WeaponHitbox>().GetUniqueObjectsInCollider().ForEach(obj => {
				if (IsTargetUnObstructed(source, obj)) {
					EventBus.Instance.TriggerEvent(EventType.HIT, (obj, source));
					if (obj.TryGetComponent<HealthController>(out HealthController enemy)) {
						DoPrimaryDamage(enemy);
					}
					if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {
						DoKnockBack(rb, source);
					}
				}
			});
		}
		catch (System.Exception) {
		}
	}

	protected virtual void DoPrimaryDamage(HealthController enemy) {
		if (ScreenShakeManager.Instance) {
			ShakeLayer DamageShakeLayer = ScreenShakeManager.Instance.GetOrAddLayer("PrimaryDamage", true);
			DamageShakeLayer.SetShakeThenStop(2, 2);
		}
		enemy.TakeDamage(WeaponStats.WeaponData.Damage);
	}

	protected virtual void DoKnockBack(Rigidbody2D rb, GameObject source) {
		if (rb.TryGetComponent<KnockbackModifier>(out KnockbackModifier kbm) && !kbm.AllowKnockback) {
			return;
		}
		rb.AddForce(rb.mass * WeaponStats.WeaponData.Knockback * (rb.gameObject.transform.position - source.transform.position).normalized, ForceMode2D.Impulse);
	}

	public override void SecondaryAction(GameObject source) {
		try {
			source.GetComponentInChildren<WeaponHitbox>().GetUniqueObjectsInCollider().ForEach(obj => {
				if (IsTargetUnObstructed(source, obj)) {
					EventBus.Instance.TriggerEvent(EventType.HIT, (obj, source));
					if (obj.TryGetComponent<HealthController>(out HealthController enemy)) {
						DoSecondaryDamage(enemy);
					}
					if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {
						DoKnockBack(rb, source);
					}
				}
			});
		}
		catch (System.Exception) {
		}
	}


	protected virtual void DoSecondaryDamage(HealthController enemy) {
		if (ScreenShakeManager.Instance) {
			ShakeLayer DamageShakeLayer = ScreenShakeManager.Instance.GetOrAddLayer("SecondaryDamage", true);
			DamageShakeLayer.SetShakeThenStop(2, 2);
		}
		enemy.TakeDamage(WeaponStats.WeaponData.Damage);
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