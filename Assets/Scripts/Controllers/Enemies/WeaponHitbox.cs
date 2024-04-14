using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour {
	private List<GameObject> _objectsInCollider = new();
	private void OnTriggerEnter(Collider other) {
		_objectsInCollider.Add(other.gameObject);
	}

	private void OnTriggerExit(Collider other) {
		_objectsInCollider.Remove(other.gameObject);
	}

	public List<GameObject> GetObjectsInCollider() {
		return _objectsInCollider;
	}
}