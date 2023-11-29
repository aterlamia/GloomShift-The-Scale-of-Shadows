using GenericPlatforformer.State;
using Godot;

namespace GenericPlatforformer.Enemy;

public abstract class EnemyState
{
    protected Enemy Enemy = null;
    protected Player Player = null;
    protected AnimationNodeStateMachinePlayback _playback = null;
    protected readonly AudioStreamPlayer2D _sound;
    public StateTypes? NextState { get; set; } = null;

    public EnemyState(Enemy enemy, Player player, AnimationNodeStateMachinePlayback playback, AudioStreamPlayer2D sound, bool canMove)
    {
        _sound = sound;
        Enemy = enemy;
        Player = player;
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