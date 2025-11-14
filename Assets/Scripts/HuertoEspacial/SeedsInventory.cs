using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsInventory : MonoBehaviour
{
    public static SeedsInventory instance;
    public Dictionary<Seed, int> inventorySeeds;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inventorySeeds = new Dictionary<Seed, int>()
        {
            { Seed.Cabbage, 1 },
            { Seed.Lettuce, 1 },
            { Seed.Beet, 1 },
            { Seed.Cilantro, 1 },
            { Seed.Parsley, 1 },
            { Seed.Celery, 1 },
            { Seed.Pumpkin, 1 },
            { Seed.Pepper, 1 },
            { Seed.Carrot, 1 },
            { Seed.Chard, 1 },
            { Seed.SweetPotato, 1 }
        };
    }
    public bool HaveSeeds(Seed seedSelected)
    {
        if (inventorySeeds.ContainsKey(seedSelected))
        {
            return inventorySeeds[seedSelected] > 0;
        }
        else
        {
            return false;
        }
    }
    public void ReduceSeeds(Seed seedSelected)
    {
        if (inventorySeeds.ContainsKey(seedSelected) && inventorySeeds[seedSelected] > 0)
        {
            inventorySeeds[seedSelected] --;
        }
    }
}
public enum Seed
{
    Cabbage,
    Lettuce,
    Beet,
    Cilantro,
    Parsley,
    Celery,
    Pumpkin,
    Pepper,
    Carrot,
    Chard,
    SweetPotato,
}


