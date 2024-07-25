using System.Diagnostics;
using Godot;
using static ClassesAndEnums;

public partial class Staff : Node2D
{
    [Export] private PackedScene playerInputScene;
    [Export] private PackedScene elementStorageScene;
    [Export] private PackedScene bulletScene;
    [Export] float bulletSpeed = 1500f;
    [Export] private float bps = 5;
    [Export] float damage = 10;

    private ElementStorage elementStorage;
    private Label playerInput;
    
    private Element currentElement;
    private float fireRate;
    private float timeUntilFire = 0f;
    private bool spellReadyToCast = false; 

    public override void _Ready()
    {
        elementStorage = (ElementStorage)elementStorageScene.Instance();
        playerInput = (Label)playerInputScene.Instance();
        fireRate = 1 / bps;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("w_cast"))
        {
            playerInput.Text += "W";
            currentElement = ClassesAndEnums.Element.FIRE;
            spellReadyToCast = true;
        }
        else if (@event.IsActionPressed("a_cast"))
        {
            playerInput.Text += "A";
            currentElement = ClassesAndEnums.Element.EARTH;
            spellReadyToCast = true;
        }
        else if (@event.IsActionPressed("d_cast"))
        {
            playerInput.Text += "D";
            currentElement = ClassesAndEnums.Element.WATER;
            spellReadyToCast = true;
        }
        else if (@event.IsActionPressed("s_cast"))
        {
            playerInput.Text += "S";
            currentElement = ClassesAndEnums.Element.AIR;
            spellReadyToCast = true;
        }
        else if (@event.IsActionPressed("shootElement"))
        {
            ColorRect colorRect = null;
            if((colorRect=elementStorage.CastFirstElementInStorage())!=null)
            {
                RigidBody2D spell = bulletScene.Instance<RigidBody2D>();
                spell.Modulate = colorRect.Color;
                spell.Rotation = GlobalRotation;
                spell.GlobalPosition = GlobalPosition;
                spell.LinearVelocity = spell.Transform.x * bulletSpeed;
                GetTree().Root.AddChild(spell);
            };
        }
    }

    public override void _Process(float delta)
    {
        if (timeUntilFire <= fireRate)
        {
            timeUntilFire += delta;
        }

        if (spellReadyToCast && timeUntilFire > fireRate)
        {
            timeUntilFire = 0;
            spellReadyToCast = false;
            elementStorage.StoreSpellColor(currentElement);
        }
    }
}