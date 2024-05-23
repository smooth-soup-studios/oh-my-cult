using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour {
	private List<GameObject> _objectsInCollider = new();
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.GetComponentsInChildren<WeaponHitbox>().Contains(this))
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

	public List<GameObject> GetUniqueObjectsInCollider() {
		_objectsInCollider = GetObjectsInCollider().Distinct().ToList();
		return _objectsInCollider;
	}
}