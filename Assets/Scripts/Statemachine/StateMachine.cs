using System.Collections.Generic;
using System.Linq;
using Managers;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(Inventory)), RequireComponent(typeof(EchoDashController), typeof(PlayerInteractionChecker))]
public class StateMachine : MonoBehaviour, ISaveable {
	private static readonly string _logname = "StateMachine";

	[Header("Debug settings")]
	[SerializeField] private TextMeshProUGUI _stateText;
	[SerializeField] private bool _changeStateLogging = false;
	[SerializeField] private bool _useEightPointHitbox = false;
	public AnimationManager PlayerAnimator;

	[Header("Movement Settings")]
	public float SpeedModifier = 5;
	public float BaseSpeed = 10;


	[Header("Tempforplaytest")]
	public WeaponHitbox WeaponHitbox;
	public GameObject HitContainer;

	private GameObject _torch;
	public bool HasTorch = false;

	[HideInInspector] public EchoDashController EchoDashController { get; private set; }
	[HideInInspector] public PlayerInteractionChecker PlayerInteractor { get; private set; }
	[HideInInspector] public Inventory PlayerInventory { get; private set; }
	private BaseState _currentState;
	private List<BaseState> _states;

	[Header("Latest Door")]
	public int LatestDoor = -1;
	public bool HasDoorKey = false;

	void Start() {
		PlayerInventory = GetComponent<Inventory>();
		PlayerAnimator = new(GetComponent<Animator>());
		EchoDashController = GetComponent<EchoDashController>();
		PlayerInteractor = GetComponent<PlayerInteractionChecker>();
		_torch = transform.Find("Jed Light 2D").gameObject;

		EventBus.Instance.Subscribe<Vector2>(EventType.MOVEMENT, SwitchSpriteOnMove);
		EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, HandleDeath);

		_states = new List<BaseState> {
			new PlayerIdleState("Idle", this),
			new PlayerMoveState("Move", this),
			new PlayerDashState("Dash", this),
			new PlayerInteractState("Interact",this),
			new PlayerAttackState("Attack", this),
			new PlayerHeavyAttackState("HeavyAttack", this),
			new PlayerDeathState("Death", this)
		};
		SwitchState("Move");

	}

	void Update() {
		_currentState.UpdateState();

		UIManager.Instance.HasPlaytestKey = HasDoorKey;
		UIManager.Instance.Health = GetComponent<HealthController>().GetCurrentHealth();

		_torch.SetActive(HasTorch);
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
			// These should never default becouse it should never be V2.zero!
			// Basically just here to initialize the value so vscode doesn't kill me.
			MovementDirection currentDirection = MovementDirection.DOWN;
			Quaternion currentRotation = HitContainer.transform.rotation;

			if (movement.x > 0) {
				currentDirection = MovementDirection.RIGHT;
				GetComponent<SpriteRenderer>().flipX = true;
				currentRotation = Quaternion.Euler(0, 0, 0);
			}
			else if (movement.x < 0) {
				currentDirection = MovementDirection.LEFT;
				GetComponent<SpriteRenderer>().flipX = false;
				currentRotation = Quaternion.Euler(0, 0, 180);
			}
			else if (movement.y > 0) {
				currentDirection = MovementDirection.UP;
				currentRotation = Quaternion.Euler(0, 0, 90);
			}
			else if (movement.y < 0) {
				currentDirection = MovementDirection.DOWN;
				currentRotation = Quaternion.Euler(0, 0, -90);
			}

			if (_useEightPointHitbox) {
				float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
				currentRotation = Quaternion.Euler(0, 0, angle);
			}
			HitContainer.transform.rotation = currentRotation;
			EventBus.Instance.TriggerEvent<MovementDirection>(EventType.MOVEMENT, currentDirection);
		}
	}

	public void LoadData(GameData data) {
		Vector3 savedPos = data.PlayerData.PlayerPosition;
		if (savedPos != Vector3.zero) {
			transform.position = savedPos;
		}
		HasDoorKey = data.PlayerData.HasDoorKey;
		HasTorch = data.PlayerData.HasTorch;
		LatestDoor = data.PlayerData.LatestDoor;
	}

	public void SaveData(GameData data) {
		data.PlayerData.LatestDoor = LatestDoor;
		data.PlayerData.HasDoorKey = HasDoorKey;
		data.PlayerData.HasTorch = HasTorch;
		data.PlayerData.PlayerPosition = transform.position;
		data.PlayerData.SceneName = SceneManager.GetActiveScene().name;
	}
}