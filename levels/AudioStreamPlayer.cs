using Godot;
using System;
using GenericPlatforformer;

public partial class AudioStreamPlayer : Godot.AudioStreamPlayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<GlobalState>("/root/GlobalState").Connect("LootChanged", new Callable(this, "lootChanged"));
		GetNode<GlobalState>("/root/GlobalState").Connect("PlayerHurt", new Callable(this, "wasHit"));
	}

	private void wasHit(int damage)
	{
		var attack = (AudioStreamWav)ResourceLoader.Load("res://assets/sounds/hurt.wav");

		Stream = attack;
		Play();
	}
    
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void lootChanged(string type, int val)
	{
		var attack = (AudioStreamOggVorbis)ResourceLoader.Load("res://assets/sounds/pickup.ogg");

		Stream = attack;
		Play();

	}
}
