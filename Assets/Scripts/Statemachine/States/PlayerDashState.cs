using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : BaseState {
	public static readonly float DashCooldown = 1.5f;
	public static readonly float DashDuration = .25f;

	private float _dashSpeedModifier = 2.5f;
	private bool _dash = true;
	public PlayerDashState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}
	public override void EnterState() {
		StateMachine.StartCoroutine(DashTime());
		UIManager.Instance.DashStart = Time.time;
		StateMachine.EchoDashController.StartEcho();
		EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, "Dash");
		VibrationManager.Instance.GetOrAddLayer(VibrationLayerNames.Dash).SetShake(0, .1f);
	}

	public override void UpdateState() {
		StateMachine.HandleMovement(StateMachine.BaseSpeed * _dashSpeedModifier * StateMachine.SpeedModifier * Time.deltaTime * Movement.normalized);

		if (!_dash) {
			StateMachine.SwitchState("Move");
		}
	}

	public override void ExitState() {
		VibrationManager.Instance.GetOrAddLayer(VibrationLayerNames.Dash).SetShake(0, 0);
		StateMachine.EchoDashController.StopEcho();
	}


	IEnumerator DashTime() {
		_dash = true;
		yield return new WaitForSeconds(DashDuration);
		_dash = false;
	}
}
