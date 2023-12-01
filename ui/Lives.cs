using Godot;
using System;
using System.Collections.Generic;

public partial class Lives : Control
{

	private int _maxLives = 10;
	private int _currentLives = 10;
	
	[Export] PackedScene hearth = null;
	private List<Hearth> _hearths = new List<Hearth>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState").Connect("PlayerHealthChanged", new Callable(this, "healthChanged"));
		
		//for max lives add a hearth for each life, each lives is 2 halve hearths
		for (int i = 0; i < _maxLives/2; i++)
		{
			var instance = (Hearth) hearth.Instantiate();
			instance.SetFullHearth();
			AddChild(instance);
			instance.Position = new Vector2(i * 16, 0);
			_hearths.Add(instance);
		}
	}

	private void UpdateHealth(int health)
	{
		for (int i = 1; i <= _maxLives/2; i++)
		{
			if(health ==  i * 2 - 1)
				_hearths[i-1].SetHalfHearth();
			else if (health <= i * 2 - 2) 
				_hearths[i-1].SetEmptyHearth();
			else
				_hearths[i-1].SetFullHearth();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void healthChanged(int health)
	{
		_currentLives = health;
		UpdateHealth(health);
		GD.Print("Health changed to " + health);
	}
}
