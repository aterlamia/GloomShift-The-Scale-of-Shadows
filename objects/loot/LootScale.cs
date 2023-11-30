using Godot;
using System;
using GenericPlatforformer;

public partial class LootScale : Node2D
{
    private void _on_pickup_body_entered(Node2D body)
    {
        if (body is Player)
        {
            GetNode<GlobalState>("/root/GlobalState").PickupLoot(LootTypes.Scale, 1);
            QueueFree();
        }
    }
}