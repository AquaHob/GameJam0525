using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
	public string itemName;
	public int countComponents;
	public List<Item> componentsList = new List<Item>();

	public bool CheckWithComponentsList(List<Item> oComponentsList){
		foreach(Item cI in oComponentsList){
			if(!componentsList.Contains(cI)) return false;
		}

		return true;
	}
    
}