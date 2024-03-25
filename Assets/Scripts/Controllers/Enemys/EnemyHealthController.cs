using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour {
	float _maxHealth = 100;
	float _currentHealth;
	// Start is called before the first frame update
	void Start() {
		_currentHealth = _maxHealth;
	}

	public void TakeDamage(float _damage) {
		_currentHealth -= _damage;
		Debug.Log(_currentHealth);
		if (_currentHealth <= 0) {
			Destroy(gameObject);
		}
	}
}
