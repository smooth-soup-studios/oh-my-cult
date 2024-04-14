using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour {
	private string _logname = "Health controller";
	[SerializeField] float _maxHealth = 100;
	float _currentHealth;
	// Start is called before the first frame update
	void Start() {
		_currentHealth = _maxHealth;
	}

	public void TakeDamage(float _damage) {
		_currentHealth -= _damage;

		if (_currentHealth <= 0) {
			EventBus.Instance.TriggerEvent<GameObject>(EventType.DEATH, gameObject);
			Logger.Log(_logname, $"The {name} is dead!");
			Destroy(gameObject);
		}
	}
}
