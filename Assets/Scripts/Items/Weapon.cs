using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;


public class Weapon : MonoBehaviour {
	public float AttackSpeed = 0.5f;
	[SerializeField] private float _damage = 10f;
	[SerializeField] private float _heavyDamage = 15f;
	[SerializeField] private float _attackRange = 10f;
	[SerializeField] private Transform _attackPoint;
	[SerializeField] private LayerMask _enemyLayer;
	[SerializeField] private AudioClip _attackSoundClip;


	public void DefaultAttack() {
		//TODO: Replace line this is on with call to WWISE event
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);
		foreach (Collider2D Enemy in hitEnemies) {
			if (Enemy.TryGetComponent<HealthController>(out HealthController opponent)) {
				opponent.TakeDamage(_damage);
			}
		}
	}
	public void HeavyAttack() {
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);
		foreach (Collider2D Enemy in hitEnemies) {
			if (Enemy.TryGetComponent<HealthController>(out HealthController opponent)) {
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
