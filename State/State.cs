using Godot;

namespace GenericPlatforformer.State;

public abstract class State
{
    protected PLayer _player = null;
    protected AnimationNodeStateMachinePlayback _playback = null;
    public StateTypes? NextState { get; set; } = null;

    public State(PLayer player, AnimationNodeStateMachinePlayback playback, bool canMove)
    {
        _player = player;
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