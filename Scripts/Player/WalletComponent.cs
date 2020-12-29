using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[System.Serializable]
public struct WalletComponent : IComponentData
{
    //how much gold the player has
    public float gold;
}

[System.Serializable]
public struct GoldMultiplierComponent : IComponentData
{
    public float multi;
}

//public int[] func
//public int* func
//public int& func
//public int*& func