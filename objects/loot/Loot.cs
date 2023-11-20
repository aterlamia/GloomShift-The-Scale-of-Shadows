using Godot;
using System;
using GenericPlatforformer;

public partial class Loot : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<GlobalState>("/root/GlobalState").Connect("LootChanged", new Callable(this, "lootChanged"));
	}

	private void lootChanged(string type, int val)
	{
		GD.Print("Loot changed 111: " + type + " " + val);
		GetNode<Label>("Label").Text = val.ToString();

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
