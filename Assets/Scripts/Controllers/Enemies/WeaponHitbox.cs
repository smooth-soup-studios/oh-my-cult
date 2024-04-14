using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour {
	private List<GameObject> _objectsInCollider = new();
	private void OnTriggerEnter2D(Collider2D other) {
		Logger.Log("WeaponHitbox", $"{other.gameObject.name} entered the hitbox!");

		_objectsInCollider.Add(other.gameObject);
	}

	private void OnTriggerExit2D(Collider2D other) {
		Logger.Log("WeaponHitbox", $"{other.gameObject.name} exited the hitbox!");

		_objectsInCollider.Remove(other.gameObject);
	}

	public List<GameObject> GetObjectsInCollider() {
		_objectsInCollider = _objectsInCollider.Where(obj => obj != null).ToList();
		return _objectsInCollider;
	}
}