using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static ClassesAndEnums;

public class ElementStorage : Control
{
    private static GridContainer gridCont;
    
    //TODO change ColorRect to texture which should be shown on Panel
    private static Dictionary<Element, ColorRect> textureOfElement = new ();
    private static List<Panel> castablePanels = new List<Panel>();

    private Vector2 gridPos = new Vector2(20, -10);
    
    public override void _Ready()
    {
        gridCont = GetNode<GridContainer>("Input");
        gridCont.SetPosition(gridCont.RectPosition + gridPos);      

        var nodes = GetNode("Queue").GetChildren();
        Enumerable.Range(0, nodes.Count).ToList().ForEach(x => castablePanels.Add((Panel)nodes[x]));
        castablePanels.Reverse();
        
        //TODO: add texture instead of ColorRect to panel
        castablePanels.ForEach(x =>
        {
            Sprite sprite = x.GetChild<Sprite>(0);
            sprite.Texture = ElementTextureMap[Element.NULL].Item2;
            sprite.Scale *= 0.3f;
        });
        textureOfElement.Add(Element.FIRE, GetNode<ColorRect>("Input/FireHolding"));
        textureOfElement.Add(Element.WATER,GetNode<ColorRect>("Input/WaterHolding"));
        textureOfElement.Add(Element.EARTH, GetNode<ColorRect>("Input/EarthHolding"));
        textureOfElement.Add(Element.AIR, GetNode<ColorRect>("Input/AirHolding"));
    }

    /// <summary>
    /// shift all textures to the right by cycling through all NON-NULL textures 
    /// </summary>
    private void RotateTextures()
    {
        //retrieves all non-null textures in storage
        List<Texture> texturesWithinElement = castablePanels
            .Where(panel => !panel.GetChild<Sprite>(0).Texture.Equals(ElementTextureMap[Element.NULL].Item2))
            .Select(panel => panel.GetChild<Sprite>(0).Texture)
            .ToList();
        
        //don't shift if only 1 texture
        if (texturesWithinElement.Count <= 1) return;
        
        //shift textures in list
        Texture firstColor = texturesWithinElement.First();
        for (int i = 0; i < texturesWithinElement.Count - 1; i++)
        {
            texturesWithinElement[i] = texturesWithinElement[i + 1];
        }
        texturesWithinElement[texturesWithinElement.Count - 1] = firstColor;
        
        //change textures in panels according to the texture list
        for (int i = 0; i < texturesWithinElement.Count; i++)
        {
            castablePanels[i].GetChild<Sprite>(0).Texture = texturesWithinElement[i];
        }
        
        //all other panels get NULL textures
        for (int i = texturesWithinElement.Count; i < castablePanels.Count; i++)
        {
            castablePanels[i].GetChild<Sprite>(0).Texture = ElementTextureMap[Element.NULL].Item2;
        }
    }
    
    /// <summary>
    /// Sets the textures of the element castablePanels to the default color
    /// </summary>
    /// <param name="element">The element that needs to be set</param>
    /// <param name="setTexture">Bool if the color should be set</param>
    public void ToggleElementPanelColor(Element element, bool setTexture) 
    {
        textureOfElement[element].Color = element switch
        {
            Element.FIRE => setTexture ? ElementTextureMap[Element.FIRE].Item1 : ElementTextureMap[Element.NULL].Item1,
            Element.WATER => setTexture ? ElementTextureMap[Element.WATER].Item1 : ElementTextureMap[Element.NULL].Item1,
            Element.EARTH => setTexture ? ElementTextureMap[Element.EARTH].Item1 : ElementTextureMap[Element.NULL].Item1,
            Element.AIR => setTexture ? ElementTextureMap[Element.AIR].Item1 : ElementTextureMap[Element.NULL].Item1,
            _ => throw new ArgumentException("Nee mann falsches element")
        };
    }
    
    /// <summary>
    /// Adds a new element to the storage by removing the first element and adding the last element
    /// </summary>
    /// <param name="newElement">The new elemnt that should be inserted</param>
    public void LifoElement(Element newElement)
    {
        //case where there is no storage left so the last element needs to be removed while the other elements get shifted to the right
        if (castablePanels.All(x => !x.GetChild<Sprite>(0).Texture.Equals(ElementTextureMap[Element.NULL].Item2)))
        {
            RotateTextures();
            castablePanels[castablePanels.Count-1].GetChild<Sprite>(0).Texture = ElementTextureMap[newElement].Item2;
        }
        //case where there is still room for an element so we just add the new element to the first found NULL element 
        else
        { 
            var panel = castablePanels.First(x => x.GetChild<Sprite>(0).Texture.Equals(ElementTextureMap[Element.NULL].Item2));
            panel.GetChild<Sprite>(0).Texture = ElementTextureMap[newElement].Item2;
        }
    }
    
    /// <summary>
    /// Event handler for when the rotate button is pressed
    /// </summary>
    /// <param name="event">The event that needs to be handled</param>
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("rotateElements"))
        {
            RotateTextures();
        }
    }
    
    /// <summary>
    /// Return to cast the first element in storage
    /// </summary>
    /// <returns>texture of the cast element</returns>
    public Texture CastFirstElementInStorage()
    {
        //retrives the texture of the first element
        Texture spellColor = castablePanels[0].GetChild<Sprite>(0).Texture;
        //rotate elements
        RotateTextures();
        //remove casted element if texture exists which can be found on the last index of the array after rotating the textures
        int lastIndex = castablePanels.FindLastIndex(panel => !panel.GetChild<Sprite>(0).Texture.Equals(ElementTextureMap[Element.NULL].Item2));
        if (lastIndex != -1)
            castablePanels[lastIndex].GetChild<Sprite>(0).Texture = ElementTextureMap[Element.NULL].Item2; 
        return spellColor;
    }

    /// <summary>
    /// best soluschon in existenz
    /// </summary>
    /// <param name="delta"></param>
    public override void _Process(float delta)
    {
        gridCont.RectRotation = 45f;
    }
}