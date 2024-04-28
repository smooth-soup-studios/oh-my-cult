using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Event = AK.Wwise.Event;

public class HealthController : MonoBehaviour, ISaveable {
	private string _logname = "Health controller";
	[SerializeField] float _maxHealth = 100;
	float _currentHealth;
	[SerializeField] Event _actorDamaged;

	void Awake() {
		_currentHealth = _maxHealth;
	}
	private void Start() {
		if (_currentHealth <= 0) {
			EventBus.Instance.TriggerEvent<GameObject>(EventType.DEATH, gameObject);
			Logger.Log(_logname, $"The {name} is dead!");
		}
	}

	public void TakeDamage(float _damage) {
		_currentHealth -= _damage;

		_actorDamaged.Post(gameObject);
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

	public void LoadData(GameData data) {
		if (data.SceneData.HealthValues.ContainsKey(gameObject.name)) {
			data.SceneData.HealthValues.TryGetValue(gameObject.name, out _currentHealth);
		}
	}

	public void SaveData(GameData data) {
		data.SceneData.HealthValues[gameObject.name] = _currentHealth;
	}
}
