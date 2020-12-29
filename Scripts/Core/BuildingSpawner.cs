using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;
using System;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private Mesh buildignMesh = null;
    [SerializeField] private Material catHouseMaterial = null;
    [SerializeField] private Material catRestaurantMaterial = null;
    [SerializeField] private Material catWizardSchoolMaterial = null;
    [SerializeField] private Material catEconomicsSchoolMaterial = null;
    [SerializeField] private Material catAdventureGuildMaterial = null;
    [SerializeField] private Material catUndergroundDungonMaterial = null;
    [SerializeField] private Material catTimeTowerMaterial = null;
    [SerializeField] private Material catTradeGuildMaterial = null;
    [SerializeField] private Material catFishingSpotMaterial = null;
    [SerializeField] private Material catStatueMaterial = null;



    private void Start()
    {
        /*if(World.DefaultGameObjectInjectionWorld.GetExistingSystem<SaveSystem>().LoadProcedure())
        {
            Debug.Log("Loaded Successfully!");
            return;
        }
        //only need to constuct if save file doesn't load successfully
        Debug.Log("Constucting buildings!");
        Construct();*/
    }

    public void Construct()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype buildingArchetype = entityManager.CreateArchetype(
                typeof(BuildingUpgradeComponent),
                typeof(GoldRewardComponent),
                typeof(CycleComponent),
                typeof(RenderMesh),
                typeof(LocalToWorld),
                typeof(Translation),
                typeof(RenderBounds),
                typeof(BuildingClickInfoCompontnet)
            );
        //very bad approach but not sure how similar/differnt they are going to be :S
        ConstructMainCatHouse(entityManager, buildingArchetype);
        ConstructCatRestaurant(entityManager, buildingArchetype);
        ConstructCatWizardSchool(entityManager, buildingArchetype);
        ConstructCatEconomicsSchool(entityManager);
        ConstructCatAdventureGuild(entityManager, buildingArchetype);
        ConstructCatUndergroundDungon(entityManager, buildingArchetype);
        ConstructCatTimeTower(entityManager);
        ConstructCatTradeGuild(entityManager, buildingArchetype);
        ConstructFishingSpot(entityManager, buildingArchetype);
        ConstructCatStatue(entityManager, buildingArchetype);
    }

    private void ConstructCatStatue(EntityManager entityManager, EntityArchetype buildingArchetype)
    {
        Vector3 pos = GetPosRelativeToCamera(0.22f, 0.85f);
        Entity catStatue = entityManager.CreateEntity(buildingArchetype);
        entityManager.SetComponentData(catStatue, new BuildingUpgradeComponent
        {
            rewardIncreaseRate = 2.05f,
            upgradeCost = 100000,
            upgradeCount = 0,
            upgradeRate = 3.17f
        });
        entityManager.SetComponentData(catStatue, new GoldRewardComponent
        {
            goldReward = 1500
        });
        entityManager.SetComponentData(catStatue, new CycleComponent
        {
            cycleDuration = 5f,
            elapsedTime = 0f
        });

        entityManager.SetComponentData(catStatue, new Translation
        {
            Value = pos,
        });

        entityManager.SetComponentData(catStatue, new BuildingClickInfoCompontnet
        {
            panelId = 9,
        });

        Unity.Mathematics.AABB paresdAABB = new Unity.Mathematics.AABB()
        {
            Center = buildignMesh.bounds.center,
            Extents = buildignMesh.bounds.extents,
        };
        entityManager.SetComponentData(catStatue, new RenderBounds
        {
            Value = paresdAABB
        });
        entityManager.SetSharedComponentData(catStatue, new RenderMesh
        {
            mesh = buildignMesh,
            material = catStatueMaterial,
        });
    }

    private void ConstructFishingSpot(EntityManager entityManager, EntityArchetype buildingArchetype)
    {
        Vector3 pos = GetPosRelativeToCamera(0.52f, 0.77f);
        Entity catFishingSpot = entityManager.CreateEntity(buildingArchetype);
        entityManager.SetComponentData(catFishingSpot, new BuildingUpgradeComponent
        {
            rewardIncreaseRate = 1.7f,
            upgradeCost = 10000,
            upgradeCount = 0,
            upgradeRate = 2.1f
        });
        entityManager.SetComponentData(catFishingSpot, new GoldRewardComponent
        {
            goldReward = 700
        });
        entityManager.SetComponentData(catFishingSpot, new CycleComponent
        {
            cycleDuration = 10f,
            elapsedTime = 0f
        });

        entityManager.SetComponentData(catFishingSpot, new Translation
        {
            Value = pos,
        });

        entityManager.SetComponentData(catFishingSpot, new BuildingClickInfoCompontnet
        {
            panelId = 8,
        });

        Unity.Mathematics.AABB paresdAABB = new Unity.Mathematics.AABB()
        {
            Center = buildignMesh.bounds.center,
            Extents = buildignMesh.bounds.extents,
        };
        entityManager.SetComponentData(catFishingSpot, new RenderBounds
        {
            Value = paresdAABB
        });
        entityManager.SetSharedComponentData(catFishingSpot, new RenderMesh
        {
            mesh = buildignMesh,
            material = catFishingSpotMaterial,
        });
    }

    private void ConstructCatTradeGuild(EntityManager entityManager, EntityArchetype buildingArchetype)
    {
        Vector3 pos = GetPosRelativeToCamera(0.77f, 0.75f);
        Entity catTradeGuild = entityManager.CreateEntity(buildingArchetype);
        entityManager.SetComponentData(catTradeGuild, new BuildingUpgradeComponent
        {
            rewardIncreaseRate = 1.55f,
            upgradeCost = 2500,
            upgradeCount = 0,
            upgradeRate = 1.75f
        });
        entityManager.SetComponentData(catTradeGuild, new GoldRewardComponent
        {
            goldReward = 550
        });
        entityManager.SetComponentData(catTradeGuild, new CycleComponent
        {
            cycleDuration = 25f,
            elapsedTime = 0f
        });
        entityManager.SetComponentData(catTradeGuild, new Translation
        {
            Value = pos,
        });

        entityManager.SetComponentData(catTradeGuild, new BuildingClickInfoCompontnet
        {
            panelId = 7,
        });

        Unity.Mathematics.AABB paresdAABB = new Unity.Mathematics.AABB()
        {
            Center = buildignMesh.bounds.center,
            Extents = buildignMesh.bounds.extents,
        };
        entityManager.SetComponentData(catTradeGuild, new RenderBounds
        {
            Value = paresdAABB
        });
        entityManager.SetSharedComponentData(catTradeGuild, new RenderMesh
        {
            mesh = buildignMesh,
            material = catTradeGuildMaterial,
        });
    }

    private void ConstructCatTimeTower(EntityManager entityManager)
    {
        //TBI
        //panel id 6
        Vector3 pos = GetPosRelativeToCamera(0.15f, 0.61f);
        EntityArchetype timeTowerBuildingArchetype = entityManager.CreateArchetype(
            typeof(BuildingUpgradeComponent),
            typeof(TimeManipulationComponent),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(Translation),
            typeof(RenderBounds),
            typeof(BuildingClickInfoCompontnet)
        );
        Entity catEconomicsSchool = entityManager.CreateEntity(timeTowerBuildingArchetype);
        entityManager.SetComponentData(catEconomicsSchool, new BuildingUpgradeComponent
        {
            rewardIncreaseRate = 1f,
            upgradeCost = 2200,
            upgradeCount = 0,
            upgradeRate = 2.44f
        });

        entityManager.SetComponentData(catEconomicsSchool, new TimeManipulationComponent
        {
            timeMod = 0.995f,
        });

        entityManager.SetComponentData(catEconomicsSchool, new Translation
        {
            Value = pos,
        });

        entityManager.SetComponentData(catEconomicsSchool, new BuildingClickInfoCompontnet
        {
            panelId = 6,
        });

        Unity.Mathematics.AABB paresdAABB = new Unity.Mathematics.AABB()
        {
            Center = buildignMesh.bounds.center,
            Extents = buildignMesh.bounds.extents,
        };
        entityManager.SetComponentData(catEconomicsSchool, new RenderBounds
        {
            Value = paresdAABB
        });
        entityManager.SetSharedComponentData(catEconomicsSchool, new RenderMesh
        {
            mesh = buildignMesh,
            material = catTimeTowerMaterial,
        });
    }

    private void ConstructCatUndergroundDungon(EntityManager entityManager, EntityArchetype buildingArchetype)
    {
        Vector3 pos = GetPosRelativeToCamera(0.82f, 0.6f);
        Entity catUndergroundDungon = entityManager.CreateEntity(buildingArchetype);
        entityManager.SetComponentData(catUndergroundDungon, new BuildingUpgradeComponent
        {
            rewardIncreaseRate = 1.4f,
            upgradeCost = 1000,
            upgradeCount = 0,
            upgradeRate = 1.69f
        });
        entityManager.SetComponentData(catUndergroundDungon, new GoldRewardComponent
        {
            goldReward = 250
        });
        entityManager.SetComponentData(catUndergroundDungon, new CycleComponent
        {
            cycleDuration = 15f,
            elapsedTime = 0f
        });

        entityManager.SetComponentData(catUndergroundDungon, new Translation
        {
            Value = pos,
        });

        entityManager.SetComponentData(catUndergroundDungon, new BuildingClickInfoCompontnet
        {
            panelId = 5,
        });

        Unity.Mathematics.AABB paresdAABB = new Unity.Mathematics.AABB()
        {
            Center = buildignMesh.bounds.center,
            Extents = buildignMesh.bounds.extents,
        };
        entityManager.SetComponentData(catUndergroundDungon, new RenderBounds
        {
            Value = paresdAABB
        });
        entityManager.SetSharedComponentData(catUndergroundDungon, new RenderMesh
        {
            mesh = buildignMesh,
            material = catUndergroundDungonMaterial,
        });
    }

    private void ConstructCatAdventureGuild(EntityManager entityManager, EntityArchetype buildingArchetype)
    {
        Vector3 pos = GetPosRelativeToCamera(0.70f, 0.35f);
        Entity catAdventureGuild = entityManager.CreateEntity(buildingArchetype);
        entityManager.SetComponentData(catAdventureGuild, new BuildingUpgradeComponent
        {
            rewardIncreaseRate = 1.3f,
            upgradeCost = 500,
            upgradeCount = 0,
            upgradeRate = 1.49f
        });
        entityManager.SetComponentData(catAdventureGuild, new GoldRewardComponent
        {
            goldReward = 150
        });
        entityManager.SetComponentData(catAdventureGuild, new CycleComponent
        {
            cycleDuration = 10f,
            elapsedTime = 0f
        });

        entityManager.SetComponentData(catAdventureGuild, new Translation
        {
            Value = pos,
        });

        entityManager.SetComponentData(catAdventureGuild, new BuildingClickInfoCompontnet
        {
            panelId = 4,
        });

        Unity.Mathematics.AABB paresdAABB = new Unity.Mathematics.AABB()
        {
            Center = buildignMesh.bounds.center,
            Extents = buildignMesh.bounds.extents,
        };
        entityManager.SetComponentData(catAdventureGuild, new RenderBounds
        {
            Value = paresdAABB
        });
        entityManager.SetSharedComponentData(catAdventureGuild, new RenderMesh
        {
            mesh = buildignMesh,
            material = catAdventureGuildMaterial,
        });
    }

    private void ConstructCatEconomicsSchool(EntityManager entityManager)
    {
        Vector3 pos = GetPosRelativeToCamera(0.70f, 0.07f);
        EntityArchetype economicBuildingArchetype = entityManager.CreateArchetype(
            typeof(BuildingUpgradeComponent),
            typeof(GoldManipulationComponent),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(Translation),
            typeof(RenderBounds),
            typeof(BuildingClickInfoCompontnet)
        );
        Entity catEconomicsSchool = entityManager.CreateEntity(economicBuildingArchetype);
        entityManager.SetComponentData(catEconomicsSchool, new BuildingUpgradeComponent
        {
            rewardIncreaseRate = 0.01f,
            upgradeCost = 700,
            upgradeCount = 0,
            upgradeRate = 1.56f
        });

        entityManager.SetComponentData(catEconomicsSchool, new GoldManipulationComponent
        {
            goldMod = 1.005f,
        });

        entityManager.SetComponentData(catEconomicsSchool, new Translation
        {
            Value = pos,
        });

        entityManager.SetComponentData(catEconomicsSchool, new BuildingClickInfoCompontnet
        {
            panelId = 3,
        });

        Unity.Mathematics.AABB paresdAABB = new Unity.Mathematics.AABB()
        {
            Center = buildignMesh.bounds.center,
            Extents = buildignMesh.bounds.extents,
        };
        entityManager.SetComponentData(catEconomicsSchool, new RenderBounds
        {
            Value = paresdAABB
        });
        entityManager.SetSharedComponentData(catEconomicsSchool, new RenderMesh
        {
            mesh = buildignMesh,
            material = catEconomicsSchoolMaterial,
        });

    }

    private void ConstructCatWizardSchool(EntityManager entityManager, EntityArchetype buildingArchetype)
    {
        Vector3 pos = GetPosRelativeToCamera(0.35f, 0.15f);
        Entity catWizardSchool = entityManager.CreateEntity(buildingArchetype);
        entityManager.SetComponentData(catWizardSchool, new BuildingUpgradeComponent
        {
            rewardIncreaseRate = 1.2f,
            upgradeCost = 300,
            upgradeCount = 0,
            upgradeRate = 1.37f
        });
        entityManager.SetComponentData(catWizardSchool, new GoldRewardComponent
        {
            goldReward = 50
        });
        entityManager.SetComponentData(catWizardSchool, new CycleComponent
        {
            cycleDuration = 7f,
            elapsedTime = 0f
        });

        entityManager.SetComponentData(catWizardSchool, new Translation
        {
            Value = pos,
        });

        entityManager.SetComponentData(catWizardSchool, new BuildingClickInfoCompontnet
        {
            panelId = 2,
        });

        Unity.Mathematics.AABB paresdAABB = new Unity.Mathematics.AABB()
        {
            Center = buildignMesh.bounds.center,
            Extents = buildignMesh.bounds.extents,
        };
        entityManager.SetComponentData(catWizardSchool, new RenderBounds
        {
            Value = paresdAABB
        });
        entityManager.SetSharedComponentData(catWizardSchool, new RenderMesh
        {
            mesh = buildignMesh,
            material = catWizardSchoolMaterial,
        });
    }

    private void ConstructCatRestaurant(EntityManager entityManager, EntityArchetype buildingArchetype)
    {
        Vector3 pos = GetPosRelativeToCamera(0.2f, 0.35f);
        Entity catRestaurant = entityManager.CreateEntity(buildingArchetype);
        entityManager.SetComponentData(catRestaurant, new BuildingUpgradeComponent
        {
            rewardIncreaseRate = 1.15f,
            upgradeCost = 100,
            upgradeCount = 0,
            upgradeRate = 1.3f
        });
        entityManager.SetComponentData(catRestaurant, new GoldRewardComponent
        {
            goldReward = 25
        });
        entityManager.SetComponentData(catRestaurant, new CycleComponent
        {
            cycleDuration = 5f,
            elapsedTime = 0f
        });

        entityManager.SetComponentData(catRestaurant, new Translation
        {
            Value = pos,
        });

        entityManager.SetComponentData(catRestaurant, new BuildingClickInfoCompontnet
        {
            panelId = 1,
        });

        Unity.Mathematics.AABB paresdAABB = new Unity.Mathematics.AABB()
        {
            Center = buildignMesh.bounds.center,
            Extents = buildignMesh.bounds.extents,
        };
        entityManager.SetComponentData(catRestaurant, new RenderBounds
        {
            Value = paresdAABB
        });
        entityManager.SetSharedComponentData(catRestaurant, new RenderMesh
        {
            mesh = buildignMesh,
            material = catRestaurantMaterial,
        });
    }

    private void ConstructMainCatHouse(EntityManager entityManager, EntityArchetype buildingArchetype)
    {
        Entity catHouse = entityManager.CreateEntity(buildingArchetype);
        entityManager.SetComponentData(catHouse, new BuildingUpgradeComponent
        {
            rewardIncreaseRate = 1.1f,
            upgradeCost = 10,
            upgradeCount = 1,
            upgradeRate = 1.2f,
        }) ;
        entityManager.SetComponentData(catHouse, new GoldRewardComponent
        {
            goldReward = 10
        });
        entityManager.SetComponentData(catHouse, new CycleComponent
        {
            cycleDuration = 1f,
            elapsedTime = 0f,
        });
        entityManager.SetComponentData(catHouse, new BuildingClickInfoCompontnet
        {
            panelId = 0,
        });
        
        Unity.Mathematics.AABB paresdAABB = new Unity.Mathematics.AABB() 
        {
            Center = buildignMesh.bounds.center,
            Extents = buildignMesh.bounds.extents,
        };
        entityManager.SetComponentData(catHouse, new RenderBounds
        {
            //should be all 0's?
            //How the fuck is it not???
            //is it just the exteds that it gets right?
            Value = paresdAABB
        });
        entityManager.SetSharedComponentData(catHouse, new RenderMesh
        {
            mesh = buildignMesh,
            material = catHouseMaterial,
        });

    }

    [SerializeField]private Camera cameraPoint = null;

    private Vector3 GetPosRelativeToCamera(float w, float h)
    {
        int width = cameraPoint.pixelWidth;
        int height = cameraPoint.pixelHeight;
        float finalW = w * width;
        float finalH = h * height;
        Vector3 finalPos = cameraPoint.ScreenToWorldPoint(new Vector3(finalW, finalH, 0f));
        finalPos.z = 0f;
        return finalPos;
    }
}
