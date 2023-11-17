using Godot;

namespace GenericPlatforformer;

public partial class GlobalState : Node
{
	private int scales = 0;
	
	
	[Signal]
	public delegate void PlayerHurtEventHandler(int damage);
	

	[Signal]
	public delegate void PlayerHealthChangedEventHandler(int health);
	
	[Signal]
	public delegate void SpawnLootEventHandler(LootTypes lootType, Vector2 position );


	[Signal]
	public delegate void LootPickupEventHandler(LootTypes lootType, int value);
    
	[Signal]
	public delegate void LootChangedEventHandler(string lootType, int value);
	
	public void SpawnLootAtPos(LootTypes lootType, Vector2 position)
	{
		EmitSignal(SignalName.SpawnLoot, lootType.ToString(), position);
	}
	
	public void SetLoot(LootTypes lootType, int value)
	{
		GD.Print("Loot changed: " + lootType + " " + value);
		EmitSignal(SignalName.LootChanged, lootType.ToString(), value);
	}

	
	public void PickupLoot(LootTypes lootType, int value)
	{
		GD.Print("Loot added .: " + lootType + " " + value);
		if (lootType == LootTypes.Scale)
		{
			scales += value;
			GD.Print("Loot changed ... : " + lootType + " " + scales);
			SetLoot(lootType, scales);
		}
	}
	
	public void PlayerWasHurt(int damage)
	{
		EmitSignal(SignalName.PlayerHurt, damage);
	}
	
	public void UpdatePlayerHealth(int health)
	{
		EmitSignal(SignalName.PlayerHealthChanged, health);
	}
}