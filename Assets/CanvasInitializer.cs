using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInitializer : MonoBehaviour
{
    [SerializeField] private Canvas canvasComp;
    private PlayerManager player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        Debug.Log("Performing Get Cam Function From Gun Canvas\n");
        canvasComp.worldCamera = player.fpsCam.GetComponent<Camera>();
        Debug.Log(player.fpsCam.GetComponent<Camera>().name + "\n");
    }
}
