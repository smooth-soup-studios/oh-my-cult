using System;
using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour, ISaveable {
	private string _logname = "Health controller";
	[field: SerializeField, Header("Object information")] public string ObjectId { get; private set; }

	[Header("Settings")]
	[SerializeField] private bool _isInvulnerable = false;
	[SerializeField] float _maxHealth = 100;
	float _currentHealth;
	[SerializeField] Event _actorDamaged;
	[SerializeField] Event _lowHealth;
	bool _isLowHealthEventPosted;

	void Awake() {
		_currentHealth = _maxHealth;
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
		if (_isInvulnerable) {
			Logger.Log(_logname, $"The {name} took no damage becouse it is invulnerable!");
		}
		else {
			_currentHealth -= damage;
			Logger.Log(_logname, $"The {name} took {damage} damage!");
		}
		StartCoroutine(FlashRed());

		if (_currentHealth <= 0) {
			EventBus.Instance.TriggerEvent<GameObject>(EventType.DEATH, gameObject);
			Logger.Log(_logname, $"The {name} is dead!");
		}

		if (!gameObject.CompareTag("Player")) return;
	}

	/// <summary>
	/// Adds health.
	/// </summary>
	/// <param name="health"></param>
	/// <returns>The current health after the operation.</returns>
	public float AddHealth(float health) {
		_currentHealth += health;
		if (_currentHealth > _maxHealth) {
			_currentHealth = _maxHealth;
		}

		return _currentHealth;
	}

	public float GetCurrentHealth() {
		return _currentHealth;
	}

	public bool IsAlive() {
		return _currentHealth > 0;
	}

	IEnumerator FlashRed() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.1f);
		GetComponent<SpriteRenderer>().color = Color.white;
	}


	private void OnValidate() {
		// Generates an unique ID based on the name & position of the gameobject.
#if UNITY_EDITOR
		ObjectId = $"{name}-{Vector3.SqrMagnitude(transform.position)}";
		UnityEditor.EditorUtility.SetDirty(this);
#endif
	}

	public void LoadData(GameData data) {
		if (gameObject.CompareTag("Player")) {
			_currentHealth = data.PlayerData.Health;
			_isInvulnerable = data.PlayerData.IsInvulnerable;
		}
		if (data.ActorData.HealthValues.ContainsKey(ObjectId)) {
			data.ActorData.HealthValues.TryGetValue(ObjectId, out _currentHealth);
		}
		if (data.ActorData.Arbitraryvalues.ContainsKey($"{ObjectId}-IsInvulnerable")) {
			data.ActorData.Arbitraryvalues.TryGetValue($"{ObjectId}-IsInvulnerable", out _isInvulnerable);
		}
	}

	public void SaveData(GameData data) {
		// Player UID changes between scenes so use dedicated ID
		if (gameObject.CompareTag("Player")) {
			data.PlayerData.Health = _currentHealth;
			data.PlayerData.IsInvulnerable = _isInvulnerable;
		}
		else {
			data.ActorData.HealthValues[ObjectId] = _currentHealth;
			data.ActorData.Arbitraryvalues[$"{ObjectId}-IsInvulnerable"] = _isInvulnerable;
		}
	}
}
