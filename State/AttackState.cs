using Godot;

namespace GenericPlatforformer.State;

public class AttackState : State
{
	private float _damage = 5.0f;
	

	public AttackState(Player player, AnimationNodeStateMachinePlayback playback, AudioStreamPlayer2D sound, bool canMove, float damage) : base(player, playback,sound,
		canMove)
	{
		_damage = damage;
	}

	public override void ProcessState(double delta)
	{
		_player.Velocity = _player.Velocity + -_player.LookingDirection() ;

	}

	public override void Enter()
	{
		var attack = (AudioStreamWav)ResourceLoader.Load("res://assets/sounds/hit.wav");

		_sound.Stream = attack;
		_playback.Travel("attack");
		_sound.Play();
	}

	public override void Exit()
	{
		// set new state
	}
}
