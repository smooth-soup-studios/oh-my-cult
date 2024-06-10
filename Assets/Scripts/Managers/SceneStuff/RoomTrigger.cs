using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour, ISaveable {

	[field: SerializeField, Header("Object information")] public string ObjectId { get; private set; }
	[Header("Settings")]
	[SerializeField] private bool _addEnemiesDynamic = true;
	[SerializeField] private List<GameObject> _borders;
	[SerializeField] private List<GameObject> _enemies;
	[SerializeField] private bool _isCleared;

	// Ensures that Unlock can only be called once either the player has entered the room,
	// or the enemies have been detected by the physics engine. OnTrigger lifecycle is a bit annoying.
	private bool _initialized = false;


	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Enemy") && _addEnemiesDynamic) {
			_enemies.Add(other.gameObject);
		}

		if (other.CompareTag("Player") && _enemies.Count > 0) {
			foreach (GameObject border in _borders) {
				border.SetActive(true);
				EventBus.Instance.TriggerEvent(EventType.INTERACT_TOGGLE, false);
			}
		}
		_initialized = true;
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Enemy")) {
			_enemies.Remove(other.gameObject);
		}
	}

	private void Start() {
		if (_isCleared) {
			List<GameObject> toremove = new();
			foreach (GameObject enemy in _enemies) {
				EventBus.Instance.TriggerEvent(EventType.DEATH, enemy);
				toremove.Add(enemy);
			}
			_enemies.RemoveAll(x => toremove.Contains(x));
		}
	}

	private void Update() {
		if (_enemies.Count <= 0 && _initialized && !_isCleared) {
			UnlockArea();
		}
	}

	private void UnlockArea() {
		foreach (GameObject border in _borders) {
			border.SetActive(false);
			_isCleared = true;
		}
		if (EventBus.Instance) {
			EventBus.Instance.TriggerEvent(EventType.INTERACT_TOGGLE, true);
		}
	}

	protected void OnValidate() {
		// Generates an unique ID based on the name & position of the gameobject.
#if UNITY_EDITOR
		ObjectId = $"{name}-{Vector3.SqrMagnitude(transform.position)}";
		UnityEditor.EditorUtility.SetDirty(this);
#endif
	}

	public virtual void LoadData(GameData data) {
		if (data.SceneData.ArbitraryTriggers.ContainsKey($"{ObjectId}-isCleared")) {
			data.SceneData.ArbitraryTriggers.TryGetValue($"{ObjectId}-isCleared", out _isCleared);
		}
	}

	public virtual void SaveData(GameData data) {
		data.SceneData.ArbitraryTriggers[$"{ObjectId}-isCleared"] = _isCleared;
	}
}
