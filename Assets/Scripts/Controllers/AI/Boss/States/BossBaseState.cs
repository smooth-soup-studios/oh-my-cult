using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState
{
	protected Boss Boss;
	public string Name;

	public BossBaseState(Boss boss, string name){
		this.Boss = boss;
		this.Name = name;
	}
	public virtual void EnterState(){
	}
	public virtual void UpdateState(){}
	public virtual void ExitState(){}

}
