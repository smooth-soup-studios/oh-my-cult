using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeathState : BossBaseState {
	public BossDeathState(Boss boss, string name) : base(boss, name) { }


	public override void EnterState() {
		Boss.BossAnimation.SetBool("IsDeath", true);
		Boss.StartCoroutine(Death());
	}
	public override void UpdateState() {

	}
	public override void ExitState() {

	}
	public IEnumerator Death() {
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(SceneDefs.OutroCutscene);
	}
}
