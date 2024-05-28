using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour, ISaveable {
	public BossStatsSO Stats;
	[SerializeField] public BossAttacks BossAttacks;
	[SerializeField] public AudioClip RoarSoundClip;
	public Transform Player;
	public BossBaseState CurrentState;
	public List<BossBaseState> States;
	[HideInInspector] public int StateCounter = 0;
	[HideInInspector] public bool Enemy = false;
	[HideInInspector] public bool Charge;

	private bool _isAlive = true;
	public Animator BossAnimation;
	[HideInInspector] public Vector2 Movement;
	public MovementDirection Direction;
	private float _xMin = -1;
	private float _xMax = 1;
	private float _yMin = -1;
	private float _yMax = 1;

	void Start() {
		if (!_isAlive) {
			gameObject.SetActive(false);
		}
		BossAnimation = GetComponent<Animator>();
		EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, obj => {
			if (obj == gameObject) {
				_isAlive = false;
				gameObject.SetActive(false);
				SceneManager.LoadScene(SceneDefs.EndingScreen);
			}
		});

		Player = GameObject.FindGameObjectWithTag("Player").transform;


		States = new List<BossBaseState>{
			new BossSlamAttack(this, "Slam"),
			new BossChargeState(this, "Charge"),
			new BossRoarState(this, "Roar"),
			new BossMoveState(this,"Move"),
			new BossIdleState(this, "Idle"),
			new BossChargeAttack(this, "ChargeAttack")
		};
		SwitchState("Idle");
	}
	public void SwitchState(string name) {
		CurrentState?.ExitState();
		CurrentState = States.FirstOrDefault(x => x.Name == name);
		CurrentState?.EnterState();
	}

	Vector2 _oldMove;
	void Update() {
		CurrentState?.UpdateState();
		if (Movement != _oldMove) {
			RotateHitboxOnMove(Movement);
			_oldMove = Movement;
		}

	}

	private void RotateHitboxOnMove(Vector2 movement) {
		movement = RoundVector(movement);
		// Transform HitContainer = GetComponentInChildren<BossRoarHitbox>().transform.parent;
		// Quaternion currentRotation = HitContainer.transform.rotation;

		float x = Mathf.Clamp(movement.x, -1, 1);
		float y = Mathf.Clamp(movement.y, -1, 1);

		if (x > 0 && y < 1) { //L
							  // currentRotation = Quaternion.Euler(0, 0, 90);
			Direction = MovementDirection.RIGHT;


		}
		else if (x < 0 && y < 1) { //R
								   // currentRotation = Quaternion.Euler(0, 0, -90);
			Direction = MovementDirection.LEFT;
		}
		else if (y > 0 && x < 1) { //U
								   // currentRotation = Quaternion.Euler(0, 0, 180);
			Direction = MovementDirection.UP;

		}
		else if (y < 0 && x < 1) { //D
								   // currentRotation = Quaternion.Euler(0, 0, 0);
			Direction = MovementDirection.DOWN;
		}

		// HitContainer.transform.rotation = currentRotation;
		Logger.Log("Rotation", Direction);
		Logger.Log($"{y}", x);


	}

	private Vector2 RoundVector(Vector2 vector) {
		return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
	}

	public void CheckForPlayer() {
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, Stats.ChargeRange, BossAttacks.EnemyLayer);

		if (hitEnemies.Length >= 1) {
			Enemy = true;
			Charge = false;
		}
	}

	public IEnumerator FlashRed() {
		GetComponent<SpriteRenderer>().color = Color.magenta;
		yield return new WaitForSeconds(0.5f);
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	public List<WeightedStates> WeightedValues;
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


	private void OnDrawGizmos() {
		if (transform == null)
			return;
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, Stats.ChargeRange);
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