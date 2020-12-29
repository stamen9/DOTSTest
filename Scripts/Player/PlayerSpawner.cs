using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class PlayerSpawner : MonoBehaviour
{
    public static Entity PlayerEntity;

    // Start is called before the first frame update
    void Awake()
    {
        World testWorld = new World("Test World");
        //SpawnNewPlayer();
    }

    public void SpawnNewPlayer()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype playerArchetype = entityManager.CreateArchetype(
                typeof(WalletComponent),
                typeof(GoldMultiplierComponent),
                typeof(Translation)
            );
        PlayerEntity = entityManager.CreateEntity(playerArchetype);
        entityManager.SetComponentData(PlayerEntity, new WalletComponent
        {
            gold = 0
        });
        entityManager.SetComponentData(PlayerEntity, new GoldMultiplierComponent
        {
            multi = 1
        });
    }
}
