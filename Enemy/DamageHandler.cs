using Godot;
using System;
using GenericPlatforformer.Enemy;
using GenericPlatforformer.State;

public partial class DamageHandler : Node2D
{
    [field: Export] public int Health { get; private set; } = 20;

    public void Hit(int damage)
    {
        if (Health > 0)
        {
            Health -= damage;
            GetParent<Enemy>().wasHit();
        }
    }


    public void Die()
    {
        GetParent<Enemy>().GetNode<GenericPlatforformer.Enemy.StateManager>("StateManager").ChangeState(StateTypes.Die);
    }
}
