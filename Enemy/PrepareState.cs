using System;
using GenericPlatforformer.State;
using Godot;

namespace GenericPlatforformer.Enemy;

public class PrepareState: EnemyState
{
    private float attackRange = 30.0f;
    private float followRange = 200.0f;
    private float attackCooldown = 1.4f; // Time between attacks
    private float timeSinceLastAttack = 0.0f;
    private bool canAttack = true;
    private float _moveSpeed = 100.0f;

    public float FollowRange
    {
        get => followRange;
        set => followRange = value;
    }

    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }

    public override void ProcessState(double delta)
    {
    }

    public override void InputState(InputEvent @event)
    {
     
    }

    public override void Enter()
    {
        GD.Print("Hier");
        _playback.Travel("prepare");
    }
    
    public override void Exit()
    {
        _playback.Travel("attack");
    }

    public PrepareState(Enemy enemy, Player player, AnimationNodeStateMachinePlayback playback, AudioStreamPlayer2D sound, bool canMove) : base(enemy,player, playback, sound,canMove)
    {
    }
}