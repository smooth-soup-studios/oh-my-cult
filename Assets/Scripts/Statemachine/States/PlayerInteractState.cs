using System.Linq;
using UnityEngine;
public class PlayerInteractState : BaseState {
	private bool _interacting = true;
	public PlayerInteractState(string name, StateMachine stateMachine) : base(name, stateMachine) { }


	public override void EnterState() {
		BaseInteractable interactable = StateMachine.PlayerInteractor.GetCurrentInteractable();
		if (interactable != null) {
			interactable.Interact(StateMachine.gameObject);
		}
		_interacting = false;
	}
	public override void UpdateState() {
		if (!_interacting) {
			StateMachine.SwitchState("Move");
		}
	}
	public override void ExitState() {
	}


}