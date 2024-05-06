using System.Collections;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour, ISaveable {
	private string _logname = "Health controller";
	[field: SerializeField, Header("Object information")] public string ObjectId { get; private set; }

	[Header("Settings")]
	[SerializeField] private bool _isInvulnerable = false;
	[SerializeField] float _maxHealth = 100;
	float _currentHealth;

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
		if (_isInvulnerable) {
			Logger.Log(_logname, $"The {name} took no damage becouse it is invulnerable!");
		}
		else {
			_currentHealth -= _damage;
			Logger.Log(_logname, $"The {name} took {_damage} damage!");
		}

		StartCoroutine(FlashRed());
		if (_currentHealth <= 0) {
			EventBus.Instance.TriggerEvent<GameObject>(EventType.DEATH, gameObject);
			Logger.Log(_logname, $"The {name} is dead!");
		}
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
		}
		if (data.ActorData.HealthValues.ContainsKey(ObjectId)) {
			data.ActorData.HealthValues.TryGetValue(ObjectId, out _currentHealth);
		}
	}

	public void SaveData(GameData data) {
		// Player UID changes between scenes so use dedicated ID
		if (gameObject.CompareTag("Player")) {
			data.PlayerData.Health = _currentHealth;
		}
		data.ActorData.HealthValues[ObjectId] = _currentHealth;
	}
}
