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
	public AnimationManager Animator;
	public BossBaseState CurrentState;
	public List<BossBaseState> States;
	[HideInInspector] public int StateCounter = 0;
	[HideInInspector] public bool Enemy = false;
	[HideInInspector] public bool Charge;

	private bool _isAlive = true;



	void Start() {
		if (!_isAlive) {
			gameObject.SetActive(false);
		}

		Animator = new(GetComponent<Animator>());

		EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, obj => {
			if (obj == gameObject) {
				_isAlive = false;
				gameObject.SetActive(false);
				SceneManager.LoadScene("OutroScene");

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

	// Update is called once per frame
	void Update() {
		CurrentState?.UpdateState();
	}

	private void OnDrawGizmos() {
		if (transform == null)
			return;
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, Stats.ChargeRange);
	}

	public void CheckForPlayer() {
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, Stats.ChargeRange, BossAttacks.EnemyLayer);

		if (hitEnemies.Length >= 1) {
			Enemy = true;
			Charge = false;
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

	public IEnumerator FlashRed() {
		GetComponent<SpriteRenderer>().color = Color.magenta;
		yield return new WaitForSeconds(0.5f);
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	public List<WeightedStates> WeightedValues;
	public int GetRendomValue(List<WeightedStates> weightedValuesList) {
		int output = 0;

		var totalWeight = 0;
		foreach (var entry in weightedValuesList) {
			totalWeight += entry.Weight;
		}
		var rndWeightValue = Random.Range(1, totalWeight + 1);

		var processedWeight = 0;
		foreach (var entry in weightedValuesList) {
			processedWeight += entry.Weight;
			if (rndWeightValue <= processedWeight) {
				output = entry.Value;
				break;
			}
		}
		return output;
	}
}