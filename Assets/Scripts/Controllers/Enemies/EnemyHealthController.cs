using System.Collections;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour {
	private string _logname = "Health controller";
	[SerializeField] float _maxHealth = 100;
	float _currentHealth;

	void Start() {
		_currentHealth = _maxHealth;
	}

	public void TakeDamage(float _damage) {
		_currentHealth -= _damage;
		StartCoroutine(FlashRed());
		Logger.Log(_logname, $"The {name} took {_damage} damage!");
		if (_currentHealth <= 0) {
			EventBus.Instance.TriggerEvent<GameObject>(EventType.DEATH, gameObject);
			Logger.Log(_logname, $"The {name} is dead!");
		}
	}

	public float GetCurrentHealth() {
		return _currentHealth;
	}

	IEnumerator FlashRed() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.1f);
		GetComponent<SpriteRenderer>().color = Color.white;
	}
}
