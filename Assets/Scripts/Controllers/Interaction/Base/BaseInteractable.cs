using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, ISaveable {
	[field: SerializeField, Header("Object information")] public string ObjectId { get; private set; }

	[Header("General Settings")]
	public float InteractionRange = 15;
	[SerializeField] protected bool AutoTrigger = false;
	[SerializeField] protected bool SingleUse = false;
	protected bool HasBeenUsed = false;

	protected virtual void Start() {
		if (SingleUse && HasBeenUsed) {
			gameObject.SetActive(false);
		}
	}

	public virtual void Interact(GameObject interactor) {
		HasBeenUsed = true;
		if (SingleUse && HasBeenUsed) {
			gameObject.SetActive(false);
		}
	}

	protected virtual void Update() {
		if (AutoTrigger) {
			Transform playerPos = FindAnyObjectByType<StateMachine>().transform;
			if (Vector3.Distance(playerPos.position, transform.position) <= InteractionRange) {
				Interact(playerPos.gameObject);
				AutoTrigger = false;
				HasBeenUsed = true;
				if (SingleUse) {
					gameObject.SetActive(false);
				}
			}
		}
	}

	public abstract void OnSelect();
	public abstract void OnDeselect();
	public virtual void OnSelectWhileDisabled() {
		if (TryGetComponent<SpriteRenderer>(out SpriteRenderer renderMeister)) {
			renderMeister.color = Color.red;
		}
	}

	protected virtual void OnDrawGizmos() {
		Gizmos.color = Color.red;
		if (AutoTrigger) {
			Gizmos.color = Color.magenta;
		}
		Gizmos.DrawWireSphere(transform.position, InteractionRange);
	}


	protected void OnValidate() {
		// Generates an unique ID based on the name & position of the gameobject.
#if UNITY_EDITOR
		ObjectId = $"{name}-{Vector3.SqrMagnitude(transform.position)}";
		UnityEditor.EditorUtility.SetDirty(this);
#endif
	}

	public virtual void LoadData(GameData data) {
		if (data.SceneData.InteractionData.ContainsKey($"{ObjectId}-HasBeenUsed")) {
			data.SceneData.InteractionData.TryGetValue($"{ObjectId}-HasBeenUsed", out HasBeenUsed);
		}
	}

	public virtual void SaveData(GameData data) {
		data.SceneData.InteractionData[$"{ObjectId}-HasBeenUsed"] = HasBeenUsed;
	}
}