using System;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine.AI;


public class EnemyBT : Tree {
	public UnityEngine.Transform[] Waypoints;
	public static float Speed = 2f;

	public static float FovRange = 30f;
	public static float AttackRange = 2f;
	public static NavMeshAgent Agent;


	private void Awake() {
		Agent = GetComponent<NavMeshAgent>();
		Agent.updateRotation = false;
		Agent.updateUpAxis = false;
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
			new TaskPatrol( Waypoints),
		});

		return root;
	}
}
