using Godot;
using System;
using GenericPlatforformer;

public partial class World : Node2D
{
	//ADD dictionary for all the levels
	[Export]
	public string LevelName = "level1";
	// Called when the node enters the scene tree for the first time.
	private GlobalState _globalState = null;
	public override void _Ready()
	{
		var level = (PackedScene) ResourceLoader.Load("res://levels/" + LevelName + ".tscn");
		var instance = (Node2D) level.Instantiate();
		AddChild(instance);

		_globalState = GetNode<GlobalState>("/root/GlobalState");
		_globalState.Connect("SwitchLevel", new Callable(this, "changeLevel"));
	}
	
	private void changeLevel(string nextLevel)
	{
		switchLevel(nextLevel);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	//switch to the next level
	public void switchLevel(string nextLevel)
	{
		var level = (PackedScene) ResourceLoader.Load("res://levels/" + nextLevel + ".tscn");
		//remove the current level
		GetTree().Root.GetNode<World>("Game").GetChild(0).CallDeferred("queue_free");
		var instance = (Node2D) level.Instantiate();
		GetTree().Root.GetNode<World>("Game").AddChild(instance);
	}
}
	
	
