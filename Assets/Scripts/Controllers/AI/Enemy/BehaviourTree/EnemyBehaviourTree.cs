using UnityEngine;
namespace BehaviorTree {


	public abstract class EnemyBehaviourTree : MonoBehaviour {
		private Node _root = null;

		protected void Awake() {
			EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, OnDeath);
		}

		protected void Start() {
			_root = SetupTree();
		}
		private void Update() {
			if (_root != null) {
				_root.Evaluate(this);
			}
		}
		protected abstract Node SetupTree();

		protected void OnDeath(GameObject target) {
			if (target == gameObject) {
				gameObject.SetActive(false);
			}
		}
	}

}