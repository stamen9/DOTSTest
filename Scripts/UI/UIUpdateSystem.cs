using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;
using TMPro;
using UnityEngine.UI;

public class UIUpdateSystem : MonoBehaviour
{
    public static UIUpdateSystem Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    [SerializeField] private PlayerResourcesUIHelper resourceSystem;
    [SerializeField] private AdsSystem adsSystem;
    [SerializeField] private GameObject AdPanel;

    private bool isUIOpenBool = false;
    private bool needsUpdate = false;
    private GameObject currentlyActiveUI;

    [SerializeField] private GameObject[] uiSceens;

    [SerializeField] private GameObject upgradePanel = null;

    [SerializeField] private TMP_Text currentLevel = null; 
    [SerializeField] private TMP_Text nextLevel = null;
    
    [SerializeField] private TMP_Text currentGold;
    [SerializeField] private TMP_Text nextGold;

    [SerializeField] private TMP_Text currentCost = null;
    [SerializeField] private TMP_Text nextCost = null;

    private Entity lastSelectedBuilding;
    private BuildingUpgradeComponent lastSelectedUpgradeComponent;

    /*!!!
        I feel lik a lot(some) of useless operation are happening here
    !!!*/ 
    public void OpenUpgradePanel(Entity e,BuildingUpgradeComponent buildingUpgradeComponent)
    {

        isUIOpenBool = true;
        lastSelectedBuilding = e;
        lastSelectedUpgradeComponent = buildingUpgradeComponent;
        upgradePanel.SetActive(true);
        UpdateUpgradePanel(lastSelectedUpgradeComponent);

        //upgradeButton.onClick.AddListener(SetBuildingForUpgrade);
    }

    void ClearAfterClik()
    {
        //upgradeButton.onClick.RemoveAllListeners();
    }

    public void OpenPanel(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void ClosePanel(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void SetNeedsUpdate(BuildingUpgradeComponent upgradeData)
    {

        if(!resourceSystem.HasGold(upgradeData.upgradeCost))
        {
            OpenPanel(AdPanel);
            return;
        }
        lastSelectedUpgradeComponent = upgradeData;
        needsUpdate = true;
    }

    private void UpdateUpgradePanel(BuildingUpgradeComponent buildingUpgradeComponent)
    {
        currentLevel.text = buildingUpgradeComponent.upgradeCount.ToString();
        nextLevel.text = (buildingUpgradeComponent.upgradeCount + 1).ToString();

        //currentGold.text =
        //nextGold.text =

        currentCost.text = buildingUpgradeComponent.upgradeCost.ToString();
        nextCost.text = ((int)(buildingUpgradeComponent.upgradeCost * buildingUpgradeComponent.upgradeRate)).ToString();
    }

    public void SetBuildingForUpgrade()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.AddComponent(lastSelectedBuilding, typeof(ScheduledForUpgrade));

        UpdateUpgradePanel(lastSelectedUpgradeComponent);
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
        isUIOpenBool = false;
    }

    public bool isUIOpen()
    {
        return isUIOpenBool;
    }

    private void Update()
    {
        if(needsUpdate)
        {
            UpdateUpgradePanel(lastSelectedUpgradeComponent);
            needsUpdate = false;
        }
    }
}
