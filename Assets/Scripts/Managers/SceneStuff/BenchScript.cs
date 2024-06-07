using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchScript : MonoBehaviour
{
	private Animator _animator;
	private BoxCollider2D _collider2D;

	private void Awake() {
		_animator = GetComponent<Animator>();
		_collider2D = GetComponent<BoxCollider2D>();
		EventBus.Instance.Subscribe<(GameObject target, GameObject hitter)>(EventType.HIT, e => { if (e.target == gameObject) Break(); });
	}

	private void Break(){
		_collider2D.enabled = false;
		_animator.SetBool("IsBroken", true);
	}
}
