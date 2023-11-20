using Godot;
using System;

public partial class Shadow : Player
{
    protected float SpeedInLight = 100.0f;
    private float _speed = 300.0f;
    private Vector2 velocity = Vector2.Zero;
    public bool IsAutonomous { get; set; } = false;
    public Vector2 ParentVelocity { get; set; } = Vector2.Zero;
    private bool _isAttacking = false;

    public Node2D LightDetector { get; private set; }

    protected float Speed
    {
        get => LightDetector == null ? _speed : SpeedInLight;
        set => _speed = value;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public Vector2 GetDirection()
    {
        return Direction;
    }


    private void wasHit(int damage)
    {
        GD.Print("The shadow was hit for " + damage + " damage");
    }
    //
    // public override void Calc(double delta)
    // {
    //     var curDir = Direction.X;
    //     velocity = Velocity;
    //     ApplyGravity(delta);
    //     ProcessInput(delta);
    //     Velocity = velocity;
    //
    //     if (curDir != Direction.X)
    //     {
    //         ChangeDir();
    //     }
    //
    //     AnimationTree.Set("parameters/move/blend_position", Direction.X);
    // }

    // protected override void ProcessInput(double delta)
    // {
    //     var curDir = Direction.X;
    //     if ((IsOnFloor() || Direction.X == 0) && StateManager.CanMove())
    //     {
    //         velocity.X = (Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left")) * Speed;
    //         Direction.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
    //     }
    //
    //     if (curDir != Direction.X)
    //     {
    //         ChangeDir();
    //     }
    // }


    public void ParentControl(double delta, Vector2 direction)
    {
        if (Math.Abs(direction.X - Direction.X) > 0.5f)
        {
            Direction = direction;
            ChangeDir();
        }

        ApplyGravity(delta);
        AnimationTree.Set("parameters/move/blend_position", Direction.X);
    }

    // public override void ApplyGravity(double delta)
    // {
    //     if (!IsOnFloor())
    //     {
    //         velocity.Y += Gravity * (float)delta;
    //     }
    // }


    public void _on_light_detector_area_entered(Node2D area)
    {
        if (area.Name == "LightArea")
        {
            LightDetector = area;
        }
    }

    public void _on_light_detector_area_exited(Node2D area)
    {
        if (area.Name == "LightArea")
        {
            LightDetector = null;
        }
    }
}