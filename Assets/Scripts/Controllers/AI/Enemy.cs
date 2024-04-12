using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public EnemyBaseState CurrentState;
	[SerializeField] public Weapon Weapon;
	public Vector2 TargetPosition;
	public Vector2 StartingPosition;
	public Rigidbody2D Rb;
	private string _logname = "AI controller";
	public LayerMask GroundLayer, PlayerLayer;
	public bool PlayerDetect = false;
	private List<EnemyBaseState> _states;
	public EnemyStatsSO Stats;
	public bool AttackMelee = false;
	[SerializeField] public Transform[] Route;
	public Transform Player;
	public NavMeshAgent Agent;
	public int RouteIndex = 0;
	[SerializeField] public float RestTime;
	public bool IsResting = false;
	public bool EndReached = false;


	void Start() {
		_states = new List<EnemyBaseState>{
			new EnemyPatrolState(this, "Patrol"),
			new PlayerDetectedState(this, "Detected"),
			new EnemyChargeState(this, "Charge"),
			new EnemyAttackState(this, "Attack")
		};
		SwitchState("Patrol");
		Rb = GetComponent<Rigidbody2D>();
		Agent = GetComponent<NavMeshAgent>();
	}
	public void SwitchState(string name) {
		CurrentState?.ExitState();
		CurrentState = _states.FirstOrDefault(x => x.Name == name);
		CurrentState?.EnterState();
	}

	private void Update() {
		CurrentState.UpdateState();

	}

	public void EnemyMovement() {
		if (!PlayerDetect) {
			if (transform.position != Route[RouteIndex].position) {
				// Move towards next point.
				transform.position = Vector3.MoveTowards(
				transform.position,
				Route[RouteIndex].position,
				Stats.Speed * Time.deltaTime);
			}
			else {
				if (!IsResting) {
					Logger.Log(name, "Rest");
					StartCoroutine(RestAtPoint());
				}
			}
		}
	}


	private void OnDrawGizmos() {
		if (transform == null)
			return;
		Gizmos.DrawWireSphere(transform.position, Stats.ObstacleDetectDistance);
		Gizmos.DrawWireSphere(transform.position, Stats.PlayerDetectDistance);
		Gizmos.DrawWireSphere(transform.position, Stats.MeleeDetectDistance);
	}
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
	}
	IEnumerator RestAtPoint() {
		IsResting = true;
		Logger.Log(name, "Rest");
		yield return new WaitForSeconds(RestTime);

		if (RouteIndex == Route.Length - 1) {
			//RouteIndex = 0;
		}
		else {
			RouteIndex++;
		}

		IsResting = false;
	}
}