using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour, ISaveable {
	public BossStatsSO Stats;
	public Animator BossAnimation;

	[SerializeField] public BossAttacks BossAttacks;
	[SerializeField] public AudioClip RoarSoundClip;
	public Transform Player;
	public BossBaseState CurrentState;
	public List<BossBaseState> States;
	public List<WeightedStates> WeightedValues;

	[HideInInspector] public bool Enemy = false;

	[HideInInspector] public Vector2 Movement;
	[HideInInspector] public bool WaitForWalking = true;
	public MovementDirection Direction;
	private bool _isAlive = true;
	private GameObject _target;
	private Vector2 _oldMove;
	public NavMeshAgent Agent { get; set; }



	void Start() {
		_target = GameObject.FindWithTag("Player");
		if (!_isAlive) {
			gameObject.SetActive(false);
		}
		BossAnimation = GetComponent<Animator>();
		EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, OnDeath);

		StartCoroutine(WaitForWalk());
		Player = GameObject.FindGameObjectWithTag("Player").transform;

		States = new List<BossBaseState>{
			new BossSlamAttack(this, "Slam"),
			new BossChargeState(this, "Charge"),
			new BossRoarState(this, "Roar"),
			new BossMoveState(this,"Move"),
			new BossIdleState(this, "Idle"),
			new BossChargeAttack(this, "ChargeAttack"),
			new BossDeathState(this, "Death")
		};
		SwitchState("Idle");
		Agent = GetComponent<NavMeshAgent>();
		Agent.updateRotation = false;
		Agent.updateUpAxis = false;
	}
	public void SwitchState(string name) {
		CurrentState?.ExitState();
		CurrentState = States.FirstOrDefault(x => x.Name == name);
		CurrentState?.EnterState();
	}

	void Update() {
		CurrentState?.UpdateState();
		if (Movement != _oldMove) {
			RotateHitboxOnMove(Movement);
			_oldMove = Movement;
		}

	}

	private void RotateHitboxOnMove(Vector2 movement) {
		movement = RoundVector(movement);

		float x = Mathf.Clamp(movement.x, -1, 1);
		float y = Mathf.Clamp(movement.y, -1, 1);

		if (x > 0 && y < 1) { //R
			Direction = MovementDirection.RIGHT;
		}
		else if (x < 0 && y < 1) { //L
			Direction = MovementDirection.LEFT;
		}
		else if (y > 0 && x < 1) { //U
			Direction = MovementDirection.UP;

		}
		else if (y < 0 && x < 1) { //D
			Direction = MovementDirection.DOWN;
		}
	}

	private Vector2 RoundVector(Vector2 vector) {
		return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
	}

	public void CheckForPlayer() {
		if (Vector2.Distance(transform.position, _target.transform.position) <= Stats.ChargeRange) {
			Enemy = true;
		}
	}


	public int GetRendomValue(List<WeightedStates> weightedValuesList) {
		int output = 0;

		int totalWeight = 0;
		foreach (WeightedStates entry in weightedValuesList) {
			totalWeight += entry.Weight;
		}
		int rndWeightValue = Random.Range(1, totalWeight + 1);

		int processedWeight = 0;
		foreach (WeightedStates entry in weightedValuesList) {
			processedWeight += entry.Weight;
			if (rndWeightValue <= processedWeight) {
				output = entry.Value;
				break;
			}
		}
		return output;
	}

	public IEnumerator WaitForWalk() {
		yield return new WaitForSeconds(4f);
		WaitForWalking = false;
	}

	private void OnDeath(GameObject objThatDied) {
		if (objThatDied == gameObject) {
			_isAlive = false;
			SwitchState("Death");
		}
	}

	public void LoadData(GameData data) {
		if (data.SceneData.ArbitraryTriggers.ContainsKey("BossDead")) {
			data.SceneData.ArbitraryTriggers.TryGetValue("BossDead", out _isAlive);
		}
	}

	public void SaveData(GameData data) {
		data.SceneData.ArbitraryTriggers["BossDead"] = isActiveAndEnabled;
	}



}