using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachine : MonoBehaviour, ISaveable{
	[SerializeField] private TextMeshProUGUI _stateText;
	[SerializeField] public Weapon Weapon; 
  public bool HasPlaytestKey = false;
  public float Health = 1f;
  
	private BaseState _currentState;
	private List<BaseState> _states;

	void Start() {
		_states = new List<BaseState> {
			new PlayerIdleState("Idle", this),
			new PlayerMoveState("Move", this),
			new PlayerDashState("Dash", this),
			new PlayerAttackState("Attack", this)
		};
		SwitchState("Idle");
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

	public void LoadData(GameData data) {
		Vector3 savedPos = data.PlayerData.PlayerPosition;
		if(savedPos != Vector3.zero){
			transform.position = savedPos;
		}
	}

	public void SaveData(GameData data) {
		data.PlayerData.PlayerPosition = transform.position;
		data.PlayerData.SceneName = SceneManager.GetActiveScene().name;
	}
}