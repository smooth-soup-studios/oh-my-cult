using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomTrigger : MonoBehaviour, ISaveable {

	[field: SerializeField, Header("Object information")] public string ObjectId { get; private set; }

	[SerializeField] private List<GameObject> _borders;
	[SerializeField] private List<GameObject> _enemies;
	[SerializeField] private bool _isCleared;

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && _enemies.Count > 0) {
			foreach (GameObject border in _borders) {
				border.SetActive(true);
				EventBus.Instance.TriggerEvent(EventType.INTERACT_TOGGLE, false);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Enemy") {
			_enemies.Remove(other.gameObject);
			if (_enemies.Count <= 0) {
				UnlockArea();
			}
		}
	}

	private void Start() {
		if (_isCleared) {
			foreach (GameObject enemy in _enemies) {
				EventBus.Instance.TriggerEvent(EventType.DEATH, enemy);
				_enemies.Remove(enemy);
			}
		}
	}

	private void UnlockArea() {
		foreach (GameObject border in _borders) {
			border.SetActive(false);
			_isCleared = true;
		}
		if (EventBus.Instance){
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
