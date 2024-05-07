using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBiteAttack : MonoBehaviour {
	[SerializeField] private WeaponStats _weaponData;
	public void Attack() {
		try {
			GetComponentInChildren<WeaponHitbox>().GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy)) {
					enemy.TakeDamage(_weaponData.WeaponData.Damage);

					if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {
						// rb.AddForce((obj.transform.position - transform.position).normalized * _weaponData.WeaponData.Knockback, ForceMode2D.Impulse);
						rb.AddForce((obj.transform.position - transform.position).normalized * 100, ForceMode2D.Impulse);
					}
				}
			});
		}
		catch (System.Exception) {
		}
	}
}
