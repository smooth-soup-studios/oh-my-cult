using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Statemachine : MonoBehaviour {
	private State _currentState;
	private List<State> _states;


	void Start() {
		_states = new List<State>{
			new Idle("Idle", this)
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
