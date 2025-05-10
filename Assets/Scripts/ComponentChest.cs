using UnityEngine;
using System;
using System.Collections.Generic;

public class ComponentChest : MonoBehaviour
{
    [SerializeField] private Item givenComponent;

    public Item GiveComponent(){
        return givenComponent;
    }
}
