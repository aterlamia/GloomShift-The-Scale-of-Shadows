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

	//if player enters switch area, switch is activated
	private void _on_body_entered(Node2D body)
	{
		GD.Print(body);
		//if player enters switch area, switch is activated
		if (body is Shadow)
		{
			_switchActive = true;
		}
	}
	
	private void _on_body_exit(Node2D body)
	{
		GD.Print(body);
		//if player enters switch area, switch is activated
		if (body is Shadow)
		{
			_switchActive = false;
		}
	}
	
	private void _on_area_entered(Area2D body)
	{
		GD.Print(body);
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("activate") && _switchActive)
		{
			// GetTree().Root.GetNode<TileMap>("Level/TileMap").Cells[1, 1]
			
			//get the tile set to a certain coordinate in the tilemap and print it
			// GD.Print(
			// 	GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").GetCellAtlasCoords(0, new Vector2I(135, 19), false));
			// GD.Print(
			// 	GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").GetCellAtlasCoords(1, new Vector2I(135, 19), false));
			// GD.Print(
			// 	GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").GetCellAtlasCoords(2, new Vector2I(135, 19), false));
			GD.Print(
				GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").GetCellAtlasCoords(3, new Vector2I(135, 19), false));
			
			GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").SetCell(3, new Vector2I(135, 19),0, new Vector2I(11, 7));
			GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").SetCell(3, new Vector2I(135, 18),0, new Vector2I(11, 6));
			
			GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").SetCell(3, new Vector2I(46, 17),0, new Vector2I(6, 2));
			GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").SetCell(3, new Vector2I(46, 18),0, new Vector2I(6, 2));
			GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").SetCell(3, new Vector2I(46, 19),0, new Vector2I(6, 2));
			// GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").SetCell(3, new Vector2I(134, 19),1, new Vector2I(6, 0));
			// GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").SetCell(3, new Vector2I(132, 19),0, new Vector2I(6, 0));
			// GetTree().Root.GetNode<TileMap>("Game/Level/TileMap").SetCell(3, new Vector2I(130, 19),-1, new Vector2I(6, 0));
			//set the tile at a certain coordinate to the tile with id 1 in the tileset
			// GetTree().Root.GetNode<TileMap>("Level/TileMap").SetCellv(new Vector2(1,1), 1);
		}
		
	}
}
