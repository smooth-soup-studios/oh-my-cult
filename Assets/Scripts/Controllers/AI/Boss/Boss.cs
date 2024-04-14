using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour {
	public BossStatsSO Stats;
	[SerializeField] public BossAttacks BossAttacks;
	[SerializeField] public AudioClip RoarSoundClip;
	public Transform Player;
	public AnimationManager Animator;
	public BossBaseState CurrentState;
	public List<BossBaseState> States;
	[HideInInspector] public int StateCounter = 0;
	[HideInInspector] public bool Enemy = false;

	// Start is called before the first frame update

	void Start() {
		Animator = new(GetComponent<Animator>());

		States = new List<BossBaseState>{
			new BossSlamAttack(this, "Slam"),
			new BossChargeState(this, "Charge"),
			new BossRoarState(this, "Roar"),
			new BossIdleState(this, "Idle")
		};
		SwitchState("Idle");
	}
	public void SwitchState(string name) {
		CurrentState?.ExitState();
		CurrentState = States.FirstOrDefault(x => x.Name == name);
		CurrentState?.EnterState();
	}

	// Update is called once per frame
	void Update() {
		CurrentState?.UpdateState();
	}

	private void OnDrawGizmos() {
		if (transform == null)
			return;
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, Stats.SlamRange);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, Stats.RoarRange);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, Stats.ChargeRange);
	}

	public void CheckForPlayer() {
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, Stats.ChargeRange, BossAttacks.EnemyLayer);

		if (hitEnemies.Length >= 1) {
			Enemy = true;
		}
	}
}