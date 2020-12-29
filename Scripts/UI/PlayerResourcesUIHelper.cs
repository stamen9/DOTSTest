using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEngine;

public class PlayerResourcesUIHelper : MonoBehaviour
{
    [SerializeField] private TMP_Text goldTextField = null;
    private float currentGold = 0;
    void Start()
    {
        World.DefaultGameObjectInjectionWorld.GetExistingSystem<GoldUpdateSystem>().OnGoldChanged += PlayerResourcesUIHelper_OnGoldChanged; ;
        World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildingUpgradeSystem>().OnGoldChanged += PlayerResourcesUIHelper_OnGoldChanged; ;
    }

    private void PlayerResourcesUIHelper_OnGoldChanged(object sender, System.EventArgs e)
    {
        currentGold += ((GoldChangedEventArgs)e).goldChange;
        goldTextField.text = ((int)currentGold).ToString();
    }

    public void SetGoldToSetAmount(float gold)
    {
        Debug.Log("Setting gold to " + gold);
        currentGold = gold;
        goldTextField.text = ((int)gold).ToString();
    }

    public bool HasGold(float amount)
    {
        if(amount > currentGold)
        {
            return false;
        }
        return true;
    }
}
