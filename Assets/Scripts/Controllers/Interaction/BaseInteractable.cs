using System;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, ISaveable {
	[SerializeField] public float InteractionRange;
	[SerializeField] protected bool AutoTrigger;

	public abstract void Interact(GameObject interactor);
	public abstract void OnSelect();
	public abstract void OnDeselect();

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
}