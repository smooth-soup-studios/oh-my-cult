using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour {
	public float AttackSpeed = 0.5f;
	[SerializeField] private float _damage = 10f;
	[SerializeField] private float _heavyDamage = 15f;
	[SerializeField] private float _attackRange = 10f;
	[SerializeField] private Transform _attackPoint;
	[SerializeField] private LayerMask _enemyLayer;


	public void DeafaultAttack() {
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);
		foreach (Collider2D Enemy in hitEnemies) {
			if (Enemy.TryGetComponent<EnemyHealthController>(out EnemyHealthController opponent)) {
				opponent.TakeDamage(_damage);
			}
		}
	}

	public void HeavyAttack() {
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);
		foreach (Collider2D Enemy in hitEnemies) {
			if (Enemy.TryGetComponent<EnemyHealthController>(out EnemyHealthController opponent)) {
				opponent.TakeDamage(_heavyDamage);
			}
		}
	}
	private void OnDrawGizmos() {
		if (_attackPoint == null)
			return;
		Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
	}
}
