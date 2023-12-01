using GenericPlatforformer.State;
using Godot;

namespace GenericPlatforformer.Enemy;

public class AttackState : EnemyState
{
    private float _damage = 5.0f;
    private float timeSinceLastAttack = 0.0f;

    public AttackState(Enemy enemy, Player player, AnimationNodeStateMachinePlayback playback, AudioStreamPlayer2D sound, bool canMove,
        float damage) : base(enemy, player, playback,sound, canMove)
    {
        _damage = damage;
    }

    public override void ProcessState(double delta)
    {
    }

    public override void Enter()
    {
        var land = (AudioStreamWav)ResourceLoader.Load("res://assets/sounds/hit2.wav");
        _playback.Travel("attack");
        _sound.Play();
    }

    public override void Exit()
    {
        _playback.Travel("ground");
    }
}