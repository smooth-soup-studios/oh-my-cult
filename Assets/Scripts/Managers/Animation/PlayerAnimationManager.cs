using UnityEngine;

public class PlayerAnimationManager {
	private string _logname = "PlayerAnimator";
	private Animator _animator;

	public PlayerAnimationManager(Animator animator) {
		_animator = animator;
	}
	public void Play(string animationName, MovementDirection direction) {
		string animation = animationName + "-" + ConvertMovementToAnimation(direction);
		_animator.Play(animation);
	}

	public void Play(string animationName) {
		_animator.Play(animationName);
	}

	public float GetAnimationDuration() {
		return _animator.GetCurrentAnimatorStateInfo(0).length;
	}

	public void Stop() {
		_animator.StopPlayback();
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