using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetUsageScript : MonoBehaviour
{
    public PlayerManager player;
    public UIManager ui;
    public MiscGadgetSO gadgetData;
    public GameObject gadgetObject;
    public Animator gadgetAnim;
    public EffectsManager effects;
    public int gadgetAmount = 2;

    public GameObject ammoPack;
    public GameObject gadgetPack;
    public GameObject smallAmmoSupplyPack;
    public GameObject smallHealthSupplyPack;
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerManager>();
        effects = FindObjectOfType<EffectsManager>();
    }
    void Update()
    {
        ui.gadgetIndicator.text = "H " + gadgetAmount.ToString();
        if(Input.GetKeyDown("q") && gadgetAmount > 0)
        {
            gadgetAnim.Play("Gadget Animation");
            if(gadgetData.gadgetType == MiscGadgetSO.GadgetFunction.Healing)
            {
                StartCoroutine(UseHealing(1f));
            }
        }
        
    }
    IEnumerator UseHealing(float duration)
    {
        float tmpHealthAmount = gadgetData.healAmount;
        yield return new WaitForSeconds(duration);
        player.stats.health += tmpHealthAmount;
        if (player.stats.health > player.stats.healthLimit)
        {
            tmpHealthAmount = player.stats.health - player.stats.healthLimit;
            player.stats.health = player.stats.healthLimit;
        }
        if (tmpHealthAmount > 0)
        {
            player.stats.armor += tmpHealthAmount;
            if (player.stats.armor > player.stats.armorLimit)
            {
                player.stats.armor = player.stats.armorLimit;
            }
        }
        gadgetAmount--;
        effects.healingEffectVolume.weight = 1f;
    }
}
