using Managers;
using UnityEngine;
namespace Controllers {
public class MovementController : MonoBehaviour {
	Rigidbody2D _rigidbody;
	[SerializeField] float speed;


	void Start() {
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update() {
		Move();
	}

	void Move() {
		var input = InputManager.Instance.Move;
		_rigidbody.velocity = input * speed;
	}

}
}