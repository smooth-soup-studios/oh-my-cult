using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachine : MonoBehaviour, ISaveable {
	private readonly string _logname = "StateMachine";
	[Header("Debug settings")]
	[SerializeField] private bool _changeStateLogging = false;

	[Header("Movement Settings")]
	public float SpeedModifier = 1;
	public float BaseSpeed = 10;


	[Header("Testing Refs")]
	[SerializeField] public Weapon Weapon;

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
	}

	void Update() {
		_currentState.UpdateState();
	}

	public void SwitchState(string name) {
		if (_changeStateLogging) {
			Logger.Log(_logname, $"Exiting {_currentState}");
		}

		_currentState?.ExitState();
		_currentState = _states.FirstOrDefault(x => x.Name == name);

		if (_changeStateLogging) {
			Logger.Log(_logname, $"Entering {_currentState}");
		}

		_currentState?.EnterState();
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