using System.Collections;
using UnityEngine;

public class PlayerHeavyAttackState : BaseState {
	private bool _using = true;
	private InteractableItem _currentItem;

	public PlayerHeavyAttackState(string name, StateMachine stateMachine) : base(name, stateMachine) { }

	public override void EnterState() {
		if (StateMachine.PlayerInventory.GetSelectedItem() == null || !StateMachine.PlayerInventory.GetSelectedItem().InvData.ItemPrefab.TryGetComponent<InteractableItem>(out _currentItem)) {
			_using = false;
			return;
		}
		StateMachine.StartCoroutine(WaitForCooldown());
		_currentItem.SecondaryAction(StateMachine.gameObject);
		if (_currentItem.ItemData.InvData.AnimationSet != null) {
			StateMachine.PlayerAnimator.Play("Player" + _currentItem.ItemData.InvData.AnimationSet, MovementDirection);
		}
	}

	public override void UpdateState() {
		StateMachine.HandleMovement(StateMachine.BaseSpeed * StateMachine.SpeedModifier * Time.deltaTime * Movement.normalized);
		if (!_using) {
			StateMachine.SwitchState("Move");
		}

	}

	public override void ExitState() { }

	private IEnumerator WaitForCooldown() {
		_using = true;
		yield return new WaitForSecondsRealtime(_currentItem.UsageCooldown);
		_using = false;
	}
}
