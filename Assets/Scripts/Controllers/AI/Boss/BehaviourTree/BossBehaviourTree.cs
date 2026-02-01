using System;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace BossBehaviour {
	public abstract class BossBehaviourTree : BaseBehaviourTree {
		private Node _root = null;

		[Header("Settings")]
		public new BossStatsSO Stats;


		private new void Awake() {
			base.Awake();
			Agent.updateRotation = false;
			Agent.updateUpAxis = false;
		}

		protected new void Update() {
			base.Update();
		}

		protected override Node SetupTree() {
			Node root = new Selector(new List<Node>
			{
			new Sequence(new List<Node>
			{
				new ChangeBehaviour()
			})

		});
			return root;
		}
	}

}

