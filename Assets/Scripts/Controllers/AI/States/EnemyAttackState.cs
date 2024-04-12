using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState {
	public EnemyAttackState(Enemy enemy, string name) : base(enemy, name) { }

	private bool _attackCooldown = false ;
	public override void EnterState() {
		Logger.Log(Name, "Attack");

		Enemy.StartCoroutine(AttackSpeed());
		Enemy.StartCoroutine(AttackCooldown());
		if (_attackCooldown) {
			Enemy.Weapon.DefaultAttack();
		}
	}

	public override void UpdateState() {
		if (Enemy.AttackMelee == false) {
			Enemy.SwitchState("Charge");
		}
		if(Enemy.PlayerDetect == false ){
			Enemy.SwitchState("Patrol");
		}

	}
	public override void ExitState() {
		Enemy.AttackMelee = false;
	}
	private IEnumerator AttackSpeed() {
		yield return new WaitForSecondsRealtime(Enemy.Weapon.AttackSpeed);
	}

	private IEnumerator AttackCooldown() {
		yield return new WaitForSecondsRealtime(Enemy.Stats.AttackCooldown);
		_attackCooldown = true;
	}
}