public abstract class BaseState {
	public string Name;
	protected StateMachine StateMachine;

	protected BaseState(string name, StateMachine stateMachine) {
		Name = name;
		StateMachine = stateMachine;
	}

	public abstract void EnterState();
	public abstract void UpdateState();
	public abstract void ExitState();
}