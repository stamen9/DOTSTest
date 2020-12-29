using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Rendering;

public class BuildingClickSystem : SystemBase
{
    //Kind of needs to run on main thread :S
    //Not optimal but whatever
    protected override void OnUpdate()
    {
        if(Input.GetMouseButtonDown(0) && !UIUpdateSystem.Instance.isUIOpen())
        {
            //Not optimal to call Camera.main
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Entities.ForEach((Entity e,
                BuildingClickInfoCompontnet buildingClickInfoCompontnet,
                WorldRenderBounds renderBounds,
                BuildingUpgradeComponent buildingUpgradeComponent
                ) =>
            {
                if (renderBounds.Value.Contains(mousePos))
                {

                    UIUpdateSystem.Instance.OpenUpgradePanel(e,buildingUpgradeComponent);
                }

            })
                .Run();
        }
    }
}
