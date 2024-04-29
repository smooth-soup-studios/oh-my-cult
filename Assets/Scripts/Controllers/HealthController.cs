using System;
using System.Collections;
using AK.Wwise;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Event = AK.Wwise.Event;

public class HealthController : MonoBehaviour, ISaveable {
	private string _logname = "Health controller";
	[SerializeField] float _maxHealth = 100;
	float _currentHealth;
	[SerializeField] Event _actorDamaged;
	[SerializeField] Event _lowHealth;
	[SerializeField] RTPC _healthValue;
	bool _isLowHealthEventPosted;

	void Awake() {
		_currentHealth = _maxHealth;
		_healthValue.SetValue(gameObject, _currentHealth);
	}

	private void Start() {
		if (_currentHealth <= 0) {
			EventBus.Instance.TriggerEvent<GameObject>(EventType.DEATH, gameObject);
			Logger.Log(_logname, $"The {name} is dead!");
		}
	}

	void Update() {
		CheckLowHealth(() => {
			if (_isLowHealthEventPosted) {
				return;
			}
			_lowHealth.Post(gameObject);
			_isLowHealthEventPosted = true;
		});
	}

	void CheckLowHealth(Action callback) {
		switch (_currentHealth) {
			case <= 50 when !_isLowHealthEventPosted:
				callback();
				break;
			case > 50:
				_isLowHealthEventPosted = false;
				break;
		}
	}

	public void TakeDamage(float damage) {
		_currentHealth -= damage;

		_actorDamaged.Post(gameObject);
		StartCoroutine(FlashRed());
		Logger.Log(_logname, $"The {name} took {damage} damage!");
		if (_currentHealth <= 0) {
			EventBus.Instance.TriggerEvent<GameObject>(EventType.DEATH, gameObject);
			Logger.Log(_logname, $"The {name} is dead!");
		}

		if (!gameObject.CompareTag("Player")) return;
		_healthValue.SetValue(gameObject, _currentHealth);
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
