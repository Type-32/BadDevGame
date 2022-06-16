using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public UnityEngine.Rendering.Volume healthEffectVolume;
    public UnityEngine.Rendering.Volume healingEffectVolume;
    public PlayerManager player;
    void Start()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthEffectVolume.weight = Mathf.Lerp(healthEffectVolume.weight, 1f - player.stats.health / 100f, Time.deltaTime * 3f);
        healingEffectVolume.weight = Mathf.Lerp(healingEffectVolume.weight, 0f, Time.deltaTime * 5f);
    }
}
