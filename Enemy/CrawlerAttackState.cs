using System;
using GenericPlatforformer.State;
using Godot;

namespace GenericPlatforformer.Enemy;

public class CrawlerAttackState : EnemyState
{
    private float _damage = 5.0f;
    private float attackRange = 30.0f;
    private float followRange = 200.0f;
    private float attackCooldown = 2.0f; // Time between attacks
    private bool canAttack = true;
    private float _moveSpeed = 100.0f;
    private Vector2 _target = Vector2.Zero;
    
    public float FollowRange
    {
        get => followRange;
        set => followRange = value;
    }


    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }

    private bool InFollowRange()
    {
        float distanceToPlayer = Enemy.GlobalPosition.DistanceTo(Player.GlobalPosition);
        return distanceToPlayer <= FollowRange;
    }


    public CrawlerAttackState(Enemy enemy, Player player, AnimationNodeStateMachinePlayback playback,
        AudioStreamPlayer2D sound, bool canMove,
        float damage) : base(enemy, player, playback, sound, canMove)
    {
        _damage = damage;
        MoveSpeed = 250f;
    }

    public override void ProcessState(double delta)
    {

        var tempDir = Enemy.Direction;

        Enemy.ApplyGravity(delta);
        if (InFollowRange())
        {
            MoveToPlayer();
        }
        else
        {
            Enemy.Direction = Vector2.Zero;
        }


        if (Math.Abs(tempDir.X - Enemy.Direction.X) > 0.1f)
        {
            Enemy.ChangeDir();
        }

        Enemy.MoveAndSlide();
    }

    public float MoveSpeed { get; set; }

    public override void Enter()
    {
        _target = Player.GlobalPosition;
        
        _playback.Travel("attack");
        _sound.Play();
    }

    public override void Exit()
    {
        GD.Print("hier222!!!!");
        _playback.Travel("move");
    }

    private void MoveToPlayer()
    {
        if (_target == Vector2.Zero)
        {
            return;
        }
        // move enemy towards player
        Vector2 direction = _target > Enemy.GlobalPosition ? Vector2.Right : Vector2.Left;
        Enemy.Velocity += (direction * MoveSpeed);
        if (Enemy.Velocity.X is > -0.1f and < 0.1f)
        {
            Enemy.Direction = Vector2.Zero;
        }
        else
        {
            Enemy.Direction = Enemy.Velocity.X > 0 ? Vector2.Right : Vector2.Left;
        }
        
        
        //when at the _target position, set target to zero and set the next state to ground
        if (Enemy.GlobalPosition.DistanceTo(_target) < 100)
        {
            GD.Print(_target);
            _target = Vector2.Zero;
            Enemy.Direction = Vector2.Zero;
            NextState = StateTypes.Ground;
        }
    }
}