using System;
using GenericPlatforformer.State;
using Godot;

namespace GenericPlatforformer.Enemy;

public class HitState: EnemyState
{
    private float attackRange = 30.0f;
    private float followRange = 200.0f;
    private float attackCooldown = 2.0f; // Time between attacks
    private float timeSinceLastAttack = 0.0f;
    private bool canAttack = true;
    private float _moveSpeed = 1000.0f;
    private float pushBackForce = 40.0f;
    public override void ProcessState(double delta)
    {
        float distanceToPlayer = Enemy.GlobalPosition.DistanceTo(Player.GlobalPosition);

        if (distanceToPlayer <= pushBackForce)
        {
            GD.Print(Enemy.Direction);
            var velocity = Enemy.Direction * -1 * _moveSpeed;
            Enemy.Velocity = velocity;
            Enemy.MoveAndSlide();
        }
    }


    public override void InputState(InputEvent @event)
    {
     
    }


    public HitState(Enemy enemy, PLayer pLayer, AnimationNodeStateMachinePlayback playback, bool canMove) : base(enemy,pLayer, playback, canMove)
    {
    }
    
    public override void Enter()
    {
        Enemy.MoveSpeed = _moveSpeed;
        _playback.Travel("hit");
    }
    
    public override void Exit()
    {
        _playback.Travel("move");
    }

}