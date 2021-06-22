using UnityEngine;

[System.Serializable]
public class Item 
{
    public string itemName;
    public Sprite polaroidIMG, objectIMG;

    public Item(string name, Sprite polarImg, Sprite objImg)
    {
        itemName = name;
        polaroidIMG = polarImg;
        objectIMG = objImg;
    }
}
