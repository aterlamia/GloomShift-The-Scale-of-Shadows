using Godot;
using System;

public partial class PLayer : CharacterBody2D
{
    private Vector2 velocity = Vector2.Zero;
    private float gravity = 2000.0f; // Adjust to control the gravity strength
    private float jumpForce = -550.0f; // Adjust to control the jump strength
    private float Speed = 300.0f; // Adjust to control the jump strength
    private int jumpsRemaining = 2; // Number of double jumps allowed
    private bool isJumping = false;

    private Vector2 _direction = Vector2.Zero;

    private AnimatedSprite2D animator;

    public override void _Ready()
    {
        animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animator.Play("idle");
    }

    public override void _PhysicsProcess(double delta)
    {
        velocity = Velocity;
        ApplyGravity(delta);
        ProcessInput(delta);
        Velocity = velocity;


        Animate();

        MoveAndSlide();
    }

    private void Animate()
    {
        if (velocity.Y != 0)
        {
            animator.Play("jump");
        }
        else if (velocity.X != 0)
        {
            animator.Play("run");
        }
        else
        {
            animator.Play("idle");
        }

        if (velocity.X < 0)
        {
            animator.FlipH = true;
        }
        else if(velocity.X > 0)
        {
            animator.FlipH = false;
        }
    }

    private void ProcessInput(double delta)
    {
        if (IsOnFloor() || IsOnCeiling() || _direction.X == 0)
        {
            // Reset jumps when on the ground or ceiling
            isJumping = false;
            jumpsRemaining = 2;
            velocity.X = (Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left")) * Speed;
            _direction.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        }
        else
        {
            Vector2 tempdirection =
                new Vector2(Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"), 0);

            if (tempdirection.X != 0 && tempdirection.X != _direction.X)
            {
                // damp velocity x to 0
                velocity.X = Mathf.Lerp(velocity.X, 0, 3f * (float)delta);
            }
        }

        if (Input.IsActionJustPressed("jump") && jumpsRemaining > 0)
        {
            velocity.Y = jumpForce;
            isJumping = true;
            jumpsRemaining--;
        }
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
}