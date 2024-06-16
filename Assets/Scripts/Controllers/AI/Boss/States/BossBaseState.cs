public class BossBaseState {
	protected Boss Boss;
	public string Name;

	public BossBaseState(Boss boss, string name) {
		Boss = boss;
		Name = name;
	}
	public virtual void EnterState() { }
	public virtual void UpdateState() { }
	public virtual void ExitState() { }

}
