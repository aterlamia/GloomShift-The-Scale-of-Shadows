using Godot;
using System;
using GenericPlatforformer;
using Godot.Collections;

public partial class LootSpawner : Node2D
{
	[Export]
	Dictionary<string, PackedScene> LootScenes = new Dictionary<string, PackedScene>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState").Connect("SpawnLoot", new Callable(this, "spawn"));
	}

	private void spawn(string lootType, Vector2 position)
	{
		if (!LootScenes.ContainsKey(lootType))
		{
			GD.Print("Could not spawn loot, loot type not found: " + lootType + LootScenes.Keys);
			return;
		}
		var loot = LootScenes[lootType];
		//create new loot if it exist in the dictionary on the given position
		if (loot != null)
		{
			var instance = (Node2D) loot.Instantiate();
			AddChild(instance);
			instance.Position = position;
		}

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
