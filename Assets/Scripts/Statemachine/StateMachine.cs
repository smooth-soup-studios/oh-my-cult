using System.Collections.Generic;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(Inventory)), RequireComponent(typeof(EchoDashController), typeof(PlayerInteractionChecker))]
public class StateMachine : MonoBehaviour, ISaveable {
	private readonly string _logname = "StateMachine";

	[Header("Debug settings")]
	[SerializeField] private TextMeshProUGUI _stateText;
	[SerializeField] private bool _changeStateLogging = false;
	public AnimationManager PlayerAnimator;

	[Header("Movement Settings")]
	public float SpeedModifier = 5;
	public float BaseSpeed = 10;


	[Header("Tempforplaytest")]
	public WeaponHitbox WeaponHitbox;
	public GameObject HitContainer;

	[HideInInspector] public EchoDashController EchoDashController { get; private set; }
	[HideInInspector] public PlayerInteractionChecker PlayerInteractor { get; private set; }
	[HideInInspector] public Inventory PlayerInventory { get; private set; }

	[SerializeField] public AudioClip RunSoundClip;
	[SerializeField] public AudioClip AttackSoundClip;
	private BaseState _currentState;
	private List<BaseState> _states;

	[Header("Latest Door")]
	public int LatestDoor = 0;
	public bool HasDoorKey = false;

	void Awake() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			if (player != gameObject) {
				Destroy(gameObject);
			}
		}

		DontDestroyOnLoad(this);
	}

	void Start() {
		PlayerInventory = GetComponent<Inventory>();
		PlayerAnimator = new(GetComponent<Animator>());
		EchoDashController = GetComponent<EchoDashController>();
		PlayerInteractor = GetComponent<PlayerInteractionChecker>();

		EventBus.Instance.Subscribe<Vector2>(EventType.MOVEMENT, SwitchSpriteOnMove);
		EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, HandleDeath);

		_states = new List<BaseState> {
			new PlayerIdleState("Idle", this),
			new PlayerMoveState("Move", this),
			new PlayerDashState("Dash", this),
			new PlayerInteractState("Interact",this),
			new PlayerAttackState("Attack", this),
			new PlayerHeavyAttackState("HeavyAttack", this)
		};
		SwitchState("Idle");

	}

	void Update() {
		_currentState.UpdateState();

		UIManager.Instance.HasPlaytestKey = HasDoorKey;
		UIManager.Instance.Health = GetComponent<EnemyHealthController>().GetCurrentHealth();
	}

	public void SwitchState(string name) {
		if (_changeStateLogging) {
			Logger.Log(_logname, $"Exiting {_currentState.Name}");
		}

		_currentState?.ExitState();
		_currentState = _states.FirstOrDefault(x => x.Name == name);
		_stateText?.SetText(_currentState.Name);

		if (_changeStateLogging) {
			Logger.Log(_logname, $"Entering {_currentState.Name}");
		}

		_currentState?.EnterState();
	}

	public void HandleMovement(Vector2 movement) {
		Rigidbody2D _RigidBody = GetComponent<Rigidbody2D>();
		_RigidBody.MovePosition(_RigidBody.position + movement);
	}

	public void HandleDeath(GameObject gameObject) {
		if (gameObject == this.gameObject) {
			SwitchState("Death");
		}
	}

	private void SwitchSpriteOnMove(Vector2 movement) {
		if (movement != Vector2.zero) {
			// This should never default becouse it should never be V2.zero!
			// Basically just here to initialize the value so vscode doesn't kill me.
			MovementDirection currentDirection = MovementDirection.DOWN;

			if (movement.x > 0) {
				currentDirection = MovementDirection.RIGHT;
				GetComponent<SpriteRenderer>().flipX = true;
				HitContainer.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else if (movement.x < 0) {
				currentDirection = MovementDirection.LEFT;
				GetComponent<SpriteRenderer>().flipX = false;
				HitContainer.transform.rotation = Quaternion.Euler(0, 0, 180);

			}
			else if (movement.y > 0) {
				currentDirection = MovementDirection.UP;
				HitContainer.transform.rotation = Quaternion.Euler(0, 0, 90);

			}
			else if (movement.y < 0) {
				currentDirection = MovementDirection.DOWN;
				HitContainer.transform.rotation = Quaternion.Euler(0, 0, -90);

			}
			EventBus.Instance.TriggerEvent<MovementDirection>(EventType.MOVEMENT, currentDirection);
		}
	}

	public void LoadData(GameData data) {
		Vector3 savedPos = data.PlayerData.PlayerPosition;
		if (savedPos != Vector3.zero) {
			transform.position = savedPos;
		}
	}

	public void SaveData(GameData data) {
		data.PlayerData.PlayerPosition = transform.position;
		data.PlayerData.SceneName = SceneManager.GetActiveScene().name;
	}
}