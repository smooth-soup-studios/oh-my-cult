using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class State
{
	public string Name;
	protected Statemachine Sm;

	public State(string name, Statemachine sm){
		Name = name;
		Sm = sm;
	}

	public abstract void EnterState();
	public abstract void UpdateState();
	public abstract void ExitState();
}
