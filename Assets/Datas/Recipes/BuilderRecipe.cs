using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BuilderRecipe", menuName = "Scriptable Objects/BuilderRecipe")]
public class BuilderRecipe : ScriptableObject
{
    public List<Recipe1_1> BuilderRecipes = new List<Recipe1_1>();
}
