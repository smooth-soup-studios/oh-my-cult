using UnityEngine;
namespace BehaviorTree {


	public abstract class EnemyBehaviourTree : MonoBehaviour {
		private Node _root = null;
		public Vector2 Movement;
		public float AttackCounter = -0.04f;

		protected void Start() {
			_root = SetupTree();
		}
		private void Update() {
			if (_root != null) {
				_root.Evaluate(this);
			}
		}
		protected abstract Node SetupTree();
	}

}