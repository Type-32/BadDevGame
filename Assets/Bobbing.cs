using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    public float walkBobAmount = 0.1f;
    public float sprintBobAmount = 0.15f;
    public float walkBobSpeed = 14f;
    public float sprintBobSpeed = 16f;
    public float returnDuration = 5f;
    public PlayerManager player;

    private float defaultYPos = 0f;
    private float defaultXPos = 0f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, defaultXPos, Time.deltaTime * 3), Mathf.Lerp(transform.localPosition.y, defaultYPos, Time.deltaTime * 3), transform.localPosition.z);
        if (player.stats.playerCameraBobEnabled && !player.ui.openedOptions)
        {
            CameraBob();
        }
    }
    void CameraBob()
    {
        if (!player.stats.onGround) return;

        if((Mathf.Abs(Input.GetAxis("Horizontal")) > 0f || Mathf.Abs(Input.GetAxis("Vertical")) > 0f))
        {
            //
            timer += Time.deltaTime * (player.stats.isSprinting ? sprintBobSpeed : walkBobSpeed);
            transform.localPosition = new Vector3(defaultXPos + Mathf.Cos(timer/2) * (player.stats.isSprinting ? sprintBobAmount : walkBobAmount), defaultYPos + Mathf.Sin(timer) * (player.stats.isSprinting ? sprintBobAmount : walkBobAmount), transform.localPosition.z);
        }
    }
}
