using Godot;
using System;

public partial class Switch1 : Area2D
{
	private Boolean _switchActive = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_body_entered(Node2D body)
	{
		if (body is Shadow)
		{
			_switchActive = true;
		}
	}
	
	private void _on_body_exit(Node2D body)
	{ 
		if (body is Shadow)
		{
			_switchActive = false;
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("activate") && _switchActive)
		{
			GetParent<TileMap>().SetCell(3, new Vector2I(135, 19),0, new Vector2I(11, 7));
			GetParent<TileMap>().SetCell(3, new Vector2I(135, 18),0, new Vector2I(11, 6));
			
			GetParent<TileMap>().SetCell(3, new Vector2I(46, 17),0, new Vector2I(6, 2));
			GetParent<TileMap>().SetCell(3, new Vector2I(46, 18),0, new Vector2I(6, 2));
			GetParent<TileMap>().SetCell(3, new Vector2I(46, 19),0, new Vector2I(6, 2));
		}
	}
}
