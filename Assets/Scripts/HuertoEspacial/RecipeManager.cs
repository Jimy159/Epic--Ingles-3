using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public Dictionary<Products, List<string>> plantRecipes = new Dictionary<Products, List<string>>();

    void Start()
    {
        InitializeRecipes();
    }

    void InitializeRecipes()
    {
        plantRecipes.Add(Products.Cabbage, new List<string> { "algo con colMorada" });
        plantRecipes.Add(Products.Lettuce, new List<string> { "algo con colBlanca" });
        plantRecipes.Add(Products.Beet, new List<string> { "tacos" });
        plantRecipes.Add(Products.Cilantro, new List<string> { "Bocaditos  para 10 personas.." });
        plantRecipes.Add(Products.Parsley, new List<string> { "Salsa de remolacha" });
        plantRecipes.Add(Products.Celery, new List<string> { "algo con culantro" });
        plantRecipes.Add(Products.Pumpkin, new List<string> { "algo con perejil" });
        plantRecipes.Add(Products.Pepper, new List<string> { "Crema de apio con queso mozzarella, Palitos de apio" });
        plantRecipes.Add(Products.Carrot, new List<string> { "algo con calabaz" });
        plantRecipes.Add(Products.Chard, new List<string> { "algo con pimiento" });
        plantRecipes.Add(Products.SweetPotato, new List<string> { "algo con zanahoria" });
    }

    public List<string> GetRecipes(Products plant)
    {
        if (plantRecipes.ContainsKey(plant))
        {
            return plantRecipes[plant];
        }
        return new List<string>();
    }
}
