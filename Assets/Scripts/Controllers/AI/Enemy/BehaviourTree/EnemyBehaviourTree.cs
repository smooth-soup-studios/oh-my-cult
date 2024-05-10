using UnityEngine;
using UnityEngine.AI;
namespace BehaviorTree {


	public abstract class EnemyBehaviourTree : MonoBehaviour {
		private Node _root = null;
		[HideInInspector] public Vector2 Movement;
		[HideInInspector] public NavMeshAgent Agent;
		[HideInInspector] public Animator EnemyAnimator;
		[HideInInspector] public float AttackCounter = -0.04f;
		public WeaponItem EnemyWeapon;

		protected void Awake() {
			EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, OnDeath);

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

		protected void OnDeath(GameObject target) {
			if (target == gameObject) {
				gameObject.SetActive(false);
			}
		}
	}

}