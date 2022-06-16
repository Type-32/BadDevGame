using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    public GunManager gun;
    public UIManager ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
    }
    void Awake()
    {
        ui = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ui.ammoPool.text = gun.stats.ammoPool.ToString();
        ui.currentAmmo.text = gun.stats.ammo.ToString();

        if (gun.stats.isAttaching || gun.stats.isAiming)
        {
            gun.ui.ui.crosshair.SetActive(false);
        }
        else
        {
            gun.ui.ui.crosshair.SetActive(true);
        }

        if(gun.core.fireMode == GunCoreFunc.fireType.single){
            ui.fireModeIndicator.text = "b";
            ui.fireModeIndicator.color = Color.yellow;
        }else if(gun.core.fireMode == GunCoreFunc.fireType.automatic){
            ui.fireModeIndicator.text = "bbbb";
            ui.fireModeIndicator.color = Color.yellow;
        }
        else if(gun.core.fireMode == GunCoreFunc.fireType.burst) {
            ui.fireModeIndicator.text = "bbb";
            ui.fireModeIndicator.color = Color.red;
        }
    }
}
