using System.Collections.Generic;

namespace BehaviorTree {
	public enum NodeState {
		RUNNING,
		SUCCESS,
		FAILURE
	}
	public class Node {
		protected NodeState State;
		public Node Parent;
		protected List<Node> Children = new();
		protected MovementDirection MovementDirection = MovementDirection.DOWN;
		

		private Dictionary<string, object> _dataContext = new();

		public Node() {
			Parent = null;
		}

		public Node(List<Node> children) {
			foreach (Node child in children) {
				_Attach(child);
			}
		}

		private void _Attach(Node node) {
			node.Parent = this;
			Children.Add(node);
		}
		public virtual NodeState Evaluate(EnemyBehaviourTree tree) => NodeState.FAILURE;

		public void SetData(string key, object value) {
			_dataContext[key] = value;
		}

		public object GetData(string key) {
			if (_dataContext.TryGetValue(key, out object value)) {
				return value;
			}
			Node node = Parent;
			while (node != null) {
				value = node.GetData(key);
				if (value != null) {
					return value;
				}
				node = node.Parent;
			}
			return null;
		}

		public bool ClearData(string key) {
			if (_dataContext.ContainsKey(key)) {
				_dataContext.Remove(key);
				return true;
			}
			Node node = Parent;
			while (node != null) {
				bool cleared = node.ClearData(key);
				if (cleared) {
					return true;
				}
				node = node.Parent;
			}
			return false;
		}
	}
}
