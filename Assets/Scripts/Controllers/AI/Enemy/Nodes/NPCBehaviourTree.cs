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
			// Update animator
			new TaskUpdateAnimator(),

			// Friendly NPC Behavior
			new Sequence(new List<Node>
			{
				new CheckActorType(ActorType.NPC),
				new CheckPlayerInRange(),
				new TaskChangeToEnemy()
			}),

			// Enemy - Attack when in range
			new Sequence(new List<Node>
			{
				new CheckAgressionDisabled(),
				new CheckTargetInAttackRange(),
				new TaskAttack(),
			}),

			// Enemy - Move to target when in range but not attack range
			new Sequence(new List<Node>
			{
				new CheckAgressionDisabled(),
				new CheckPlayerInRange(),
				new CheckTargetUnobstructed(),
				new TaskGoToTarget(),
			}),
			// Enemy - Search for target when lost
			new Sequence(new List<Node>
			{
				new CheckLastKnownLocation(),
				new TaskSearchLastKnownLocation(transform),
			}),

			// Enemy - Patrol when no target is available and waypoints have been set
			new Sequence(new List<Node>
			{
				// new CheckActorType(ActorType.MeleeEnemy),
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
