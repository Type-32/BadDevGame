using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioClip selectedSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioEmitter;

    public void OnButtonClick()
    {
        audioEmitter.clip = clickSound;
        audioEmitter.Play();
    }
    public void OnButtonSelected()
    {
        audioEmitter.clip = selectedSound;
        audioEmitter.Play();
    }
}
