using UnityEngine;

public class PatrolPointHighlighter : MonoBehaviour {
	[SerializeField] private bool _visible;
	private void OnDrawGizmos() {
		if (_visible) {
			OnDrawGizmosSelected();
		}
	}

	private void OnDrawGizmosSelected() {

		for (int i = 0; i < transform.childCount; i++) {
			Transform child;
			Transform nextChild;
			child = transform.GetChild(i);
			nextChild = i == transform.childCount - 1 ? transform.GetChild(0) : transform.GetChild(i + 1);
			Debug.DrawLine(child.position, nextChild.position, Color.red);
		}
	}
}