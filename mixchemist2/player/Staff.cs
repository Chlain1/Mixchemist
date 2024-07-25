using System.Diagnostics;
using Godot;
using static ClassesAndEnums;

public partial class Staff : Node2D
{
    [Export] private PackedScene elementStorageScene;
    [Export] private PackedScene bulletScene;
    [Export] float bulletSpeed = 1500f;
    [Export] private float bps = 5;
    [Export] float damage = 10;

    private ElementStorage elementStorage;

    //[Signal]
    //public delegate void StoreSpell(Element storedElement);
    
    private Element currentElement;
    private float fireRate;
    private float timeUntilFire = 0f;
    private bool spellReadyToCast = false; 

    public override void _Ready()
    {
        elementStorage = (ElementStorage)elementStorageScene.Instance();
        fireRate = 1 / bps;
        //Connect(nameof(StoreSpell), elementStorage, nameof(ElementStorage.StoreSpellColor));
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("w_cast"))
        {
            currentElement = ClassesAndEnums.Element.FIRE;
            spellReadyToCast = true;
        }
        else if (@event.IsActionPressed("a_cast"))
        {
            currentElement = ClassesAndEnums.Element.EARTH;
            spellReadyToCast = true;
        }
        else if (@event.IsActionPressed("d_cast"))
        {
            currentElement = ClassesAndEnums.Element.WATER;
            spellReadyToCast = true;
        }
        else if (@event.IsActionPressed("s_cast"))
        {
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