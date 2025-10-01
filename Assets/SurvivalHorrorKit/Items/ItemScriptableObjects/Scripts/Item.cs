using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string Name; //Name of the item
    public Sprite itemSprite; //Sprite of the item
    public GameObject itemPrefab; //Prefab of the item
    public bool isStackable; //Set if item can be stacked or not
    public bool isDroppable; //Set if item can be dropped or not
    public int maxItems; //Item's max spawn value
}