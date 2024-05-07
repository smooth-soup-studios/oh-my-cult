using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BossAttacks : MonoBehaviour {
	public BossStatsSO Stats;
	[SerializeField] public LayerMask EnemyLayer;
	[SerializeField] private WeaponStats _weaponData;



	//TODO:
	//rename the component to calculate health
	public void SlamAttack() {

		// Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, Stats.SlamRange, EnemyLayer);
		// foreach (Collider2D Enemy in hitEnemies) {
		// 	if (Enemy.TryGetComponent<EnemyHealthController>(out EnemyHealthController opponent)) {
		// 		opponent.TakeDamage(Stats.SlamDamage);
		// 	}
		// }

		try {
			GetComponentInChildren<BossSlamHitboxLeft>().GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController opponent)) {
					opponent.TakeDamage(_weaponData.WeaponData.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
				try {
			GetComponentInChildren<BossSlamHitboxRight>().GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController opponent)) {
					opponent.TakeDamage(_weaponData.WeaponData.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
	}

	//TODO:
	//rename the component to calculate health

	public void RoarAttack() {
	// 	Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, Stats.RoarRange, EnemyLayer);
	// 	foreach (Collider2D Enemy in hitEnemies) {
	// 		if (Enemy.TryGetComponent<EnemyHealthController>(out EnemyHealthController opponent)) {
	// 			opponent.TakeDamage(Stats.RoarAttack);
	// 		}
	// 	}
				try {
			GetComponentInChildren<BossRoarHitbox>().GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController opponent)) {
					opponent.TakeDamage(_weaponData.WeaponData.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
	}

	// //TODO:
	// //rename the component to calculate health
	public void ChargeAttack() {
		// Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, Stats.ChargeRange, EnemyLayer);
		// foreach (Collider2D Enemy in hitEnemies) {
		// 	if (Enemy.TryGetComponent<EnemyHealthController>(out EnemyHealthController opponent)) {
		// 		opponent.TakeDamage(Stats.ChargeAttack);
		// 	}
		// }
						try {
			GetComponentInChildren<BossRoarHitbox>().GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<EnemyHealthController>(out EnemyHealthController opponent)) {
					opponent.TakeDamage(_weaponData.WeaponData.Damage);
				}
			});
		}
		catch (System.Exception) {
		}
	}


}
