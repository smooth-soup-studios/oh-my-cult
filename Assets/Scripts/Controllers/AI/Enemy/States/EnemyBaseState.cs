using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class EnemyBaseState {
	protected  Enemy Enemy;
	public  string Name;

	public EnemyBaseState(Enemy enemy, string name) {
		this.Enemy = enemy;
		this.Name = name;
	}
	public virtual void EnterState(){
	}
	public virtual void UpdateState(){}
	public virtual void ExitState(){}
}
