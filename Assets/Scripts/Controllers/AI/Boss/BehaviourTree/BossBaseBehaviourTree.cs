using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace BossBehaviourTree{
public abstract class BossBaseBehaviourTree : MonoBehaviour
{
    private BossNode _root = null;

    [Header("Settings")]
    public ActorStats BossStats; 

    public NavMeshAgent Agent {get; set;}
    public Animator Animator{get; set;}
    public Vector2 Movement {get; set;}
    public GameObject Target {get; set;}


	protected void Awake() {
        EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, OnDeath); 
		
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        Agent.speed = BossStats.Speed;
	}

	protected void Start() {
		_root = SetupTree(); 
	}

	protected void Update() {
		if (_root != null) {
            _root.Evaluate(this); 
        }
	}
	protected abstract BossNode SetupTree();
	protected void OnDeath(GameObject target) {
			if (target == gameObject) {
				gameObject.SetActive(false);
			}
		}
}
}
