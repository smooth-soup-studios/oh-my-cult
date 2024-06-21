using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(SpriteRenderer))]
public class TorchPickupPoint : BaseInteractable {
	private SpriteRenderer _spriteRenderer;
	private Light2D _light;
	protected TooltipController TooltipController;

	void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_light = GetComponent<Light2D>();
		TooltipController = GetComponent<TooltipController>();
	}

	protected override void Update() {
		base.Update();
		_light.enabled = !HasBeenUsed;
	}

	public override void Interact(GameObject interactor) {
		TooltipController.HideTooltip();
		interactor.GetComponent<StateMachine>().HasTorch = true;
		base.Interact(interactor);

	}

	public override void OnDeselect() {
		_spriteRenderer.color = Color.white;
		TooltipController.HideTooltip();
	}

	public override void OnSelect() {
		_spriteRenderer.color = Color.green;
		TooltipController.ShowTooltip();
	}

	public override void LoadData(GameData data) {
		base.LoadData(data);
		if (data.SceneData.ArbitraryTriggers.ContainsKey($"{ObjectId}-TorchExists")) {
			data.SceneData.ArbitraryTriggers.TryGetValue($"{ObjectId}-TorchExists", out HasBeenUsed);
		}
	}

	public override void SaveData(GameData data) {
		base.SaveData(data);
		data.SceneData.ArbitraryTriggers[$"{ObjectId}-TorchExists"] = HasBeenUsed;
	}
}