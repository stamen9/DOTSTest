using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using System;

public class BuildingUpgradeSystem : SystemBase
{
    EndSimulationEntityCommandBufferSystem m_EndSimulationEcbSystem;

    public event EventHandler OnGoldChanged; 

    private EntityQuery m_BuildingQuery;
    private EntityQuery m_WalletQuery;


    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEcbSystem = World
            .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        var queryDescription0 = new EntityQueryDesc
        {
            All = new ComponentType[] { typeof(WalletComponent) },
        };
        m_WalletQuery = GetEntityQuery(new EntityQueryDesc[] { queryDescription0 });

    }
    private void ReQuery()
    { 
        var queryDescription1 = new EntityQueryDesc
        {
            All = new ComponentType[] { typeof(ScheduledForUpgrade),typeof(BuildingUpgradeComponent) },
            Any = new ComponentType[] { typeof(GoldRewardComponent),typeof(TimeManipulationComponent),typeof(GoldManipulationComponent)}
        };
        m_BuildingQuery = GetEntityQuery(new EntityQueryDesc[] { queryDescription1 });
    }

    protected override void OnUpdate()
    {
        ReQuery();

        NativeArray<Entity> buildingArray =
            m_BuildingQuery.ToEntityArray(Unity.Collections.Allocator.TempJob);
        NativeArray<Entity> walletComponents = 
            m_WalletQuery.ToEntityArray(Unity.Collections.Allocator.TempJob);
        //not really paralel so losing the point of the System a bit?
        //if done with Entities.ForEach would couse raice conditions(probably?).
        for (int i = 0; i < buildingArray.Length; i++)
        {
            int curGold = 
                (int)EntityManager.GetComponentData<WalletComponent>(walletComponents[0]).gold;
            BuildingUpgradeComponent BuildingUpgradeComponent =
                EntityManager.GetComponentData<BuildingUpgradeComponent>(buildingArray[i]);
            if (curGold >= BuildingUpgradeComponent.upgradeCost)
            {
                EntityManager.SetComponentData(walletComponents[0], new WalletComponent 
                {
                    gold = curGold - BuildingUpgradeComponent.upgradeCost,
                });
                OnGoldChanged?.Invoke(this, new GoldChangedEventArgs { goldChange = - BuildingUpgradeComponent.upgradeCost });
                EntityManager.SetComponentData(buildingArray[i], new BuildingUpgradeComponent
                {
                    rewardIncreaseRate = BuildingUpgradeComponent.rewardIncreaseRate,
                    upgradeCount = BuildingUpgradeComponent.upgradeCount + 1,
                    upgradeCost = ((int)(BuildingUpgradeComponent.upgradeCost * BuildingUpgradeComponent.upgradeRate)),
                    upgradeRate = BuildingUpgradeComponent.upgradeRate,
                });
                if (EntityManager.HasComponent<GoldRewardComponent>(buildingArray[i]))
                {
                    GoldRewardComponent oldData = EntityManager.GetComponentData<GoldRewardComponent>(buildingArray[i]);
                    EntityManager.SetComponentData(buildingArray[i], new GoldRewardComponent
                    {
                        goldReward = (int)(oldData.goldReward * BuildingUpgradeComponent.rewardIncreaseRate),
                        rewardIsReady = oldData.rewardIsReady,
                    });
                }
                if(EntityManager.HasComponent<TimeManipulationComponent>(buildingArray[i]))
                {
                    float multi = EntityManager.GetComponentData<TimeManipulationComponent>(buildingArray[i]).timeMod;
                    World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildingTickSystem>().ModifyTimeMulti(multi);
                }
                if (EntityManager.HasComponent<GoldManipulationComponent>(buildingArray[i]))
                {
                    GoldMultiplierComponent currentGoldMod = EntityManager.GetComponentData<GoldMultiplierComponent>(walletComponents[0]);
                    EntityManager.SetComponentData(walletComponents[0], new GoldMultiplierComponent
                    {
                        multi = currentGoldMod.multi * (EntityManager.GetComponentData<GoldManipulationComponent>(buildingArray[i]).goldMod),
                    });
                }
                UIUpdateSystem.Instance.SetNeedsUpdate(EntityManager.GetComponentData<BuildingUpgradeComponent>(buildingArray[i]));
            }
            EntityManager.RemoveComponent(buildingArray[i], typeof(ScheduledForUpgrade));
        }
        buildingArray.Dispose();
        walletComponents.Dispose();
    }
}
