using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FoundryRecipe", menuName = "Scriptable Objects/FoundryRecipe")]
public class FoundryRecipe : ScriptableObject
{
    public List<Recipe> foundryRecipes = new List<Recipe>();

}

