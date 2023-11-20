using Godot;
using System;

public partial class Hearth : AspectRatioContainer
{
	[Export] Texture2D fullHearth = null;
	[Export] Texture2D halfHearth = null;
	[Export] Texture2D emptyHearth = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetFullHearth()
	{
		GetNode<TextureRect>("Sprite").Texture = fullHearth;
	}
	
	public void SetHalfHearth()
	{
		GetNode<TextureRect>("Sprite").Texture = halfHearth;
	}
	
	public void SetEmptyHearth()
	{
		GetNode<TextureRect>("Sprite").Texture = emptyHearth;
	}
}
