using Managers;


public class BossDeathState : BossBaseState {
	public BossDeathState(Boss boss, string name) : base(boss, name) { }


	public override void EnterState() {
		Boss.BossAnimation.SetBool("IsDeath", true);
		GameManager.Instance.StartCoroutine(GameManager.Instance.DoTheBossCutsceneThingHereBecauseTheBossWouldGetDisabled());
	}
	public override void UpdateState() {

	}
	public override void ExitState() {

	}
}
