using Managers;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class KeyPickupPoint : BaseInteractable {
	private bool _exists = true;
	private SpriteRenderer _spriteRenderer;

	void Awake() {
		if (!_exists) {
			Destroy(gameObject);
		}
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public override void Interact(GameObject interactor) {
		_exists = false;
		interactor.GetComponent<StateMachine>().HasDoorKey = true;

		UIManager.Instance.ShowDialogBox("You picked up a key!", "Use this to unlock the church door.", 5f);
		gameObject.SetActive(false);
	}

	public override void OnDeselect() {
		_spriteRenderer.color = Color.white;
	}

	public override void OnSelect() {
		_spriteRenderer.color = Color.green;
	}

	public override void LoadData(GameData data) {
		if (data.SceneData.ArbitraryTriggers.ContainsKey(ObjectId + "KeyExists")) {
			data.SceneData.ArbitraryTriggers.TryGetValue(ObjectId + "KeyExists", out _exists);
		}
	}

	public override void SaveData(GameData data) {
		data.SceneData.ArbitraryTriggers[ObjectId + "KeyExists"] = _exists;
	}
}