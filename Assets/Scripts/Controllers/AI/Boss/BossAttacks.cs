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
	// public Boss Boss;
	public BossAttackHitbox Hitbox;

	//TODO:
	//rename the component to calculate health
	// add enum to check attack and add a where statement
	public void Attack(MovementDirection currentDirection, BossAttackType attackType) {
		Logger.Log("Slam", currentDirection);
		GetComponentsInChildren<BossAttackHitbox>().Where(e => e != null).Where(e => e.Direction == currentDirection).Where(e => e.AttackType == attackType).ToList().ForEach(e => e.GetUniqueObjectsInCollider().ForEach(obj => {
			if (obj.TryGetComponent<HealthController>(out HealthController opponent)) {
				opponent.TakeDamage(_weaponData.WeaponData.Damage);
			}
		}));

	}
}
