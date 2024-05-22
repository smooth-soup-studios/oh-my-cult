using System.Linq;
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
	public Boss Boss;
	public BossSlamHitbox Hitbox;

	//TODO:
	//rename the component to calculate health
	public void SlamAttack() {

		SlamFlashOnAttackLeft.StartCoroutine(SlamFlashOnAttackLeft.FlashSlamAttack());
		SlamFlashOnAttackRight.StartCoroutine(SlamFlashOnAttackRight.FlashSlamAttack());
		// GetComponentsInChildren<BossSlamHitbox>().Where(e => e != null).ToList().ForEach(e => e.GetObjectsInCollider().ForEach(obj => {
		// 	if (obj.TryGetComponent<HealthController>(out HealthController opponent)) {
		// 		opponent.TakeDamage(_weaponData.WeaponData.Damage);
		// 	}
		// }));

		switch (Boss.direction) {
			case Boss.Direction.LEFT:
				Hitbox.HitboxDown(); 
				break;
			case Boss.Direction.UP:
				Logger.Log("", "");
				break;
			case Boss.Direction.RIGHT:
				Logger.Log("", "");
				break;
			case Boss.Direction.DOWN:
				Logger.Log("", "");
				break;

		}
	}

	//TODO:
	//rename the component to calculate health

	public void RoarAttack() {
		RoarFlashOnAttack.StartCoroutine(RoarFlashOnAttack.FlashRoarAttack());
		try {
			GetComponentsInChildren<BossRoarHitbox>().Where(e => e != null).ToList().ForEach(e => e.GetObjectsInCollider().ForEach(obj => {
				if (obj.TryGetComponent<HealthController>(out HealthController opponent)) {
					opponent.TakeDamage(_weaponData.WeaponData.Damage);
				}
			}));
		}
		catch (System.Exception) {
		}
	}

	// //TODO:
	// //rename the component to calculate health
	public void ChargeAttack() {

		ChargeFlashOnAttackLeft.StartCoroutine(ChargeFlashOnAttackLeft.FlashChargeAttack());
		ChargeFlashOnAttackRight.StartCoroutine(ChargeFlashOnAttackRight.FlashChargeAttack());

		GetComponentsInChildren<BossSlamHitbox>().Where(e => e != null).ToList().ForEach(e => e.GetObjectsInCollider().ForEach(obj => {
			if (obj.TryGetComponent<HealthController>(out HealthController opponent)) {
				opponent.TakeDamage(_weaponData.WeaponData.Damage);
			}
		}));
	}


}
