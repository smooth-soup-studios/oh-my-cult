using UnityEngine;

public class EnemyBiteAttack : MonoBehaviour {
	[SerializeField] private WeaponStats _weaponData;
	public void Attack() {
		try {
			GetComponentInChildren<WeaponHitbox>().GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<HealthController>(out HealthController enemy)) {
					enemy.TakeDamage(_weaponData.WeaponData.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
	}
}
