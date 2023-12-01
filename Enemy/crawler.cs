using Godot;
using System;
using GenericPlatforformer;
using GenericPlatforformer.Enemy;
using GenericPlatforformer.State;

public partial class crawler : Enemy
{
    private Vector2 velocity = Vector2.Zero;
    public float MoveSpeed { get; set; } = 100.0f;
    private bool isMovingRight = true;
    private float gravity = 2000.0f; // Adjust to control the gravity strength
    private float jumpForce = -590.0f; // Adjust to control the jump strength
    protected AnimationTree AnimationTree;
    private float Speed = 300.0f; // Adjust to control the jump strength
    protected Sprite2D Sprite;


    // ready that cals the base ready
    public override void _Ready()
    {
        base._Ready();
        base.type = 2;
        AnimationTree = GetNode<AnimationTree>("AnimationTree");
        AnimationTree.Active = true;
        Sprite = GetNode<Sprite2D>("EnemySprite");
    }


    // Attack variables

    public int type = 1;
    public Vector2 Direction = Vector2.Zero;
    private bool _isAttacking = false;

    public void wasHit()
    {
        var stateMachine = GetNode<StateManager>("StateManager");
        if (GetNode<DamageHandler>("DamageHandler").IsDead)
        {
            stateMachine.ChangeState(StateTypes.Die);
        }

        stateMachine.ChangeState(StateTypes.Hit);
    }

    public void spawnLoot()
    {
        GetNode<GlobalState>("/root/GlobalState").SpawnLootAtPos(LootTypes.Scale, GlobalPosition);
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
        if (Direction.X < 0)
        {
            Sprite.FlipH = true;
        }
        else if (Direction.X > 0)
        {
            Sprite.FlipH = false;
        }

        // ugly hack make it better later
        if (!HasNode("Attack"))
        {
            return;
        }

        var attackCol = GetNode<Area2D>("Attack");
        if (Direction.X < 0)
        {
            attackCol.Position = new Vector2(-MathF.Abs(attackCol.Position.X), attackCol.Position.Y);
        }
        else if (Direction.X > 0)
        {
            attackCol.Position = new Vector2(MathF.Abs(attackCol.Position.X), attackCol.Position.Y);
        }
    }

    public void _on_attack_body_entered(Node2D body)
    {
        GetNode<GlobalState>("/root/GlobalState").PlayerWasHurt(1);
    }
}