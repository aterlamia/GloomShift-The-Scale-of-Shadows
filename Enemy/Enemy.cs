using Godot;
using System;
using GenericPlatforformer.State;

namespace GenericPlatforformer.Enemy;

public partial class Enemy : CharacterBody2D
{
    private Vector2 velocity = Vector2.Zero;
    public float MoveSpeed { get; set; } = 100.0f;
    private bool isMovingRight = true;
    private float gravity = 2000.0f; // Adjust to control the gravity strength
    private float jumpForce = -590.0f; // Adjust to control the jump strength
    protected AnimationTree AnimationTree;
    private float Speed = 300.0f; // Adjust to control the jump strength
    private Sprite2D Sprite;

    // Attack variables

    public Vector2 Direction = Vector2.Zero;
    private bool _isAttacking = false;

    public void wasHit()
    {
        var stateMachine = GetNode<StateManager>("StateManager");
        stateMachine.ChangeState(StateTypes.Hit);

    }

    public override void _Ready()
    {
        AnimationTree = GetNode<AnimationTree>("AnimationTree");
        AnimationTree.Active = true;
        Sprite = GetNode<Sprite2D>("EnemySprite");
    }

    // Called in every frame.
    public override void _PhysicsProcess(double delta)
    {
        AnimationTree.Set("parameters/move/blend_position", Direction.X);
    }

    public void ApplyGravity(double delta)
    {
        if (IsOnFloor() || IsOnCeiling())
        {
            velocity.Y = 0;
        }
        else
        {
            velocity.Y += gravity * (float)delta;
        }

        Velocity = velocity;
    }

    public void ChangeDir()
    {
        var attackCol = GetNode<Area2D>("Attack");
        if (Direction.X < 0)
        {
            Sprite.FlipH = true;
            attackCol.Position = new Vector2(-MathF.Abs(attackCol.Position.X), attackCol.Position.Y);
        }
        else if (Direction.X > 0)
        {
            attackCol.Position = new Vector2(MathF.Abs(attackCol.Position.X), attackCol.Position.Y);
            Sprite.FlipH = false;
        }
    }

    // private void MoveEnemy()
    // {
    //     if (!inAttackRange())
    //     {
    //         velocity.X = isMovingRight ? moveSpeed : -moveSpeed;
    //     }
    //
    //     Velocity = velocity;
    //     MoveAndSlide();
    //
    //     if (IsOnWall() && !inAttackRange())
    //     {
    //         isMovingRight = !isMovingRight;
    //     }
    // }


    // Called in every frame.
    public override void _Process(double delta)
    {
        // if (!canAttack)
        // {
        //     // Update the attack cooldown
        //     timeSinceLastAttack += (float)delta;
        //     if (timeSinceLastAttack >= attackCooldown)
        //     {
        //         canAttack = true;
        //     }
        // }
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

    public void _on_attack_area_entered(Node2D body)
    {
        GD.Print(body);
    }
}