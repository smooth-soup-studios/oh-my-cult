using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public EnemyBaseState CurrentState;
[SerializeField] public Weapon Weapon;

public Vector2 TargetPosition;
public Vector2 StartingPosition;

	public Rigidbody2D Rb;
	private string _logname = "AI controller";

	public Transform ObjectDetection;
	public LayerMask GroundLayer, PlayerLayer;


	public int  FacingRight = 1;

	public bool PlayerDetect = false;

	

	private List<EnemyBaseState> _states;
	public EnemyStatsSO Stats;
	public bool AttackMelee = false;


	void Start() {
		_states = new List<EnemyBaseState>{
			new EnemyPatrolState(this, "Patrol"),
			new PlayerDetectedState(this, "Detected"),
			new EnemyChargeState(this, "Charge"),
			new EnemyAttackState(this, "Attack")
		};
		SwitchState("Patrol");


		// CurrentState.EnterState();


	}
	public void SwitchState(string name) {
		CurrentState?.ExitState();
		CurrentState = _states.FirstOrDefault(x => x.Name == name);
		CurrentState?.EnterState();

		//switcht steeds naar basestate omdat hier logic mist
	}

	private void Update() {
		CurrentState.UpdateState();

	}

	// Update is called once per frame
	public void EnemyMovement() {
		if (!PlayerDetect) {
			if (FacingRight == 1) {
				Rb.velocity = new Vector2(Stats.Speed, Rb.velocity.y);
			}
			else {
				Rb.velocity = new Vector2(-Stats.Speed, Rb.velocity.y);
			}
		}
	}

	//use overlap circle all to check for obstacle or enemy use the diameter to set the range

	public void CheckForObstacles() {
		// RaycastHit2D hitObstatcle = Physics2D.Raycast(ObjectDetection.position, Vector2.right, RaycastDistance, GroundLayer);
		Collider2D[] hitObstacles = Physics2D.OverlapCircleAll(ObjectDetection.position, Stats.ObstacleDetectDistance, GroundLayer);

		if (hitObstacles.Length >= 1) {
			Logger.Log(_logname, $"Hit a wall");
			Rotate();
		}


	}

	private void OnDrawGizmos() {
		if (ObjectDetection == null)
			return;
		Gizmos.DrawWireSphere(ObjectDetection.position, Stats.ObstacleDetectDistance);
		Gizmos.DrawWireSphere(ObjectDetection.position, Stats.PlayerDetectDistance);
				Gizmos.DrawWireSphere(ObjectDetection.position, Stats.MeleeDetectDistance);
	}
	public void CheckForPlayer() {
		// RaycastHit2D hitPlayer = Physics2D.Raycast(ObjectDetection.position, Vector2.right, PlayerDetectDistance, PlayerLayer);
		Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(ObjectDetection.position, Stats.PlayerDetectDistance, PlayerLayer);

		if (hitPlayers.Length >= 1) {
			PlayerDetect = true;
		}
	}
		public void CheckForMeleeRange() {
			Collider2D[] hitMeleeTarget = Physics2D.OverlapCircleAll(ObjectDetection.position, Stats.MeleeDetectDistance, PlayerLayer);

		if (hitMeleeTarget.Length >= 1) {
			AttackMelee = true;
		}
	}




	void Rotate() {
		transform.Rotate(0, 180, 0);
		FacingRight = -FacingRight;
	}
}
