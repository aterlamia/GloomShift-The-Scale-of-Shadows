using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    private Vector2 velocity = Vector2.Zero;
    private float moveSpeed = 100.0f;
    private bool isMovingRight = true;
    private float gravity = 2000.0f; // Adjust to control the gravity strength
    private float jumpForce = -590.0f; // Adjust to control the jump strength

    private float Speed = 300.0f; // Adjust to control the jump strength

    // Attack variables
    private float attackRange = 30.0f; // Adjust this to set the attack range
    private float attackCooldown = 2.0f; // Time between attacks
    private float timeSinceLastAttack = 0.0f;
    private bool canAttack = true;
    private AnimatedSprite2D animator;


    // Reference to the player (you should set this in the Inspector)
    private PLayer player;

    private bool _isAttacking = false;
    // Called when the node enters the scene tree for the first time.


    public void wasHit()
    {
        GD.Print(isMovingRight);
        velocity = new Vector2(isMovingRight ? 120 : -120, -120);
    }

    public override void _Ready()
    {
        player = GetTree().Root
            .GetNode<PLayer>("Game/Level/PlayerContainer/Player"); // Adjust the path to the player node in your scene
        animator = GetNode<AnimatedSprite2D>("./EnemySprite");
        animator.Play("idle");
    }

    public bool inAttackRange()
    {
        float distanceToPlayer = GlobalPosition.DistanceTo(player.GlobalPosition);

        return distanceToPlayer <= attackRange;
    }
    // Called in every frame.
    public override void _PhysicsProcess(double delta)
    {
        // Calculate the distance to the player
        
        if(inAttackRange()){
            if (canAttack)
            {
                // The player is within attack range, perform the attack
                AttackPlayer();
            }
        }

        MoveEnemy();

        ApplyGravity(delta);
        Animate();
    }

    private void ApplyGravity(double delta)
    {
        if (IsOnFloor() || IsOnCeiling())
        {
            velocity.Y = 0;
        }
        else
        {
            velocity.Y += gravity * (float)delta;
        }
    }

    public void Animate()
    {
        // play attack animation, when animation finishes set _isAttacking to false
        if (_isAttacking)
        {
            animator.Play("attack");

            // get current frame
            if (animator.Frame == 5)
            {
                _isAttacking = false;
            }

            return;
        }

        if (velocity.Y != 0)
        {
            animator.Play("walk");
        }
        else if (velocity.X != 0)
        {
            animator.Play("walk");
        }
        else
        {
            animator.Play("idle");
        }

        if (velocity.X < 0)
        {
            animator.FlipH = true;
        }
        else if (velocity.X > 0)
        {
            animator.FlipH = false;
        }
    }

    private void MoveEnemy()
    {
        if (!inAttackRange())
        {
            velocity.X = isMovingRight ? moveSpeed : -moveSpeed;
        }

        Velocity = velocity;
        MoveAndSlide();

        if (IsOnWall() && !inAttackRange())
        {
            isMovingRight = !isMovingRight;
        }
    }

    private void AttackPlayer()
    {
        canAttack = false;
        timeSinceLastAttack = 0.0f;
        _isAttacking = true;
    }

    // Called in every frame.
    public override void _Process(double delta)
    {
        if (!canAttack)
        {
            // Update the attack cooldown
            timeSinceLastAttack += (float)delta;
            if (timeSinceLastAttack >= attackCooldown)
            {
                canAttack = true;
            }
        }
    }

    public void _on_attack_hit_point_area_entered(Area2D body)
    {
        // var parent = body.GetParent();
        //
        // GD.Print(parent);
        // if (parent is Enemy)
        // {
        //     GD.Print("hit enemy");
        //     body.QueueFree();
        // }
    }

    public void _on_attack_hit_point_body_entered(Node2D body)
    {
        // GD.Print(body);
    }
}