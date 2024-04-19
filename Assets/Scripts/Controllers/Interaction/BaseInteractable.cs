using System;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, ISaveable {
	[field: SerializeField, Header("Object information")] public string ObjectId { get; private set; }

	[Header("General Settings")]
	public float InteractionRange = 15;
	[SerializeField] protected bool AutoTrigger = false;

	public abstract void Interact(GameObject interactor);
	public abstract void OnSelect();
	public abstract void OnDeselect();

	protected virtual void Update() {
		if (AutoTrigger) {
			Transform playerPos = FindAnyObjectByType<StateMachine>().transform;
			if (Vector3.Distance(playerPos.position, transform.position) <= InteractionRange) {
				Interact(playerPos.gameObject);
			}
		}
	}

	protected virtual void OnDrawGizmos() {
		Gizmos.color = Color.red;
		if (AutoTrigger) {
			Gizmos.color = Color.magenta;
		}
		Gizmos.DrawWireSphere(transform.position, InteractionRange);
	}

	public virtual void LoadData(GameData data) {
		Logger.LogWarning("Test", name + " Loaded");
	}

	public virtual void SaveData(GameData data) {
		Logger.LogWarning("Test", name + " Saved");

	}

	private void OnValidate() {
		// Generates an unique ID based on the name & position of the gameobject.
#if UNITY_EDITOR
		ObjectId = $"{name}-{Vector3.SqrMagnitude(transform.position)}";
		UnityEditor.EditorUtility.SetDirty(this);
#endif
	}
}