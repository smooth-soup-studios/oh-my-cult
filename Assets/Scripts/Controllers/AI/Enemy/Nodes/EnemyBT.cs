using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;


public class EnemyBT : BaseBehaviourTree {
	public Transform[] Waypoints;
	private Vector2 _oldMovement;

	[Header("Debug")]
	public bool DisableAgression = false;

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
				new CheckEnemyType(),
				new CheckEnemyInRange(transform),
				new TaskChangeToEnemy(transform)

			}),

			new Sequence(new List<Node>
			{
				new CheckAgressionDisabled(transform),
				new CheckEnemyInAttackRange(transform),
				new TaskAttack(transform),
			}),
			new Sequence(new List<Node>
			{
				new CheckAgressionDisabled(transform),
				new CheckEnemyInRange(transform),
				new TaskGoToTarget(transform),
			}),
			new Sequence(new List<Node>
			{
				new CheckLastKnownLocation(transform),
				new TaskSearchLastKnownLocation(transform),
			}),
			new Sequence(new List<Node>
			{
				new CheckAgentHasWaypoints(transform),
				new TaskPatrol( Waypoints)
			}),
			new TaskRandomWalk(transform),
		});

		return root;
	}

	private void RotateHitboxOnMove(Vector2 movement) {
		Transform HitContainer = GetComponentInChildren<WeaponHitbox>().transform.parent;
		float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
		HitContainer.transform.rotation = Quaternion.Euler(0, 0, angle);
	}


}
