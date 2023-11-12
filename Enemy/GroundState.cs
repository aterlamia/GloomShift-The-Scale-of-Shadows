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
    
    private void AttackPlayer()
    {
        canAttack = false;
        timeSinceLastAttack = 0.0f;
    }

    private bool InFollowRange()
    {
        float distanceToPlayer = Enemy.GlobalPosition.DistanceTo(Player.GlobalPosition);
        return distanceToPlayer <= followRange;
    }
    
    public override void ProcessState(double delta)
    {
        timeSinceLastAttack += (float)delta;

        var tempDir = Enemy.Direction;
        if (timeSinceLastAttack >= attackCooldown && inAttackRange())
        {
            GD.Print("Attack range", timeSinceLastAttack);
            
            timeSinceLastAttack = 0;
            NextState = StateTypes.Attack;
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
        Enemy.Velocity += (direction * _moveSpeed);
        if (Enemy.Velocity.X is > -0.1f and < 0.1f)
        {
            Enemy.Direction = Vector2.Zero;
        }
        else
        {
            Enemy.Direction = Enemy.Velocity.X > 0 ? Vector2.Right : Vector2.Left;
        }
    }

    // private void AttackPlayer()
    // {
    //     canAttack = false;
    //     timeSinceLastAttack = 0.0f;
    //     _isAttacking = true;
    // }
    
    public bool inAttackRange()
    {
        float distanceToPlayer = Enemy.GlobalPosition.DistanceTo(Player.GlobalPosition);
        return distanceToPlayer <= attackRange;
    }
    public override void InputState(InputEvent @event)
    {
     
    }


    public GroundState(Enemy enemy, PLayer pLayer, AnimationNodeStateMachinePlayback playback, bool canMove) : base(enemy,pLayer, playback, canMove)
    {
    }
}