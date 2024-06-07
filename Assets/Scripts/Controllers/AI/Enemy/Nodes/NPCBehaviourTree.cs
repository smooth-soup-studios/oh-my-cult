using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;


public class NPCBehaviourTree : BaseBehaviourTree {
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
			// Friendly NPC Behavior
			new Sequence(new List<Node>
			{
				new CheckActorType(ActorType.NPC),
				new CheckEnemyInRange(),
				new TaskChangeToEnemy()
			}),

			new Sequence(new List<Node>
			{
				new CheckAgressionDisabled(),
				new CheckEnemyInAttackRange(),
				new TaskAttack(),
			}),
			new Sequence(new List<Node>
			{
				new CheckAgressionDisabled(),
				new CheckEnemyInRange(),
				new TaskGoToTarget(),
			}),
			new Sequence(new List<Node>
			{
				new CheckLastKnownLocation(),
				new TaskSearchLastKnownLocation(transform),
			}),
			new Sequence(new List<Node>
			{
				new CheckAgentHasWaypoints(Waypoints),
				new TaskPatrol(Waypoints)
			}),
			new TaskRandomWalk(),
		});

		return root;
	}

	private void RotateHitboxOnMove(Vector2 movement) {
		Transform HitContainer = GetComponentInChildren<WeaponHitbox>().transform.parent;
		float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
		HitContainer.transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
