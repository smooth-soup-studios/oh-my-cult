using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachine : MonoBehaviour, ISaveable {
	[Header("Movement Settings")]
	public float SpeedModifier = 1;
	public float BaseSpeed = 10;
	[Header("Testing Refs")]
	[SerializeField] private TextMeshProUGUI _stateText;
	[SerializeField] public Weapon Weapon;
	[SerializeField] public AudioClip RunSoundClip;
	[HideInInspector] public EchoDashController EchoDashController { get; private set; }
	[HideInInspector] public PlayerAnimationManager PlayerAnimator;
	private BaseState _currentState;
	private List<BaseState> _states;

	void Start() {
		PlayerAnimator = new(GetComponent<Animator>());
		EventBus.Instance.Subscribe<Vector2>(EventType.MOVEMENT, SwitchSpriteOnMove);

		_states = new List<BaseState> {
			new PlayerIdleState("Idle", this),
			new PlayerMoveState("Move", this),
			new PlayerDashState("Dash", this),
			new PlayerAttackState("Attack", this),
			new PlayerHeavyAttackState("HeavyAttack", this)
		};
		SwitchState("Idle");

		EchoDashController = GetComponent<EchoDashController>();
	}

	void Update() {
		_currentState.UpdateState();
	}

	public void SwitchState(string name) {
		_currentState?.ExitState();
		_currentState = _states.FirstOrDefault(x => x.Name == name);
		_stateText?.SetText(_currentState.Name);
		_currentState?.EnterState();
	}

	public void HandleMovement(Vector2 movement) {
		Rigidbody2D _RigidBody = GetComponent<Rigidbody2D>();
		_RigidBody.MovePosition(_RigidBody.position + movement);
	}

	private void SwitchSpriteOnMove(Vector2 movement) {
		// Default Position

		if (movement != Vector2.zero) {
			// This should never default becouse it should never be V2.zero!
			// Basically just here to initialize the value so vscode doesn't kill me.
			MovementDirection currentDirection = MovementDirection.DOWN;

			if (movement.x > 0) {
				currentDirection = MovementDirection.RIGHT;
				GetComponent<SpriteRenderer>().flipX = true;

			}
			else if (movement.x < 0) {
				currentDirection = MovementDirection.LEFT;
				GetComponent<SpriteRenderer>().flipX = false;
			}
			else if (movement.y > 0) {
				currentDirection = MovementDirection.UP;
			}
			else if (movement.y < 0) {
				currentDirection = MovementDirection.DOWN;
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