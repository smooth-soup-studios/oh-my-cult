
using UnityEngine;

public abstract class BaseState {
	public string Name;
	protected StateMachine StateMachine;
	protected Vector2 Movement = Vector2.zero;


	protected BaseState(string name, StateMachine stateMachine) {
		Name = name;
		StateMachine = stateMachine;
		EventBus.Instance.Subscribe<Vector2>(EventType.MOVEMENT, move => Movement = move);
	}

	public abstract void EnterState();
	public abstract void UpdateState();
	public abstract void ExitState();
}