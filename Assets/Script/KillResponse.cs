using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillResponse : MonoBehaviour
{
    [Header("Script References")]
    public PlayerManager player;
    public UIManager ui;

    [Space]
    [Header("Element References")]
    public GameObject killMessage;
    public List<RectTransform> killmsgArray = new List<RectTransform>();

    // Update is called once per frame
    void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        ui = FindObjectOfType<UIManager>();
    }
    public float InvokeKillMessage()
    {
        GameObject tmp = Instantiate(killMessage, transform.position, transform.rotation, transform);
        Debug.Log(tmp.name + " is instantiated ");
        if(killmsgArray.Count != 0)
        {
            for(int i = 0; i < killmsgArray.Count; i++)
            {
                float elementY = killmsgArray[i].position.y - 20f;
                Vector3 destinatedV3 = new Vector3(killmsgArray[i].position.x, elementY, killmsgArray[i].position.z);
                killmsgArray[i].position = Vector3.Lerp(killmsgArray[i].position, destinatedV3, 1.5f);
                //killmsgArray[i].position = new Vector3(killmsgArray[i].position.x, elementY, killmsgArray[i].position.z);
            }
            killmsgArray.Add(tmp.GetComponent<RectTransform>());

        }
        else
        {
            killmsgArray.Add(tmp.GetComponent<RectTransform>());
        }
        return 4f;
    }
    public IEnumerator ResetKillMessage(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        for (int i = 0; i < killmsgArray.Count; i++)
        {
            Destroy(killmsgArray[i].gameObject);
        }
        killmsgArray.Clear();
    }
}
