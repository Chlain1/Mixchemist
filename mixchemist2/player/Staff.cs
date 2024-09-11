using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;
using mixchemist2.spell;
using static ClassesAndEnums;

public partial class Staff : Node2D
{
    [Export] private PackedScene PlayerInputFieldScene;
    [Export] private PackedScene elementStorageScene;
    [Export] private PackedScene bulletScene;
    [Export] private PackedScene elementGaugeScene;
    [Export] float bulletSpeed = 1500f;
    [Export] private float bps = 5;
    [Export] float damage = 10;
    [Export] int CASTING_COST = 5;

    private ElementStorage elementStorage;
    private PlayerInputField playerInput;
    
    private Element currentElement;
    private float fireRate;
    private bool castingMode = false;
    private bool[] castingArray = new bool[4]; // 0: fire, 1: water, 2: earth, 3: air
    private float timeUntilFire = 0f;
    private bool spellReadyToCast = false;
    private int fireStored = 100;
    private int waterStored = 100;
    private int earthStored = 100;
    private int airStored = 100;

    private ElementGauge elementGauge;

    public override void _Ready()
    {
        elementStorage = (ElementStorage)elementStorageScene.Instance();
        playerInput = (PlayerInputField)PlayerInputFieldScene.Instance();
        elementGauge = (ElementGauge)elementGaugeScene.Instance();
        elementGauge.UpdateGauge(Element.FIRE, fireStored);
        elementGauge.UpdateGauge(Element.WATER, waterStored);
        elementGauge.UpdateGauge(Element.EARTH, earthStored);
        elementGauge.UpdateGauge(Element.AIR, airStored);

        fireRate = 1 / bps;
        ResetCastingArray(false);
    }

    /// <summary>
    /// Resets the casting array to false.
    /// </summary>
    /// <param name="toggleColors">Boolean if the element is toggled</param>
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

    /// <summary>
    /// Event based Input function for the player.
    /// </summary>
    /// <param name="event">Input Event from the player</param>
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
         
            if (@event.IsActionPressed("ui_up") && GameManager.Instance.AllowedBasicElements.Contains(Element.FIRE) && fireStored >= CASTING_COST)
            {
                castingArray[0] = !castingArray[0];
                elementStorage.ToggleElementPanelColor(Element.FIRE, castingArray[0]);
                if (castingArray[0])
                {
                    fireStored -= CASTING_COST;
                    elementGauge.UpdateGauge(Element.FIRE, fireStored);
                }
                else
                {
                    fireStored += CASTING_COST;
                    elementGauge.UpdateGauge(Element.FIRE, fireStored);
                }
            }
            if (@event.IsActionPressed("ui_right") && GameManager.Instance.AllowedBasicElements.Contains(Element.WATER) && waterStored >= CASTING_COST)
            {
                castingArray[1] = !castingArray[1];
                elementStorage.ToggleElementPanelColor(Element.WATER, castingArray[1]);
                if (castingArray[1])
                {
                    waterStored -= CASTING_COST;
                    elementGauge.UpdateGauge(Element.WATER, waterStored);
                }
                else
                {
                    waterStored += CASTING_COST;
                    elementGauge.UpdateGauge(Element.WATER, waterStored);
                }
            }
            if (@event.IsActionPressed("ui_left") && GameManager.Instance.AllowedBasicElements.Contains(Element.EARTH) && earthStored >= CASTING_COST)
            {
                castingArray[2] = !castingArray[2];
                elementStorage.ToggleElementPanelColor(Element.EARTH, castingArray[2]);
                if (castingArray[2])
                {
                    earthStored -= CASTING_COST;
                    elementGauge.UpdateGauge(Element.EARTH, earthStored);
                } 
                else
                {
                    earthStored += CASTING_COST;
                    elementGauge.UpdateGauge(Element.EARTH, earthStored);
                }
            }
            if (@event.IsActionPressed("ui_down") && GameManager.Instance.AllowedBasicElements.Contains(Element.AIR) && airStored >= CASTING_COST)
            {
                castingArray[3] = !castingArray[3];
                elementStorage.ToggleElementPanelColor(Element.AIR, castingArray[3]);
                if (castingArray[3])
                {
                    airStored -= CASTING_COST;
                    elementGauge.UpdateGauge(Element.AIR, airStored);
                }
                else 
                { 
                    airStored += CASTING_COST;
                    elementGauge.UpdateGauge(Element.AIR, airStored);
                }
            }
            
        }
        
        if (@event.IsActionPressed("shootElement"))
        {
            Texture spellInStorage = elementStorage.CastFirstElementInStorage();
            if(!spellInStorage.Equals(ElementTextureMap[Element.NULL].Item2))
            {
                ConcreteSpell spell = bulletScene.Instance<ConcreteSpell>();
                spell.SetElement(ElementTextureMap.Keys.First(x => ElementTextureMap[x].Item2.Equals(spellInStorage)));
                var sprite = spell.GetChild<Sprite>(0);
                sprite.Texture = spellInStorage;
                //sprite.Scale = new Vector2(spell.Scale.x / sprite.Texture.GetWidth(), spell.Scale.y / sprite.Texture.GetHeight());
                
                spell.Rotation = GlobalRotation;
                spell.GlobalPosition = GlobalPosition;
                spell.LinearVelocity = spell.Transform.x * bulletSpeed;
                GetTree().Root.AddChild(spell);
            }
        }
    }

    /// <summary>
    /// DKNF of all the elements to determine the current element.
    /// </summary>
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
            elementStorage.LifoElement(currentElement);
        }
    }
}