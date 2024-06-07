using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class KeyPickupPoint : BaseInteractable {
	private SpriteRenderer _spriteRenderer;

	void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public override void Interact(GameObject interactor) {

		interactor.GetComponent<StateMachine>().HasDoorKey = true;
		UIManager.Instance.ShowDialogBox("You picked up a key!", "Use this to unlock the church door.", 5f);
		base.Interact(interactor);

	}

	public override void OnDeselect() {
		_spriteRenderer.color = Color.white;
	}

	public override void OnSelect() {
		_spriteRenderer.color = Color.green;
	}

	public override void LoadData(GameData data) {
		base.LoadData(data);
		if (data.SceneData.ArbitraryTriggers.ContainsKey($"{ObjectId}-KeyExists")) {
			data.SceneData.ArbitraryTriggers.TryGetValue($"{ObjectId}-KeyExists", out HasBeenUsed);
		}
	}

	public override void SaveData(GameData data) {
		base.SaveData(data);
		data.SceneData.ArbitraryTriggers[$"{ObjectId}-KeyExists"] = HasBeenUsed;
	}
}