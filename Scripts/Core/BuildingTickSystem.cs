using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class BuildingTickSystem : SystemBase
{
    EntityManager entityManager;
    protected override void OnCreate()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        base.OnCreate();
    }

    private float TimeMulti = 1f;

    public void ModifyTimeMulti(float multi)
    {
        TimeMulti *= multi;
    }

    protected override void OnUpdate()
    {
        //ensures that they don't get mutated mid job
        float dt = Time.DeltaTime;
        float tm = TimeMulti;
        Entities.ForEach((BuildingUpgradeComponent buildingUpgradeComponent, ref CycleComponent cycleComoponent, ref GoldRewardComponent goldRewardComponent) =>
        {
            goldRewardComponent.rewardIsReady = false;
            //How to keep track of multy?
            
            cycleComoponent.elapsedTime += dt * (buildingUpgradeComponent.upgradeCount < 1 ? 0f : 1f) * tm;
            if (cycleComoponent.cycleDuration < cycleComoponent.elapsedTime)
            {
                goldRewardComponent.rewardIsReady = true;
                cycleComoponent.elapsedTime = 0f;
            }
        }).ScheduleParallel();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
