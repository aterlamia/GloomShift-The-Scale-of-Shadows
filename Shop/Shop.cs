using Godot;
using GenericPlatforformer;
using GenericPlatforformer.Shop;

public partial class Shop : Control
{
    private GlobalState _globalState = null;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //assign globalstate to a private variable
        // if (GlobalState.Instance != null)
        _globalState = GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState");


        if (_globalState.HasPower(Powers.SolidShadow))
        {
            GetNode<TextureButton>(
                    "NinePatchRect/MarginContainer/HSplitContainer/VSplitContainer/GridContainer/Sepperate").Disabled =
                false;
        }
        // if()
        // "Shop/:
    }


    private void _on_SolidShadow_pressed()
    {
        _globalState.BuyPower(Powers.SeperareShadow);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}