using Godot;
using System;

public partial class PLayer : CharacterBody2D
{
    private Vector2 velocity = Vector2.Zero;
    private float gravity = 2000.0f; // Adjust to control the gravity strength
    private float jumpForce = -590.0f; // Adjust to control the jump strength
    private float Speed = 300.0f; // Adjust to control the jump strength
    private int jumpsRemaining = 2; // Number of double jumps allowed
    private bool isJumping = false;
    private Area2D _attackHitPoint;
    public bool ShadowControlled { get; set; } = false;

    private Vector2 _direction = Vector2.Zero;

    private AnimatedSprite2D animator;
    private bool _isAttacking = false;


    public Vector2 GetDirection()
    {
        return _direction;
    }

    public override void _Ready()
    {
        animator = GetNode<AnimatedSprite2D>("PlayerCol/Player");
        animator.Play("idle");
        _attackHitPoint = GetNode<Area2D>("Sword");
    }

    public void Calc(double delta)
    {
        velocity = Velocity;
        ApplyGravity(delta);

        if (!ShadowControlled)
        {
            ProcessInput(delta);
        }

        Velocity = velocity;
    }

    public void Animate()
    {
        // play attack animation, when animation finishes set _isAttacking to false
        if (_isAttacking)
        {
            animator.Play("attack");

            // get current frame
            if (animator.Frame == 0)
            {
                
            }
            else if (animator.Frame == 3)
            {
                _attackHitPoint.Monitoring = true;
            }
            else if (animator.Frame == 4)
            {
                _isAttacking = false;
                _attackHitPoint.Monitoring = false;
            }

            return;
        }

        if (velocity.Y != 0)
        {
            animator.Play("run");
        }
        else if (velocity.X != 0)
        {
            animator.Play("run");
        }
        else
        {
            animator.Play("idle");
        }
    }


    private void changeDir()
    {
        if (_direction.X < 0)
        {
            animator.FlipH = true;
            GetNode<Sword>("Sword").Position = new Vector2(MathF.Abs(GetNode<Sword>("Sword").Position.X )* -1,
                GetNode<Sword>("Sword").Position.Y);
        }
        else if (_direction.X > 0)
        {
            animator.FlipH = false;
            GetNode<Sword>("Sword").Position = new Vector2(MathF.Abs(GetNode<Sword>("Sword").Position.X) ,
                GetNode<Sword>("Sword").Position.Y);
        }
    }
    private void ProcessInput(double delta)
    {
        var curDir = _direction.X;
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

        if (Input.IsActionJustPressed("attack"))
        {
            _isAttacking = true;
        }

        if (curDir != _direction.X)
        {
            changeDir();
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