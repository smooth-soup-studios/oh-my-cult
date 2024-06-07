using UnityEngine;
using UnityEngine.Events;

public class EventInteractable : BaseInteractable {
	[Header("Event Settings")]
	[SerializeField] private UnityEvent<GameObject> _event = new();
	[SerializeField] SpriteRenderer _spriteRenderer;
	[SerializeField] private TooltipController _tooltipController;

	public override void Interact(GameObject interactor) {
		_event.Invoke(interactor);
		if (_tooltipController) {
			_tooltipController.Select();
			_tooltipController.HideTooltip();
		}
		base.Interact(interactor);
	}

	public override void OnDeselect() {
		if (_spriteRenderer) {
			_spriteRenderer.color = Color.white;
		}
		if (_tooltipController) {
			_tooltipController.HideTooltip();
		}
	}

	public override void OnSelect() {
		if (_spriteRenderer) {
			_spriteRenderer.color = Color.green;
		}
		if (_tooltipController) {
			_tooltipController.ShowTooltip();
		}
	}

	public override void OnSelectWhileDisabled() {
		if (_spriteRenderer) {
			_spriteRenderer.color = Color.red;
		}
	}
}