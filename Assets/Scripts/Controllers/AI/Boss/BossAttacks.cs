using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossAttacks : MonoBehaviour {
	public BossStatsSO Stats;
	[SerializeField] public LayerMask EnemyLayer;
	[SerializeField] private WeaponStats _weaponData;

	public void FlashSlam(MovementDirection currentDirection, BossAttackType attackType) {
		GetComponentsInChildren<FlashOnAttack>().Where(e => e.Direction == currentDirection).Where(e => e.AttackType == attackType).ToList().ForEach(obj => {
			if (obj.TryGetComponent(out FlashOnAttack flash)) {
				StartCoroutine(flash.FlashSlamAttack());
			}
		});
	}
	public void FlashRoar(MovementDirection currentDirection, BossAttackType attackType) {
		GetComponentsInChildren<FlashOnAttack>().Where(e => e.Direction == currentDirection).Where(e => e.AttackType == attackType).ToList().ForEach(obj => {
			if (obj.TryGetComponent(out FlashOnAttack flash)) {
				StartCoroutine(flash.FlashRoarAttack());
			}
		});
	}

	public void Attack(MovementDirection currentDirection, BossAttackType attackType) {
		List<GameObject> Target = new();
		GetComponentsInChildren<BossAttackHitbox>().Where(e => e != null).Where(e => e.Direction == currentDirection).Where(e => e.AttackType == attackType).ToList().ForEach(e => Target.AddRange(e.GetUniqueObjectsInCollider()));
		Target.Distinct().ToList().ForEach(obj => {
			EventBus.Instance.TriggerEvent(EventType.HIT, (obj, gameObject));
			if (obj.TryGetComponent(out HealthController opponent)) {
				opponent.TakeDamage(_weaponData.WeaponData.Damage);

				if (ScreenShakeManager.Instance) {
					ScreenShakeManager.Instance.GetOrAddLayer(VibrationLayerNames.ReceivePrimaryDamage, true).SetShakeThenStop(5f, 1.25f, 1.5f);
				}
				if (VibrationManager.Instance) {
					VibrationManager.Instance.GetOrAddLayer(VibrationLayerNames.ReceivePrimaryDamage, true).SetShakeThenStop(1f, .75f, .5f);
				}
			}
		});

	}
}
