using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathState : BaseState {


	public PlayerDeathState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}


	public override void EnterState() {
		//SceneManager.LoadScene(SceneDefs.EndingScreen);
			StateMachine.PlayerAnimator.Play("PlayerDeath");
	}

	public override void UpdateState() {

	}

	public override void ExitState() {

	}
}