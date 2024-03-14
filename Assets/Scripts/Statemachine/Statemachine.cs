using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour {
	private BaseState _currentState;
	private List<BaseState> _states;


	void Start() {
		_states = new List<BaseState>{
			new PlayerIdleState("Idle", this)
		};
		_currentState = _states.FirstOrDefault(x => x.Name == "Idle");
	}

	public void SwitchState(string name) {
		_currentState.ExitState();
		_currentState = _states.FirstOrDefault(x => x.Name == name);
		_currentState.EnterState();
	}

	void Update() {
		_currentState.UpdateState();
	}
}
