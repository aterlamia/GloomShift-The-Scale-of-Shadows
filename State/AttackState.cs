using Godot;

namespace GenericPlatforformer.State;

public class AttackState : State
{
    private float _damage = 5.0f;
    

    public AttackState(Player player, AnimationNodeStateMachinePlayback playback, bool canMove, float damage) : base(player, playback,
        canMove)
    {
        _damage = damage;
    }

    public override void ProcessState(double delta)
    {
        
    }

    public override void Enter()
    {
        GD.Print("Attack!");
        _playback.Travel("attack");
    }

    public override void Exit()
    {
        // set new state
    }
}