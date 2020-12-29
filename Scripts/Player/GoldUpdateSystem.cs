using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class GoldUpdateSystem : SystemBase
{

    public event EventHandler OnGoldChanged; 

    public struct GoldChangedEvent
    {
        public float goldChange;
    }

    NativeQueue<GoldChangedEvent> eventQueue;

    protected override void OnCreate()
    {
        eventQueue = new NativeQueue<GoldChangedEvent>(Allocator.Persistent);


    }

    protected override void OnStartRunning()
    {
        Entities.ForEach((WalletComponent walletComponent) =>
        {
            PlayerResourcesUIHelper ps = GameObject.FindObjectOfType<PlayerResourcesUIHelper>();
            Debug.Log(walletComponent.gold);
            ps.SetGoldToSetAmount(walletComponent.gold);
        }).Run();
    }

    protected override void OnDestroy()
    {
        eventQueue.Dispose();
    }

    protected override void OnUpdate()
    {
        EntityQuery m_Query = GetEntityQuery(ComponentType.ReadOnly<GoldRewardComponent>());
        var goldDataArray = m_Query.ToComponentDataArray<GoldRewardComponent>(Unity.Collections.Allocator.TempJob);
        NativeQueue<GoldChangedEvent>.ParallelWriter eventQueueParallel = eventQueue.AsParallelWriter();

        Entities.ForEach((GoldMultiplierComponent goldMultiplierComponent, ref WalletComponent walletComponent) =>
        {
            for(int i = 0; i < goldDataArray.Length; i++)
            {
                if(goldDataArray[i].rewardIsReady)
                {
                    float goldEarned = goldDataArray[i].goldReward * goldMultiplierComponent.multi;
                    walletComponent.gold += goldEarned;
                    eventQueueParallel.Enqueue(new GoldChangedEvent { goldChange = goldEarned });
                }
            }
        })
            .WithDeallocateOnJobCompletion(goldDataArray)
            .ScheduleParallel();
        
        //not optimal
        this.CompleteDependency();

        while(eventQueue.TryDequeue(out GoldChangedEvent goldChangedEvent))
        {
            GoldChangedEventArgs newEventArgs = new GoldChangedEventArgs();
            newEventArgs.goldChange = goldChangedEvent.goldChange;

            OnGoldChanged?.Invoke(this, newEventArgs);
        }
    }
}

public class GoldChangedEventArgs : EventArgs
{
    public float goldChange;
}