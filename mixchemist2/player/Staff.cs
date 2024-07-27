using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using static ClassesAndEnums;

public partial class Staff : Node2D
{
    [Export] private PackedScene PlayerInputFieldScene;
    [Export] private PackedScene elementStorageScene;
    [Export] private PackedScene bulletScene;
    [Export] float bulletSpeed = 1500f;
    [Export] private float bps = 5;
    [Export] float damage = 10;

    private ElementStorage elementStorage;
    private PlayerInputField playerInput;
    
    private Element currentElement;
    private float fireRate;
    private bool castingMode = false;
    private bool[] castingArray = new bool[4]; // 0: fire, 1: water, 2: earth, 3: air
    private float timeUntilFire = 0f;
    private bool spellReadyToCast = false; 

    public override void _Ready()
    {
        elementStorage = (ElementStorage)elementStorageScene.Instance();
        playerInput = (PlayerInputField)PlayerInputFieldScene.Instance();
        fireRate = 1 / bps;
        ResetCastingArray(false);
    }

    private void ResetCastingArray(bool toggleColors)
    {

        if (toggleColors)
        {
            elementStorage.ToggleElementPanelColor(Element.FIRE, false);
            elementStorage.ToggleElementPanelColor(Element.WATER, false);
            elementStorage.ToggleElementPanelColor(Element.EARTH, false);
            elementStorage.ToggleElementPanelColor(Element.AIR, false);
        }
        
        for (int i = 0; i < castingArray.Length; i++)
        {
            castingArray[i] = false;
        }
    }

    public override void _Input(InputEvent @event)
    {
        
        if(@event.IsActionPressed("ui_cast"))
        {
            castingMode = true;
        }

        if (@event.IsActionReleased("ui_cast"))
        {
            castingMode = false;
            SaveAction();
            ResetCastingArray(true);
        }

        if (castingMode)
        {
         
            if (@event.IsActionPressed("ui_up") && GameManager.Instance.AllowedBasicElements.Contains(Element.FIRE))
            {
                castingArray[0] = !castingArray[0];
                elementStorage.ToggleElementPanelColor(Element.FIRE, castingArray[0]);
            }
            if (@event.IsActionPressed("ui_right") && GameManager.Instance.AllowedBasicElements.Contains(Element.WATER))
            {
                castingArray[1] = !castingArray[1];
                elementStorage.ToggleElementPanelColor(Element.WATER, castingArray[1]);
            }
            if (@event.IsActionPressed("ui_left") && GameManager.Instance.AllowedBasicElements.Contains(Element.EARTH))
            {
                castingArray[2] = !castingArray[2];
                elementStorage.ToggleElementPanelColor(Element.EARTH, castingArray[2]);
            }
            if (@event.IsActionPressed("ui_down") && GameManager.Instance.AllowedBasicElements.Contains(Element.AIR))
            {
                castingArray[3] = !castingArray[3];
                elementStorage.ToggleElementPanelColor(Element.AIR, castingArray[3]);
            }
            
        }
        
        /*if (@event.IsActionPressed("w_cast"))
        {
            playerInput.AddTextToLabel("W"); 
            currentElement = ClassesAndEnums.Element.FIRE;
            spellReadyToCast = true;
        }
        else if (@event.IsActionPressed("a_cast"))
        {
            playerInput.AddTextToLabel("A"); 
            currentElement = ClassesAndEnums.Element.EARTH;
            spellReadyToCast = true;
        }
        else if (@event.IsActionPressed("d_cast"))
        {
            playerInput.AddTextToLabel("D"); 
            currentElement = ClassesAndEnums.Element.WATER;
            spellReadyToCast = true;
        }
        else if (@event.IsActionPressed("s_cast"))
        {
            playerInput.AddTextToLabel("S"); 
            currentElement = ClassesAndEnums.Element.AIR;
            spellReadyToCast = true;
        }*/
        if (@event.IsActionPressed("shootElement"))
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

    private void SaveAction()
    {

        // Only if no spell is selected, set to false
        bool canCast = true;
        
        if (castingArray[0]) // Has fire
        {
            if (castingArray[1]) // Has water
            {
                if (castingArray[2]) // Has earth
                {
                    if (castingArray[3]) // Has air
                    {
                        currentElement = ClassesAndEnums.Element.SHADOW;
                    }
                    else // Has no air
                    {
                        currentElement = ClassesAndEnums.Element.FIRE_WATER_EARTH;
                    }
                }
                else // Has no earth
                {
                    if (castingArray[3]) // Has air
                    {
                        currentElement = ClassesAndEnums.Element.FIRE_WATER_AIR;
                    }
                    else // Has no air
                    {
                        currentElement = ClassesAndEnums.Element.FIRE_WATER;
                    }
                }
            }
            else // Has no water
            {
                if (castingArray[2]) // Has earth
                {
                    if (castingArray[3]) // Has air
                    {
                        currentElement = ClassesAndEnums.Element.FIRE_EARTH_AIR;
                    }
                    else // Has no air
                    {
                        currentElement = ClassesAndEnums.Element.FIRE_EARTH;
                    }
                }
                else // Has no earth
                {
                    if (castingArray[3]) // Has air
                    {
                        currentElement = ClassesAndEnums.Element.FIRE_AIR;
                    }
                    else // Has no air
                    {
                        currentElement = ClassesAndEnums.Element.FIRE;
                    }
                }
            }
        }
        else // Has no fire
        {
            if (castingArray[1]) // Has water
            {
                if (castingArray[2]) // Has earth
                {
                    if (castingArray[3]) // Has air
                    {
                        currentElement = ClassesAndEnums.Element.WATER_EARTH_AIR;
                    }
                    else // Has no air
                    {
                        currentElement = ClassesAndEnums.Element.WATER_EARTH;
                    }
                }
                else // Has no earth
                {
                    if (castingArray[3]) // Has air
                    {
                        currentElement = ClassesAndEnums.Element.WATER_AIR;
                    }
                    else // Has no air
                    {
                        currentElement = ClassesAndEnums.Element.WATER;
                    }
                }
            }
            else // Has no water
            {
                if (castingArray[2]) // Has earth
                {
                    if (castingArray[3]) // Has air
                    {
                        currentElement = ClassesAndEnums.Element.EARTH_AIR;
                    }
                    else // Has no air
                    {
                        currentElement = ClassesAndEnums.Element.EARTH;
                    }
                }
                else // Has no earth
                {
                    if (castingArray[3]) // Has air
                    {
                        currentElement = ClassesAndEnums.Element.AIR;
                    }
                    else // Has no air
                    {
                        canCast = false;
                    }
                }
            }
        }

        if (canCast)
        {
            spellReadyToCast = true;
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