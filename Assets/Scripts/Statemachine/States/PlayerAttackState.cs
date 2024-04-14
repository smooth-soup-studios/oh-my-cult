using System.Collections;
using UnityEngine;
using Managers;

public class PlayerAttackState : BaseState {
	private bool _using = true;
	private InteractableItem _currentItem;

	public PlayerAttackState(string name, StateMachine stateMachine) : base(name, stateMachine) { }

	public override void EnterState() {
		if (StateMachine.PlayerInventory.GetSelectedItem() == null || !StateMachine.PlayerInventory.GetSelectedItem().ItemPrefab.TryGetComponent<InteractableItem>(out _currentItem)) {
			_using = false;
			return;
		}
		StateMachine.StartCoroutine(WaitForCooldown());
		_currentItem.PrimaryAction(StateMachine);
		if (_currentItem.ItemData.AnimationSet != null) {
			StateMachine.PlayerAnimator.Play("Player" + _currentItem.ItemData.AnimationSet, MovementDirection);
			SoundManager.Instance.PlayClip(StateMachine.AttackSoundClip, StateMachine.transform, 1f);
		}
	}

	public override void UpdateState() {
		StateMachine.transform.Translate(StateMachine.BaseSpeed * StateMachine.SpeedModifier * Time.deltaTime * Movement);
		if (!_using) {
			StateMachine.SwitchState("Idle");
		}

	}

	public override void ExitState() { }

	private IEnumerator WaitForCooldown() {
		_using = true;
		yield return new WaitForSecondsRealtime(_currentItem.UsageCooldown);
		_using = false;
	}
}
