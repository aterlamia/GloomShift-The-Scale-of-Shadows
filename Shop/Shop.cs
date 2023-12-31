using Godot;
using GenericPlatforformer;
using GenericPlatforformer.Shop;

public partial class Shop : Control
{
    private GlobalState _globalState = null;
    private int currentActive = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _globalState = GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState");
        _globalState.Connect("PowerChanged", new Callable(this, "close"));
    }

    private void close(int power)
    {
        GetParent<CanvasLayer>().Visible = false;
    }

    private void _on_buy_button_pressed()
    {
        var sidebar =
            GetNode<Node>(
                "NinePatchRect/MarginContainer/HSplitContainer/AspectRatioContainer/Panel/MarginContainer/Sidebar");
        sidebar.GetNode<Button>("MarginContainer/BuyButton").Disabled = false;

        if (currentActive == 1)
        {
            _globalState.BuyPower(Powers.SeperareShadow);
            _globalState.SetLoot (LootTypes.Scale,_globalState.Scales - 1);
        }
        if (currentActive == 2)
        {
            _globalState.BuyPower(Powers.ShrinkShadow);
            _globalState.SetLoot (LootTypes.Scale,_globalState.Scales - 2);
        }
        
        if (currentActive == 3)
        {
            _globalState.BuyPower(Powers.DualJump);
            _globalState.SetLoot (LootTypes.Scale,_globalState.Scales - 4);
        }
    }

    public void _on_button_pressed()
    {
       GetParent<CanvasLayer>().Visible = false;
    }

    public void checkBuyable()
    {
        // GetNode<TextureButton>(
        //             "NinePatchRect/MarginContainer/HSplitContainer/VSplitContainer/GridContainer/Sepperate/Panel/AspectRatioContainer/SeperateButton")
        //         .Disabled =
        //     true;
        // GetNode<TextureButton>(
        //             "NinePatchRect/MarginContainer/HSplitContainer/VSplitContainer/GridContainer/Shrink/Panel/AspectRatioContainer2/ShrinkButton")
        //         .Disabled =
        //     true;
        //
        // GetNode<TextureButton>(
        //             "NinePatchRect/MarginContainer/HSplitContainer/VSplitContainer/GridContainer/Double/Panel/AspectRatioContainer2/DoubleButton")
        //         .Disabled =
        //     true;
        // if (_globalState.Scales > 0 && !_globalState.HasPower(Powers.SeperareShadow))
        // {
        //     GetNode<TextureButton>(
        //                 "NinePatchRect/MarginContainer/HSplitContainer/VSplitContainer/GridContainer/Sepperate/Panel/AspectRatioContainer/SeperateButton")
        //             .Disabled =
        //         false;
        // }
        //
        // if (_globalState.Scales > 1 && !_globalState.HasPower(Powers.ShrinkShadow))
        // {
        //     GetNode<TextureButton>(
        //                 "NinePatchRect/MarginContainer/HSplitContainer/VSplitContainer/GridContainer/Shrink/Panel/AspectRatioContainer2/ShrinkButton")
        //             .Disabled =
        //         false;
        // }
        //
        //
        // if (_globalState.Scales > 3 && !_globalState.HasPower(Powers.DualJump))
        // {
        //     GetNode<TextureButton>(
        //                 "NinePatchRect/MarginContainer/HSplitContainer/VSplitContainer/GridContainer/Double/Panel/AspectRatioContainer2/DoubleButton")
        //             .Disabled =
        //         false;
        // }
        //
        // var sidebar =
        //     GetNode<Node>(
        //         "NinePatchRect/MarginContainer/HSplitContainer/AspectRatioContainer/Panel/MarginContainer/Sidebar");
        // sidebar.GetNode<RichTextLabel>("MarginContainer2/RichTextLabel").Text = "";
        // sidebar.GetNode<RichTextLabel>("MarginContainer4/RichTextLabel").Text = "";
        // sidebar.GetNode<RichTextLabel>("MarginContainer3/RichTextLabel").Text = "";
        // sidebar.GetNode<Button>("MarginContainer/BuyButton").Disabled = true;
    }

    private void _on_SolidShadow_pressed()
    {
        showData(1);
        currentActive = 1;

    }

    private void _on_shrink_button_pressed()
    {
        showData(2);
        currentActive = 2;
    }

    private void _on_double_button_pressed()
    {
        showData(3);
        currentActive = 3;
    }
    
    // make this better ... 
    private void showData(int type)
    {
        var title = "";
        var text = "";
        var text2 = "";
        var price = 0;

        switch (type)
        {
            case 1:
                title = "Solid Shadow";
                text =
                    "With this power-up, you sever the unbreakable link with your shadow while gaining the ability to manipulate it at will. Unshackle yourself from the past synergy and watch as your detached shadow becomes a loyal ally.";
                text2 = "Press [W] to separate your shadow.  \nCost: 1 scale";
                price = 1;
                break;
            case 2:
                title = "Shrink Shadow";
                text =
                    "Shrink your shadow, expand your possibilities! Harness the might of this power-up to dynamically alter the size of your shadow based on surrounding light sources. Diminish your shadow's scale to a fraction, slipping through tight spaces.";
                text2 = "Press [W] to separate your shadow near a light source.  \nCost: 2 scales";
                price = 2;
                break;
            case 3:
                title = "Double Jump";
                text =
                    "Need that extra height? Your shadow will give you a boost! With this power-up, you can jump twice in the air. Use it to reach new heights and explore new areas.";
                text2 = "Press [Up] mid air and your shadow will give you a boost.  \nCost: 4 scales";
                price = 4;
                break;
        }

        var sidebar =
            GetNode<Node>(
                "NinePatchRect/MarginContainer/HSplitContainer/AspectRatioContainer/Panel/MarginContainer/Sidebar");
        sidebar.GetNode<RichTextLabel>("MarginContainer2/RichTextLabel").Text = title;
        sidebar.GetNode<RichTextLabel>("MarginContainer4/RichTextLabel").Text = text;
        sidebar.GetNode<RichTextLabel>("MarginContainer3/RichTextLabel").Text = text2;
        sidebar.GetNode<Button>("MarginContainer/BuyButton").Disabled = true;
        if (_globalState.Scales > 0 && !_globalState.HasPower(Powers.SeperareShadow) && type == 1)
        {
            sidebar.GetNode<Button>("MarginContainer/BuyButton").Disabled = false;
        }

        if (_globalState.Scales > 1 && _globalState.HasPower(Powers.SeperareShadow)  && !_globalState.HasPower(Powers.ShrinkShadow)&& type == 2)
        {
            GetNode<TextureButton>(
                        "NinePatchRect/MarginContainer/HSplitContainer/VSplitContainer/GridContainer/Shrink/Panel/AspectRatioContainer2/ShrinkButton")
                    .Disabled =
                false;
            sidebar.GetNode<Button>("MarginContainer/BuyButton").Disabled = false;
        }
        
        if (_globalState.Scales > 3 && _globalState.HasPower(Powers.SeperareShadow) && !_globalState.HasPower(Powers.DualJump)&& type == 3)
        {
            GetNode<TextureButton>(
                        "NinePatchRect/MarginContainer/HSplitContainer/VSplitContainer/GridContainer/Double/Panel/AspectRatioContainer2/DoubleButton")
                    .Disabled =
                false;
            sidebar.GetNode<Button>("MarginContainer/BuyButton").Disabled = false;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}