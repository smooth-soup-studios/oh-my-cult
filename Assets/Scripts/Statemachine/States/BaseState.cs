using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState
{
	public string Name;
	protected StateMachine Sm;

	public BaseState(string name, StateMachine sm){
		Name = name;
		Sm = sm;
	}

	public abstract void EnterState();
	public abstract void UpdateState();
	public abstract void ExitState();
}
