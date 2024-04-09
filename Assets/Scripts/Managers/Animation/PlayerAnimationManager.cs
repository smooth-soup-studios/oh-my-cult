using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationManager {
	private string _logname = "PlayerAnimator";
	private Animator _animator;

	[Header("Debug logging")]
	[SerializeField] private bool _animationDebugLogging = false;

	public PlayerAnimationManager(Animator animator) {
		_animator = animator;
	}
	public PlayerAnimationManager(Animator animator, bool debug) : this(animator) {
		_animationDebugLogging = debug;
	}

	public void Play(string animationName, MovementDirection direction) {
		string animation = animationName + "-" + ConvertMovementToAnimation(direction);
		_animator.Play(animation);
		if (_animationDebugLogging) {
			Logger.Log(_logname, $"Playing animation {animation} on {_animator.gameObject.name}");
		}
	}

	public void Play(string animationName) {
		_animator.Play(animationName);
		if (_animationDebugLogging) {
			Logger.Log(_logname, $"Playing animation {animationName} on {_animator.gameObject.name}");
		}
	}

	public float GetAnimationDuration() {
		return _animator.GetCurrentAnimatorStateInfo(0).length;
	}

	public void Stop() {
		_animator.StopPlayback();
		if (_animationDebugLogging) {
			Logger.Log(_logname, $"Stopped animation playback on {_animator.gameObject.name}");
		}
	}

	private string ConvertMovementToAnimation(MovementDirection direction) {
		return direction switch {
			MovementDirection.UP => "Up",
			MovementDirection.DOWN => "Down",
			MovementDirection.LEFT => "Side",
			MovementDirection.RIGHT => "Side",
			_ => "",
		};
	}
}