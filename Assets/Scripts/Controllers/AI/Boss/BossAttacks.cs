using System.Collections;
using System.Linq;
using UnityEngine;

public class BossAttacks : MonoBehaviour {
	public BossStatsSO Stats;
	[SerializeField] public LayerMask EnemyLayer;
	[SerializeField] private WeaponStats _weaponData;

	public void FlashSlam(MovementDirection currentDirection, BossAttackType attackType) {
		GetComponentsInChildren<FlashOnAttack>().Where(e => e.Direction == currentDirection).Where(e => e.AttackType == attackType).ToList().ForEach(obj => {
			if (obj.TryGetComponent<FlashOnAttack>(out FlashOnAttack flash)) {
				StartCoroutine(flash.FlashSlamAttack());
			}
		});
	}
		public void FlashRoar(MovementDirection currentDirection, BossAttackType attackType) {
		GetComponentsInChildren<FlashOnAttack>().Where(e => e.Direction == currentDirection).Where(e => e.AttackType == attackType).ToList().ForEach(obj => {
			if (obj.TryGetComponent<FlashOnAttack>(out FlashOnAttack flash)) {
				StartCoroutine(flash.FlashRoarAttack());
			}
		});
	}

	public void Attack(MovementDirection currentDirection, BossAttackType attackType) {
		GetComponentsInChildren<BossAttackHitbox>().Where(e => e != null).Where(e => e.Direction == currentDirection).Where(e => e.AttackType == attackType).ToList().ForEach(e => e.GetUniqueObjectsInCollider().ForEach(obj => {
			if (obj.TryGetComponent<HealthController>(out HealthController opponent)) {
				opponent.TakeDamage(_weaponData.WeaponData.Damage);
			}
		}));

	}
}
