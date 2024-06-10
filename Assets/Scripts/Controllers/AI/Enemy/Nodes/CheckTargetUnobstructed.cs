using System.Collections;
using BehaviorTree;
using UnityEngine;

public class CheckTargetUnobstructed : Node {
	protected LayerMask IgnoreThisMask;

	protected GameObject OldTarget;
	protected bool CanCheckAgain = false;
	protected bool LastResult = true;

	public CheckTargetUnobstructed() {
		IgnoreThisMask = ~(1 << LayerMask.NameToLayer("Enemy") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
	}

	public CheckTargetUnobstructed(LayerMask ignoreThisMask) {
		IgnoreThisMask = ignoreThisMask;
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {

		GameObject target = tree.Target;
		if (target != null) {
			if (IsTargetUnObstructed(tree, target)) {
				return NodeState.SUCCESS;
			}
		}

		// Reset if target should not be visible
		tree.SearchLocation = Vector3.zero;
		tree.Target = null;
		return NodeState.FAILURE;
	}


	protected bool IsTargetUnObstructed(MonoBehaviour source, GameObject target) {
		if (target == OldTarget && !CanCheckAgain) {
			return LastResult;
		}
		source.StartCoroutine(DelayCheck(0.5f));
		OldTarget = target;


		RaycastHit2D hit = Physics2D.Raycast(source.transform.position, target.transform.position - source.transform.position, Vector3.Distance(target.transform.position, source.transform.position), IgnoreThisMask);
		if (hit && hit.collider.gameObject == target) {
			Debug.DrawRay(source.transform.position, target.transform.position - source.transform.position, Color.green, 2f);
			LastResult = true;
			return true;
		}
		Debug.DrawRay(source.transform.position, target.transform.position - source.transform.position, Color.grey, 2f);
		LastResult = false;
		return false;
	}

	IEnumerator DelayCheck(float delay) {
		CanCheckAgain = false;
		yield return new WaitForSeconds(delay);
		CanCheckAgain = true;
	}
}