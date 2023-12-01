using System;
using GenericPlatforformer.State;
using Godot;

namespace GenericPlatforformer.Enemy;

public class GroundState: EnemyState
{
    private float attackRange = 30.0f;
    private float followRange = 200.0f;
    private float attackCooldown = 2.0f; // Time between attacks
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

    private void AttackPlayer()
    {
        canAttack = false;
        timeSinceLastAttack = 0.0f;
    }

    private bool InFollowRange()
    {
        float distanceToPlayer = Enemy.GlobalPosition.DistanceTo(Player.GlobalPosition);
        return distanceToPlayer <= FollowRange;
    }
    
    public override void ProcessState(double delta)
    {
        timeSinceLastAttack += (float)delta;

        var tempDir = Enemy.Direction;
        if (timeSinceLastAttack >= attackCooldown && inAttackRange())
        {
            Console.WriteLine("Attack" + Enemy.type);
            if(Enemy.type == 1)
                NextState = StateTypes.Attack;
            else
                NextState = StateTypes.Prepare;
            timeSinceLastAttack = 0;
            
        }
        else
        {
            Enemy.ApplyGravity(delta);
            if (InFollowRange())
            {

                if (inAttackRange())
                {
                    Enemy.Direction = Vector2.Zero;
                }
                else
                {
                    MoveToPlayer();
                }

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
        
        
    }

    private void MoveToPlayer()
    {
        // move enemy towards player
        Vector2 direction = Player.GlobalPosition > Enemy.GlobalPosition ? Vector2.Right : Vector2.Left;
        Enemy.Velocity += (direction * MoveSpeed);
        if (Enemy.Velocity.X is > -0.1f and < 0.1f)
        {
            Enemy.Direction = Vector2.Zero;
        }
        else
        {
            Enemy.Direction = Enemy.Velocity.X > 0 ? Vector2.Right : Vector2.Left;
        }
    }

    
    public bool inAttackRange()
    {
        float distanceToPlayer = Enemy.GlobalPosition.DistanceTo(Player.GlobalPosition);
        return distanceToPlayer <= AttackRange;
    }
    public override void InputState(InputEvent @event)
    {
     
    }

    public override void Enter()
    {
        Enemy.MoveSpeed = MoveSpeed;
    }

    public GroundState(Enemy enemy, Player player, AnimationNodeStateMachinePlayback playback, AudioStreamPlayer2D sound, bool canMove) : base(enemy,player, playback, sound,canMove)
    {
    }
}