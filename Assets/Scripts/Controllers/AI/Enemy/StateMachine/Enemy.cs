using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ISaveable {

	[field: SerializeField, Header("Object information")] public string ObjectId { get; private set; }


	public EnemyBaseState CurrentState;
	[SerializeField] public Weapon Weapon;

	public Rigidbody2D Rb;
	public LayerMask GroundLayer, PlayerLayer;
	[HideInInspector] public bool PlayerDetect = false;
	private List<EnemyBaseState> _states;
	public EnemyStatsSO Stats;
	[HideInInspector] public bool AttackMelee = false;
	[SerializeField] public Transform[] Route;
	public Transform Player;
	[HideInInspector] public int RouteIndex = 0;
	[SerializeField] public float RestTime;
	[HideInInspector] public bool IsResting = false;
	[HideInInspector] public bool EndReached = false;
	public NavMeshAgent Agent;

//outdated
	void Start() {

		EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, obj => {
			if (obj == gameObject) {
				gameObject.SetActive(false);
			}
		});

		Player = GameObject.FindGameObjectWithTag("Player").transform;


		_states = new List<EnemyBaseState>{
			new EnemyPatrolState(this, "Patrol"),
			new PlayerDetectedState(this, "Detected"),
			new EnemyChargeState(this, "Charge"),
			new EnemyAttackState(this, "Attack")
		};
		SwitchState("Patrol");
		Rb = GetComponent<Rigidbody2D>();
		// Rb.isKinematic = true; // May need to be removed
		Agent = GetComponent<NavMeshAgent>();
		Agent.updateRotation = false;
		Agent.updateUpAxis = false;
	}
	public void SwitchState(string name) {
		CurrentState?.ExitState();
		CurrentState = _states.FirstOrDefault(x => x.Name == name);
		CurrentState?.EnterState();
	}

	private void Update() {
		CurrentState.UpdateState();
	}

	// private void OnDrawGizmos() {
	// 	if (transform == null)
	// 		return;
	// 	Gizmos.DrawWireSphere(transform.position, Stats.PlayerDetectDistance);
	// 	Gizmos.DrawWireSphere(transform.position, Stats.MeleeDetectDistance);
	// }
	public void CheckForPlayer() {
		Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, Stats.PlayerDetectDistance, PlayerLayer);

		if (hitPlayers.Length >= 1) {
			PlayerDetect = true;
		}
	}
	public void CheckForMeleeRange() {
		Collider2D[] hitMeleeTarget = Physics2D.OverlapCircleAll(transform.position, Stats.MeleeDetectDistance, PlayerLayer);

		if (hitMeleeTarget.Length >= 1) {
			AttackMelee = true;
		}
		else {
			AttackMelee = false;
		}
		Array.Clear(hitMeleeTarget, 0, hitMeleeTarget.Length);
	}

	private void OnValidate() {
		// Generates an unique ID based on the name & position of the gameobject.
#if UNITY_EDITOR
		ObjectId = $"{name}-{Vector3.SqrMagnitude(transform.position)}";
		UnityEditor.EditorUtility.SetDirty(this);
#endif
	}

	public void LoadData(GameData data) {
		if (data.ActorData.PositionValues.ContainsKey(ObjectId)) {
			data.ActorData.PositionValues.TryGetValue(ObjectId, out Vector3 savedPos);
			if (savedPos != Vector3.zero) {
				transform.position = savedPos;
			}
		}
	}

	public void SaveData(GameData data) {
		data.ActorData.PositionValues[ObjectId] = transform.position;
	}
}