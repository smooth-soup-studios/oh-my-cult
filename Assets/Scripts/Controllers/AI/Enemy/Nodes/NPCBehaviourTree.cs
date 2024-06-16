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
			// All - Update animator
			new TaskUpdateAnimator(),

			// Friendly - Change to enemy when detecting player
			new Sequence(new List<Node>
			{
				new CheckActorType(ActorType.NPC),
				new CheckPlayerInRange(),
				new TaskChangeToEnemy()
			}),

			// Ranged - Retreat if enemy too close
			new Sequence(new List<Node>
			{
				new CheckActorType(ActorType.RangedEnemy),
				new CheckTargetInRetreatRange(transform),
				new TaskRetreatFromEnemy(transform)
			}),

			// Enemy - Attack when in range
			new Sequence(new List<Node>
			{
				new Inverter(new CheckActorType(ActorType.NPC)),
				new CheckAgressionDisabled(),
				new CheckTargetInAttackRange(),
				new TaskAttack(),
			}),

			// Ranged - Shoot
			new Sequence(new List<Node>
			{
				new CheckActorType(ActorType.RangedEnemy),
				new CheckPlayerInRange(),
				new TaskShoot()
			}),

			// Enemy - Move to target when in range but not attack range
			new Sequence(new List<Node>
			{
				new Inverter(new CheckActorType(ActorType.NPC)),
				new CheckAgressionDisabled(),
				new CheckPlayerInRange(),
				new CheckTargetUnobstructed(),
				new TaskGoToTarget(),
			}),
			// Enemy - Search for target when lost
			new Sequence(new List<Node>
			{
				new Inverter(new CheckActorType(ActorType.NPC)),
				new CheckLastKnownLocation(),
				new TaskSearchLastKnownLocation(transform),
			}),

			// All - Patrol when no target is available and waypoints have been set
			new Sequence(new List<Node>
			{
				new CheckAgentHasWaypoints(Waypoints),
				new TaskPatrol(Waypoints)
			}),

			// Enemy - Backup random walk
			new Sequence(new List<Node>
			{
				new Inverter(new CheckActorType(ActorType.NPC)),
				new TaskRandomWalk(),
			}),
		});

		return root;
	}

	private void RotateHitboxOnMove(Vector2 movement) {
		WeaponHitbox hitbox = GetComponentInChildren<WeaponHitbox>();
		if (hitbox) {
			Transform HitContainer = hitbox.transform.parent;
			float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
			HitContainer.transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}


}
