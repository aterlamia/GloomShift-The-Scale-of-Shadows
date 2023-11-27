using GenericPlatforformer.State;
using Godot;

namespace GenericPlatforformer.Enemy;

public class AttackState : EnemyState
{
    private float _damage = 5.0f;
    private float timeSinceLastAttack = 0.0f;

    public AttackState(Enemy enemy, Player player, AnimationNodeStateMachinePlayback playback, bool canMove,
        float damage) : base(enemy, player, playback,
        canMove)
    {
        _damage = damage;
    }

    public override void ProcessState(double delta)
    {
    }

    public override void Enter()
    {
        _playback.Travel("attack");
    }

    public override void Exit()
    {
        _playback.Travel("move");
    }
}