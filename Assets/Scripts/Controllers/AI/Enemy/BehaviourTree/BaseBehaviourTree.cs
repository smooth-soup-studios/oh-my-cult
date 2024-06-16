using UnityEngine;
using UnityEngine.AI;
namespace BehaviorTree {


	public abstract class BaseBehaviourTree : MonoBehaviour {
		private Node _root = null;

		[Header("Settings")]
		public ActorType ActorType;
		public ActorStats Stats;
		public bool RandomizeWaypoints = true;
		public Transform[] Waypoints;
		public GameObject NPCTransformPrefab;

		[Header("Debug")]
		public bool DisableAgression = false;


		// Non serialized publics
		public NavMeshAgent Agent { get; set; }
		public Animator ActorAnimator { get; set; }
		public Vector2 Movement { get; set; }
		public float AttackCounter { get; set; } = 0;
		public GameObject Target { get; set; } = null; //TF is this for? Should probably be replaced?
		public Vector3 SearchLocation { get; set; } = Vector3.zero;
		public bool HalfwayTransitionAnimation { get; set; } = false;


		protected void Awake() {
			EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, OnDeath);

			Agent = GetComponent<NavMeshAgent>();
			ActorAnimator = GetComponent<Animator>();

			Agent.speed = Stats.Speed;
		}

		protected void Start() {
			_root = SetupTree();
		}
		protected void Update() {
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

		private void OnDrawGizmos() {
			if (Stats) {
				Gizmos.color = Color.magenta;
				Gizmos.DrawWireSphere(transform.position, Stats.AttackRange);
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(transform.position, Stats.DetectionRange);
				Gizmos.color = Color.green;
				Gizmos.DrawWireSphere(transform.position, Stats.RetreatRange);
			}
		}

	}
}