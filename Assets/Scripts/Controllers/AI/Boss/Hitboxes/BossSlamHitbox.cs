using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossSlamHitbox : MonoBehaviour {
	[SerializeField] private WeaponStats _weaponData;
	private List<GameObject> _objectsInCollider = new();
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.GetComponentsInChildren<BossSlamHitbox>().Contains(this))
			return;
		_objectsInCollider.Add(other.gameObject);
	}

	private void OnTriggerExit2D(Collider2D other) {
		_objectsInCollider.Remove(other.gameObject);
	}

	public List<GameObject> GetObjectsInCollider() {
		_objectsInCollider = _objectsInCollider.Where(obj => obj != null).ToList();
		return _objectsInCollider;
	}
	public void HitboxDown() {
		GetComponentsInChildren<BossSlamHitbox>().Where(e => e != null).ToList().ForEach(e => e.GetObjectsInCollider().ForEach(obj => {
			if (obj.TryGetComponent<HealthController>(out HealthController opponent)) {
				opponent.TakeDamage(_weaponData.WeaponData.Damage);
			}

		}));
	}
		public void HitboxLeft() {
		GetComponentsInChildren<BossSlamHitbox>().Where(e => e != null).ToList().ForEach(e => e.GetObjectsInCollider().ForEach(obj => {
			if (obj.TryGetComponent<HealthController>(out HealthController opponent)) {
				opponent.TakeDamage(_weaponData.WeaponData.Damage);
			}

		}));
	}
		public void HitboxRight() {
		GetComponentsInChildren<BossSlamHitbox>().Where(e => e != null).ToList().ForEach(e => e.GetObjectsInCollider().ForEach(obj => {
			if (obj.TryGetComponent<HealthController>(out HealthController opponent)) {
				opponent.TakeDamage(_weaponData.WeaponData.Damage);
			}

		}));
	}
		public void HitboxUp() {
		GetComponentsInChildren<BossSlamHitbox>().Where(e => e != null).ToList().ForEach(e => e.GetObjectsInCollider().ForEach(obj => {
			if (obj.TryGetComponent<HealthController>(out HealthController opponent)) {
				opponent.TakeDamage(_weaponData.WeaponData.Damage);
			}

		}));
	}
}
