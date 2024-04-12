using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	private string _logname = "Boss";
	[SerializeField] private Animator _animator;
	[SerializeField] private int _attackCooldown;
	private bool _isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (!_isAttacking) {
			StartCoroutine(AttackTimer());
		}
    }

	IEnumerator AttackTimer() {
		PerformAttack();
		yield return new WaitForSecondsRealtime(_attackCooldown);
	}

	private void PerformAttack() {
	}

	private void Charge() {
		Logger.Log(_logname, $"The {name} is using {System.Reflection.MethodBase.GetCurrentMethod().Name}");
	}

	private void Slam() {
		Logger.Log(_logname, $"The {name} is using {System.Reflection.MethodBase.GetCurrentMethod().Name}");
	}

	private void Roar() {
		Logger.Log(_logname, $"The {name} is using {System.Reflection.MethodBase.GetCurrentMethod().Name}");
	}
}
