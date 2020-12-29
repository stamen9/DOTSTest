using UnityEngine;
using UnityEditor;
using System.IO;
using Unity.Entities;
using Unity.Entities.Serialization;
using System.Collections;
using Unity.Collections;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class SaveSystem : SystemBase
{
    World tempWorld;
    EntityManager tempWorldEntityManager;


    private void SaveProcedure()
    {
        Debug.Log("Attempting To Save Game...");
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        
        string path = Application.persistentDataPath + "/GameSave.cat";

        Debug.Log("Path used to Save: " + path);

        StreamBinaryWriter binaryWriter = new StreamBinaryWriter(path);
        object[] unityReferencedObjects;
        SerializeUtility.SerializeWorld(entityManager, binaryWriter,out unityReferencedObjects);

        Debug.Log("h");
    }

    private bool SaveFileExists()
    {
        string path = Application.persistentDataPath + "/GameSave.cat";

        Debug.Log("Path used to Load: " + path);

        if (File.Exists(path))
        {
            return true;
        }

        return false;
    }

    void LoadProcedure()
    {
        tempWorld = new World("DeserializetionWorld");
        tempWorldEntityManager = tempWorld.EntityManager;

        Debug.Log("Attempting To Load Game...");

        string path = Application.persistentDataPath + "/GameSave.cat";

        Debug.Log("Path used to Load: " + path);

        if(File.Exists(path))
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            StreamBinaryReader binaryReader = new StreamBinaryReader(path);
            var transaction = tempWorldEntityManager.BeginExclusiveEntityTransaction();

            //Get all shared component data

            Object[] sharedObjects = Resources.LoadAll("Materials/BuildingMats/");
            Mesh mesh = Utilites.BuildQuad();
            ArrayUtility.Insert(ref sharedObjects, 0, mesh);

            SerializeUtility.DeserializeWorld(transaction, binaryReader,sharedObjects);
            tempWorldEntityManager.EndExclusiveEntityTransaction();

            var buildingQuery = new EntityQueryDesc
            {
                Any = new ComponentType[] { typeof(BuildingUpgradeComponent) },
            };
            var walletQuery = new EntityQueryDesc
            {
                Any = new ComponentType[] { typeof(WalletComponent) },
            };

            EntityQuery m_BuildingGroup = tempWorldEntityManager.CreateEntityQuery(buildingQuery);
            EntityQuery m_WalletGroup = tempWorldEntityManager.CreateEntityQuery(walletQuery);

            entityManager.MoveEntitiesFrom(tempWorldEntityManager, m_BuildingGroup);
            entityManager.MoveEntitiesFrom(tempWorldEntityManager, m_WalletGroup);
        }
    }

    protected override void OnStartRunning()
    {
        if(SaveFileExists())
        {
            LoadProcedure();
            return;
        }
            PlayerSpawner ps = GameObject.FindObjectOfType<PlayerSpawner>();
            ps.SpawnNewPlayer();
            BuildingSpawner bs = GameObject.FindObjectOfType<BuildingSpawner>();
            bs.Construct();
    }

    /*void OnApplicationQuit()
    {
        SaveProcedure();
    }*/
    
    protected override void OnStopRunning()
    {
        SaveProcedure();
    }

    protected override void OnUpdate()
    {
        //throw new System.NotImplementedException();
    }
        
}
