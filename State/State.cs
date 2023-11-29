using Godot;

namespace GenericPlatforformer.State;

public abstract class State
{
	protected readonly Player _player = null;
	protected readonly AnimationNodeStateMachinePlayback _playback = null;
	protected readonly AudioStreamPlayer2D _sound = null;
	public StateTypes? NextState { get; set; } = null;

	public State(Player player, AnimationNodeStateMachinePlayback playback, AudioStreamPlayer2D sound, bool canMove)
	{
		_player = player;
		_sound = sound;
		_playback = playback;
		CanMove = canMove;
	}

	public bool CanMove { get; } = true;
	
	
	public virtual void ProcessState(double delta)
	{
		
	}

	public virtual void InputState(InputEvent @event)
	{
		
	}
	
	public virtual void Enter()
	{
		
	}
	
	public virtual void Exit()
	{
		
	}
	
}
