    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[System.Serializable]
public struct CycleComponent: IComponentData
{
    //in seconds how ofted does it give out gold
    public float cycleDuration;
    //How long has it gone without reward
    public float elapsedTime;
}
[System.Serializable]
public struct GoldRewardComponent: IComponentData
{
    //gold reward upon compleating a cycle
    public int goldReward;
    //should add gold on next tick
    public bool rewardIsReady;
}

[System.Serializable]
//split it more!!
public struct BuildingUpgradeComponent : IComponentData
{
    //cost for current upgrade
    public int upgradeCost;
    //rate at which the cost to upgrade increases 
    public float upgradeRate;
    //rate at which the reward increases
    public float rewardIncreaseRate;
    //number of upgrades
    public int upgradeCount;
}

[System.Serializable]
public struct TimeManipulationComponent: IComponentData
{
    public float timeMod;
}

[System.Serializable]
public struct GoldManipulationComponent : IComponentData
{
    public float goldMod;
}

[System.Serializable]
public struct BuildingClickInfoCompontnet: IComponentData
{
    public int panelId;
}

[System.Serializable]
public struct ScheduledForUpgrade : IComponentData
{
}

