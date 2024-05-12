using UnityEngine;
using UnityEngine.AI;
namespace BehaviorTree {


	public abstract class EnemyBehaviourTree : MonoBehaviour {
		private Node _root = null;
		[HideInInspector] public Vector2 Movement;
		[HideInInspector] public NavMeshAgent Agent;
		[HideInInspector] public Animator EnemyAnimator;
		[HideInInspector] public float AttackCounter = -0.04f;
		[HideInInspector] public float Speed = 2f;
		[HideInInspector] public float FovRange = 30f;
		[HideInInspector] public float AttackRange = 20f;
		[HideInInspector] public  GameObject Target = null;
		[HideInInspector] public  Vector3 SearchLocation = Vector3.zero;

		protected void Awake() {
			Agent = GetComponent<NavMeshAgent>();
			EnemyAnimator = GetComponent<Animator>();
		}
		protected void Start() {
			_root = SetupTree();
		}
		private void Update() {
			if (_root != null) {
				_root.Evaluate(this);
			}
		}
		protected abstract Node SetupTree();
	}

}