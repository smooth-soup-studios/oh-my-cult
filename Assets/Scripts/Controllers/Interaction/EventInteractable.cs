using UnityEngine;
using UnityEngine.Events;

public class EventIneractable : BaseInteractable {
	[Header("Event Settings")]
	[SerializeField] private UnityEvent<GameObject> _event = new();
	[SerializeField] private bool _isSingleUse = false;
	private bool _hasBeenUsed = false;

	private void Start() {
		if (_isSingleUse && _hasBeenUsed) {
			gameObject.SetActive(false);
		}
	}


	public override void Interact(GameObject interactor) {
		_event.Invoke(interactor);
		_hasBeenUsed = true;
	}

	public override void OnDeselect() {
	}

	public override void OnSelect() {
	}

	public override void LoadData(GameData data) {
		if (data.SceneData.InteractionData.ContainsKey(ObjectId + "HasBeenUsed")) {
			data.SceneData.InteractionData.TryGetValue(ObjectId + "HasBeenUsed", out _hasBeenUsed);
		}
	}

	public override void SaveData(GameData data) {
		data.SceneData.InteractionData[ObjectId + "HasBeenUsed"] = _hasBeenUsed;
	}

}