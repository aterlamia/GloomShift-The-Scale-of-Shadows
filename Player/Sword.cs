using Godot;
using System;

public partial class Sword : Area2D
{
    public void _on_attack_hit_point_body_entered(Node2D body)
    {
        if (body.HasNode("DamageHandler"))
        {
            body.GetNode<DamageHandler>("DamageHandler").Hit(5);
        }
        else
        {
        }

    }
}