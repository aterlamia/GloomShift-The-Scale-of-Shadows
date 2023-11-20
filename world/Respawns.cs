using Godot;
using System;
using GenericPlatforformer;

public partial class Respawns : Node2D
{
	private void _on_respawn_area_entered(Node2D error, NodePath nodePath)
	{
		GD.Print(nodePath);
		var node2D = GetNode<Node2D>(nodePath);
		var globalState = GetNode<GlobalState>("/root/GlobalState");
		globalState.SetRespawnPoint(node2D);
	}
}
