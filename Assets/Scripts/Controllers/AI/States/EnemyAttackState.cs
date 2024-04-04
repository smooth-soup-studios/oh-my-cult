using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState {
	public EnemyAttackState(Enemy enemy, string name) : base(enemy, name) { }

	public override void EnterState() {
		Logger.Log(Name, "Attack");
		Collider2D[] hitMeleeTarget = Physics2D.OverlapCircleAll(Enemy.ObjectDetection.position, Enemy.Stats.MeleeDetectDistance, Enemy.PlayerLayer);
		foreach (Collider2D hitCollider in hitMeleeTarget) {
			IDamageable damageable = hitCollider.GetComponent<IDamageable>();

			if (damageable != null) {
				Enemy.Weapon.Attack();
			}
		}
	}

	public override void UpdateState() {
		if (Enemy.PlayerDetect == false) {
			Enemy.SwitchState("Charge");
		}
	}
	public override void ExitState() {
		Enemy.AttackMelee = false;
	}
}
