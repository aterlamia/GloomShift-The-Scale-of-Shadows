using Godot;
using System;
using GenericPlatforformer;

public partial class World : Node2D
{
	//ADD dictionary for all the levels
	[Export]
	public string LevelName = "level3";
	// Called when the node enters the scene tree for the first time.
	private GlobalState _globalState = null;

	private Node2D _container = null;
	public override void _Ready()
	{
		_globalState = GetNode<GlobalState>("/root/GlobalState");
		_globalState.Connect("SwitchLevel", new Callable(this, "changeLevel"));
		_globalState.Connect("Gameover", new Callable(this, "gameover"));
		_container = GetTree().Root.GetNode<Node2D>("Game/LevelContainer");
		switchLevel(LevelName);
	}
	
	private void changeLevel(string nextLevel)
	{
		switchLevel(nextLevel);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void gameover()
	{
		if (_container.GetChildCount() > 0)
		{
			_container.GetChild(0).CallDeferred("queue_free");
		}
	}

	//switch to the next level
	public void switchLevel(string nextLevel)
	{
		var level = (PackedScene) ResourceLoader.Load("res://levels/" + nextLevel + ".tscn");
		//remove the current level
		if (_container.GetChildCount() > 0)
		{
			_container.GetChild(0).CallDeferred("queue_free");
		}

		var instance = (Node2D) level.Instantiate();
		_container.AddChild(instance);
	}
}
	
	
