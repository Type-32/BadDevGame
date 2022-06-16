using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTrackerBase : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject trackingObject;
    public RectTransform selfTransform;
    public UIManager uiManager;
    public Image pointImage;
    public RectTransform compassUIBar;
    // Update is called once per frame
    private void Start()
    {
        selfTransform = transform.gameObject.GetComponent<RectTransform>();
        uiManager = FindObjectOfType<UIManager>();
        fpsCam = FindObjectOfType<MouseLookScript>().GetComponent<Camera>();
        compassUIBar = uiManager.compassUI.GetComponent<RectTransform>();
    }
    void Update()
    {
        PointTrackingFunction();
    }
    public void PointTrackingFunction()
    {
        float tempY = selfTransform.anchoredPosition.y;

        transform.position = fpsCam.WorldToScreenPoint(trackingObject.transform.position);
        transform.position = new Vector3(transform.position.x, tempY, transform.position.z);

        //Debug.Log(selfTransform.anchoredPosition.x);
        selfTransform.anchoredPosition = new Vector3(Mathf.Clamp(selfTransform.anchoredPosition.x, -(compassUIBar.rect.width / 2), compassUIBar.rect.width / 2), tempY, transform.position.z);

        if (!trackingObject.GetComponent<MeshRenderer>().isVisible || (selfTransform.anchoredPosition.x >= compassUIBar.rect.width / 2 || selfTransform.anchoredPosition.x <= -(compassUIBar.rect.width / 2)))
        {
            pointImage.enabled = false;
        }
        else
        {
            pointImage.enabled = true;
        }

    }
}
