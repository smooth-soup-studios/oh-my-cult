using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BossAttacks : MonoBehaviour {
	public BossStatsSO Stats;
	[SerializeField] public LayerMask EnemyLayer;
	[SerializeField] private WeaponStats _weaponData;
	public FlashOnAttack SlamFlashOnAttackLeft;

	public FlashOnAttack SlamFlashOnAttackRight;
	public FlashOnAttack RoarFlashOnAttack;
	public FlashOnAttack ChargeFlashOnAttackLeft;
	public FlashOnAttack ChargeFlashOnAttackRight;

	//TODO:
	//rename the component to calculate health
	public void SlamAttack() {

		SlamFlashOnAttackLeft.StartCoroutine(SlamFlashOnAttackLeft.FlashSlamAttack());
		SlamFlashOnAttackRight.StartCoroutine(SlamFlashOnAttackRight.FlashSlamAttack());
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
		RoarFlashOnAttack.StartCoroutine(RoarFlashOnAttack.FlashRoarAttack());
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

		ChargeFlashOnAttackLeft.StartCoroutine(ChargeFlashOnAttackLeft.FlashChargeAttack());
		ChargeFlashOnAttackRight.StartCoroutine(ChargeFlashOnAttackRight.FlashChargeAttack());
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


}
