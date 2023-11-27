using System;
using GenericPlatforformer.Shop;
using Godot;

namespace GenericPlatforformer;

public partial class GlobalState : Node
{
    private int scales = 0;
    private Node2D _respawnPoint = null;

    private int _powersState = 0;

    public int Scales
    {
        get => scales;
        set => scales = value;
    }

    
    public bool HasPower(Powers power)
    {
        var hasPower =  (_powersState & (int)power) == (int)power;
        GD.Print("Has power: " + power + " - " +  hasPower );
        return hasPower;
    }

    public void BuyPower(Powers power)
    {
        _powersState += (int)power;
        SetPower(power);
        GetTree().Root.GetNode<CanvasLayer>("Game/Level/Shop").Visible = false;
    }

    
    [Signal]
    public delegate void CloseDialogEventHandler();
    
    [Signal]
    public delegate void PowerChangedEventHandler(int power);

    public void SetPower(Powers power)
    {
        EmitSignal(SignalName.PowerChanged, (int)power);
    }

    [Signal]
    public delegate void PlayerHurtEventHandler(int damage);


    [Signal]
    public delegate void PlayerHealthChangedEventHandler(int health);

    [Signal]
    public delegate void SwitchLevelEventHandler(string nextlvl);


    [Signal]
    public delegate void SpawnLootEventHandler(LootTypes lootType, Vector2 position);


    [Signal]
    public delegate void LootPickupEventHandler(LootTypes lootType, int value);

    public void SetRespawnPoint(Node2D point)
    {
        _respawnPoint = point;
    }

    [Signal]
    public delegate void RespawnEventHandler(Node2D point);

    [Signal]
    public delegate void LootChangedEventHandler(string lootType, int value);

    public void SwitchLevelTo(string lvl)
    {
        EmitSignal(SignalName.SwitchLevel, lvl);
    }
    
    public void SpawnLootAtPos(LootTypes lootType, Vector2 position)
    {
        EmitSignal(SignalName.SpawnLoot, lootType.ToString(), position);
    }

    public void FireCloseDialog()
    {
        EmitSignal(SignalName.CloseDialog);
    }


    public void RespawnPlayer()
    {
        EmitSignal(SignalName.Respawn, _respawnPoint);
    }

    public void SetLoot(LootTypes lootType, int value)
    {
        EmitSignal(SignalName.LootChanged, lootType.ToString(), value);
    }


    public void PickupLoot(LootTypes lootType, int value)
    {
        if (lootType == LootTypes.Scale)
        {
            Scales += value;
            SetLoot(lootType, Scales);
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