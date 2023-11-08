using Godot;
using System;

public partial class DamageHandler : Node2D
{
    [Export]
    private int _health = 20;
    
    public void Hit(int damage)
    {
        _health -= damage;
        
        GetParent<Enemy>().wasHit();
        if (_health <= 0)
        {
            // delete the parent of this node
            GetParent().QueueFree();
        }
    }
    
}
