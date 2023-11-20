using Godot;
using System;
using GenericPlatforformer;

public partial class outofbounds : Area2D
{
	private void _on_area_entered(Node2D node)
	{
		var globalState = GetNode<GlobalState>("/root/GlobalState");
		globalState.PlayerWasHurt(2);
		globalState.RespawnPlayer();
	}
}
