using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public int Health { get; private set; } = 10;
    public int MaxHealth { get; private set; } = 10;
    protected Vector2 LocalVelocity = Vector2.Zero;
    protected float Gravity = 980.0f;
    protected float Speed = 300.0f;
    private Area2D _attackHitPoint;
    protected AnimationTree AnimationTree;
    protected Sprite2D Sprite;
    protected StateManager StateManager;
    public bool ShadowControlled { get; set; } = false;

    protected Vector2 Direction = Vector2.Zero;

    public Vector2 GetDirection()
    {
        return Direction;
    }

    public override void _Ready()
    {
        AnimationTree = GetNode<AnimationTree>("AnimationTree");
        _attackHitPoint = GetNode<Area2D>("Sword");
        Sprite = GetNode<Sprite2D>("Sprite2D");
        AnimationTree.Active = true;
        StateManager = GetNode<StateManager>("StateManager");
        GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState").Connect("PlayerHurt", new Callable(this, "wasHit"));
        GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState").UpdatePlayerHealth(Health);
    }

    private void wasHit(int damage)
    {
        Health -= damage;
        GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState").UpdatePlayerHealth(Health);
        
    }
    public virtual void Calc(double delta)
    {
        LocalVelocity = Velocity;
        ApplyGravity(delta);

        if (!ShadowControlled)
        {
            ProcessInput(delta);
        }

        Velocity = LocalVelocity;
        AnimationTree.Set("parameters/move/blend_position", Direction.X);
    }

    protected void ChangeDir()
    {
        if (Direction.X < 0)
        {
            Sprite.FlipH = true;
            GetNode<Sword>("Sword").Position = new Vector2(MathF.Abs(GetNode<Sword>("Sword").Position.X) * -1,
                GetNode<Sword>("Sword").Position.Y);
        }
        else if (Direction.X > 0)
        {
            Sprite.FlipH = false;
            GetNode<Sword>("Sword").Position = new Vector2(
                MathF.Abs(GetNode<Sword>("Sword").Position.X),
                GetNode<Sword>("Sword").Position.Y);
        }
    }

    protected virtual void ProcessInput(double delta)
    {
        var curDir = Direction.X;
        if ( StateManager.CanMove())
        {
            LocalVelocity.X = (Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left")) * Speed;
            Direction.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");

          
        }
        if (curDir != Direction.X)
        {
            ChangeDir();
        }
    }

    public virtual void ApplyGravity(double delta)
    {
        if (!IsOnFloor())
        {
            LocalVelocity.Y += Gravity * (float)delta;
        }
    }
}