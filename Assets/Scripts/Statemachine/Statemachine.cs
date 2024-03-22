using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StateMachine : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI _stateText;
	[SerializeField] public Weapon Weapon; 
	private BaseState _currentState;
	private List<BaseState> _states;

	void Start() {
		_states = new List<BaseState> {
			new PlayerIdleState("Idle", this),
			new PlayerMoveState("Move", this),
			new PlayerDashState("Dash", this),
			new PlayerAttackState("Attack", this)
		};
		SwitchState("Idle");
	}

	void Update() {
		_currentState.UpdateState();
	}

	public void SwitchState(string name) {
		_currentState?.ExitState();
		_currentState = _states.FirstOrDefault(x => x.Name == name);
		_stateText?.SetText(_currentState.Name);
		_currentState?.EnterState();
	}

}