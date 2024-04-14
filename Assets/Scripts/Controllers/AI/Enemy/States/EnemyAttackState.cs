using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState {
	public EnemyAttackState(Enemy enemy, string name) : base(enemy, name) { }

	private bool _attackCooldown = true;
	private bool _switchState = false;
	public override void EnterState() {
		Enemy.StartCoroutine(AttackSpeed());
		Logger.Log(Name, "Attack");
		_switchState = false;
		Enemy.StartCoroutine(SwitchTime());
	}
	public override void UpdateState() {
		Enemy.CheckForMeleeRange();
		if (_attackCooldown) {
			Enemy.Weapon.DefaultAttack();
			Enemy.StartCoroutine(AttackCooldown());
		}

		if (Enemy.AttackMelee == false) {
			Enemy.SwitchState("Charge");
		}
	}
	public override void ExitState() {
		Enemy.AttackMelee = false;
	}
	private IEnumerator AttackSpeed() {
		yield return new WaitForSecondsRealtime(Enemy.Weapon.AttackSpeed);
	}

	private IEnumerator AttackCooldown() {
		_attackCooldown = false;
		yield return new WaitForSecondsRealtime(Enemy.Stats.AttackCooldown);
		_attackCooldown = true;

	}
	IEnumerator SwitchTime() {
		yield return new WaitForSecondsRealtime(Enemy.Stats.AttackCooldown);
		_switchState = true;
	}
}