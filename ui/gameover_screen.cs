using Godot;
using System;
using GenericPlatforformer;

public partial class gameover_screen : Control
{
	private GlobalState _globalState;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_globalState = GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState");
		GetNode<GlobalState>("/root/GlobalState").Connect("Gameover", new Callable(this, "gameover"));
	}

	private void gameover()
	{
		GD.Print("gamoverscree");
		Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_button_pressed()
	{
		Visible = false;
		_globalState.SwitchLevelTo(_globalState.Currentlevel);
	}
}
