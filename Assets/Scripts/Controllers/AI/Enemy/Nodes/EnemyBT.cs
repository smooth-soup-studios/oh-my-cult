using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;


public class EnemyBT : EnemyBehaviourTree {
	public Transform[] Waypoints;
	public static float Speed = 2f;
	public static float FovRange = 30f;
	public static float AttackRange = 20f;
	public static GameObject Target = null;
	public static Vector3 SearchLocation = Vector3.zero;

	private Vector2 _oldMovement;

	private new void Awake() {
		base.Awake();
		Agent.updateRotation = false;
		Agent.updateUpAxis = false;
	}
	protected new void Update() {
		base.Update();
		if (Movement != _oldMovement) {
			RotateHitboxOnMove(Movement);
			_oldMovement = Movement;
		}
	}

	protected override Node SetupTree() {
		Node root = new Selector(new List<Node>
		{
			 new Sequence(new List<Node>
			{
				new CheckEnemyInAttackRange(transform),
				new TaskAttack(transform),
			}),
			new Sequence(new List<Node>
			{
				new CheckEnemyInRange(transform),
				new TaskGoToTarget(transform),
			}),
			new Sequence(new List<Node>
			{
				new CheckLastKnownLocation(transform),
				new TaskSearchLastKnownLocation(transform),
			}),
			new TaskPatrol( Waypoints),
		});

		return root;
	}

	private void RotateHitboxOnMove(Vector2 movement) {
		Transform HitContainer = GetComponentInChildren<WeaponHitbox>().transform.parent;
		float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
		HitContainer.transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	private void OnDrawGizmos() {
		if (transform == null)
			return;
		Gizmos.DrawWireSphere(transform.position, FovRange);
	}
}
