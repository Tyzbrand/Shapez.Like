using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackFeature : MonoBehaviour
{
    private Stack<(Action undo, Action redo)> undoStack = new();
    private Stack<(Action undo, Action redo)> redoStack = new();

    private BuildingManager buildingManager;
    private PlayerVariables player;

    private void Start()
    {
        buildingManager = ReferenceHolder.instance.buildingManager;
        player = ReferenceHolder.instance.playervariable;
    }



    public void UndoAfterConstruct(Vector2 pos, BuildingBH building, int rotation)
    {
        if (undoStack.Count >= 15) CapUndo();
        undoStack.Push((undo:() => buildingManager.RemoveBuilding(pos, false), redo: () => { player.rotation = rotation; buildingManager.AddBuilding(pos, building, false); }));
    }

    public void UndoAfterDestruct(BuildingBH building, Vector2 pos, int rotation, RecipeParent recipe = null)
    {
        if (undoStack.Count >= 15) CapUndo();

        if (recipe == null) undoStack.Push((undo: () => { player.rotation = rotation; buildingManager.AddBuilding(pos, building, false); }, redo: () => buildingManager.RemoveBuilding(pos, false)));
        else undoStack.Push(( undo : () =>
        {
            player.rotation = rotation;
            buildingManager.AddBuilding(pos, building, false, () =>
            {
                if (building is Foundry foundry) foundry.currentRecipe = (Recipe11_1)recipe;
                else if (building is Builder builder) builder.currentRecipe = (Recipe1_1)recipe;
            });
        }, redo : () => buildingManager.RemoveBuilding(pos, false)));

    }

    //actions

    public void Undo()
    {
        if (undoStack.Count == 0) return;
        
        var undoAction = undoStack.Pop();

        if (redoStack.Count >= 15) CapRedo(); 
        redoStack.Push(undoAction);

        undoAction.undo.Invoke();
    }

    public void Redo()
    {
        if (redoStack.Count == 0) return;
        
        var redoAction = redoStack.Pop();

        if (undoStack.Count >= 15) CapUndo(); 
        undoStack.Push(redoAction);

        redoAction.redo.Invoke();
    }

    //Utilitaire

    private void CapUndo()
    {
        var tempList = undoStack.ToList();
        tempList.RemoveAt(tempList.Count - 1);
        undoStack = new Stack<(Action undo, Action redo)>(tempList.Reverse<(Action undo, Action redo)>());
    }

    private void CapRedo()
    {
        var tempList = redoStack.ToList();
        tempList.RemoveAt(tempList.Count - 1);
        redoStack = new Stack<(Action undo, Action redo)>(tempList.Reverse<(Action undo, Action redo)>()); 
    }
    

}
