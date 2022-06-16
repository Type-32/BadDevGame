using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Gadget Data", fileName = "New Gadget")]
public class MiscGadgetSO : ScriptableObject
{
    public enum GadgetFunction
    {
        Healing,
        Damage,
        Throwables,
        AreaEffect,
        Empty
    }
    [Header("Generics")]
    public string gadgetName = "";
    public GadgetFunction gadgetType = GadgetFunction.Empty;

    [Space]
    [Header("Gadget Function Healing")]
    public float healAmount = 75f;

    [Space]
    [Header("Gadget Function Damage")]
    public float damageAmount = 75f;

    [Space]
    [Header("Gadget Function Throwables")]
    public float throwablesDamageAmount = 75f;
    public float throwForce = 0f;
    public float effectOnImpactDelaySeconds = 3f;
    public bool isExplosiveOnImpact = false;
}
