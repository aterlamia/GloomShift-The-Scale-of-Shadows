using Godot;
using System;
using GenericPlatforformer;

public partial class Start : Node2D
{
	private GlobalState _globalState = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_globalState = GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState");
		_globalState.Connect("PowerChanged", new Callable(this, "powerChanged"));
	}


	private void powerChanged(int power)
	{
		if (power == 1)
		{
			GetNode<Node2D>("shadow").Visible = true;
		}
		if (power == 2)
		{
			GetNode<Node2D>("Shrink").Visible = true;
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
