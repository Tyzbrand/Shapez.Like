using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FoundryRecipe", menuName = "Scriptable Objects/FoundryRecipe")]
public class FoundryRecipe : ScriptableObject
{
    public List<Recipe11_1> foundryRecipes = new List<Recipe11_1>();

}

