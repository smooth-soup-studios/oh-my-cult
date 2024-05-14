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
			if (i == transform.childCount - 1) {
				continue;
			}
			else {
				Transform child = transform.GetChild(i);
				Transform nextChild = transform.GetChild(i + 1);
				Debug.DrawLine(child.position, nextChild.position, Color.red);
			}
		}
	}
}