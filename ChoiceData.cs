using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Choice", menuName = "Scriptable Object/ChoiceData")]
public class ChoiceData : ScriptableObject
{
    public enum ItemType { Bullet, Antenna, Bag, Rocket, Helmet, HealthPotion, StarPotion }

    [Header("#Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public int[] stardustNeed;
    [TextArea]
    public string[] itemDesc;
    

    [Header("#Weapon")]
    public GameObject prefab;
    public Sprite sprite;
}
