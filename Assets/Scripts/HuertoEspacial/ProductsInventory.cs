using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsInventory : MonoBehaviour
{
    public static ProductsInventory instance;

    public Dictionary<Products, int> inventarioProductos = new Dictionary<Products, int>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        inventarioProductos = new Dictionary<Products, int>()
        {
            { Products.Cabbage, 0 },
            { Products.Lettuce, 0 },
            { Products.Beet, 0 },
            { Products.Cilantro, 0 },
            { Products.Parsley, 0 },
            { Products.Celery, 0 },
            { Products.Pumpkin, 0 },
            { Products.Pepper, 0 },
            { Products.Carrot, 0 },
            { Products.Chard, 0 },
            { Products.SweetPotato, 0 }
        };
    }

    
}

public enum Products
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
    SweetPotato
}
