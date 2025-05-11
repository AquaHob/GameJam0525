using UnityEngine;
using System;
using System.Collections.Generic;

public class WorkStation : MonoBehaviour
{
    public List<Item> currentHeldComponents = new List<Item>();
    public BabySystem BabySystem;
    public SoundFXManager SoundFXManager;

    public Item AddNewComponent(Item newComponent){
        if(currentHeldComponents.Contains(newComponent)) return newComponent;

        // create temp componentsList with newComponent
        List<Item> tempComponentsList = new List<Item>();
        foreach(Item cI in currentHeldComponents){
            tempComponentsList.Add(cI);
        }
        tempComponentsList.Add(newComponent);



        if(!BabySystem.isHappy && tempComponentsList.Count <= 2){
            // Check Happiness Item
            Item cHapItem = BabySystem.GetCurrentHappinessItem();
            if(cHapItem.CheckWithComponentsList(tempComponentsList)){
                if(currentHeldComponents.Count+1 >= cHapItem.componentsList.Count){
                    // correct and Item is Finished
                    ClearWorkSpace();
                    return cHapItem;
                }else{
                    // correct but not finished yet
                    currentHeldComponents.Add(newComponent);
                    return null;
                }
            }
        }


        // Check Main Item
        Item cMainItem = BabySystem.GetCurrentMainItem();
        if(cMainItem.CheckWithComponentsList(tempComponentsList)){
            if(currentHeldComponents.Count+1 >= cMainItem.componentsList.Count){
                // correct and Item is Finished
                ClearWorkSpace();
                return cMainItem;
            }else{
                // correct but not finished yet
                currentHeldComponents.Add(newComponent);
                return null;
            }
        }

        // incorrect
        return newComponent;
    }

    private void PlayCombineSuccessSound(){
        // SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.combineSuccessSound, transform, 1.0f);
    }

    private void PlayCombineFailedSound(){
        // SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.combineFailureSound, transform, 1.0f);
    }

    public void ClearWorkSpace(){
        currentHeldComponents = new List<Item>();
    }
}
