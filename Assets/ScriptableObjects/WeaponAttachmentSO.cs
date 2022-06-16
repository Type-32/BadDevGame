using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon Attachment Data", fileName = "New Weapon Attachment")]
public class WeaponAttachmentSO : ScriptableObject
{
    public enum WeaponAttachmentTypes
    {
        Barrel,
        Underbarrel,
        Sight,
        Null
    };
    [Header("Generic Fields")]
    public string attachmentName = "";
    public WeaponAttachmentTypes attachmentType;
    public GameObject attachmentObject;

    [Space]
    [Header("Barrel-Type Attachment Stats")]
    public bool changesMuzzleSound = false;
    public AudioClip overrideMuzzleSound;

    [Space]
    [Header("Underbarrel-Type Attachment Stats")]
    public bool changesCameraRecoil = false;
    public float counterRecoilAmount = 0f;

    [Space]
    [Header("Sight-Type Attachment Stats")]
    public bool changesCameraDefaultFOV = false;
    [Tooltip("Overrides the direct change of FOV amount into the multiplier change of FOV amount")] public bool useFOVMultiplier = false;
    public float cameraFOVChangedAmount = 0f;
    public float cameraFOVChangedMultiplier = 1.0f;
}
