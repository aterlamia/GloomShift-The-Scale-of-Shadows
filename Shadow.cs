using Godot;
using System;

public partial class Shadow : CharacterBody2D
{
    private Vector2 velocity = Vector2.Zero;
    private float gravity = 2000.0f; // Adjust to control the gravity strength
    private float jumpForce = -590.0f; // Adjust to control the jump strength
    private float Speed = 300.0f; // Adjust to control the jump strength
    private int jumpsRemaining = 2; // Number of double jumps allowed
    private bool isJumping = false;
    public bool FinishFall { get; set; } = false;
    public bool IsAutonamous { get; set; } = false;
    private Vector2 _direction = Vector2.Zero;
    public Vector2 ParentVelocity { get; set; } = Vector2.Zero;
    private AnimatedSprite2D animator;

    
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
    
    public Vector2 GetDirection()
    {
        return _direction;
    }

    
    public override void _Ready()
    {
        animator = GetNode<AnimatedSprite2D>("ShadowCol/ShadowSprite");
        animator.Play("idle");
    }

    public void Calc(double delta)
    {
        if (IsAutonamous || FinishFall)
        {
            velocity = Velocity;
            ApplyGravity(delta);
            ProcessInput(delta);
            Velocity = velocity;
        }
        else
        {
            Velocity = ParentVelocity;
        }
    }

    public void Animate()
    {
        
        if (_direction.Y != 0)
        {
            animator.Play("run");
        }
        else if (_direction.X != 0)
        {
            animator.Play("run");
        }
        else
        {
            animator.Play("idle");
        }

        if (_direction.X < 0)
        {
            animator.FlipH = true;
            animator.Position = new Vector2(8, animator.Position.Y);
        }
        else if (_direction.X > 0)
        {
            animator.FlipH = false;
            animator.Position = new Vector2(-4, animator.Position.Y);
        }
    }

    private void ProcessInput(double delta)
    {
        if (IsOnFloor() || IsOnCeiling() || _direction.X == 0)
        {
            // Reset jumps when on the ground or ceiling
            isJumping = false;
            jumpsRemaining = 2;
            FinishFall = false;
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