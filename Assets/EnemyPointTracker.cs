using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPointTracker : PointTrackerBase
{
    [SerializeField] private Animator enemyPointAnim;
    // Update is called once per frame
    private void Start()
    {
        base.selfTransform = transform.gameObject.GetComponent<RectTransform>();
        base.uiManager = FindObjectOfType<UIManager>();
        base.fpsCam = FindObjectOfType<MouseLookScript>().GetComponent<Camera>();
        base.compassUIBar = uiManager.compassUI.GetComponent<RectTransform>();
    }
    void Update()
    {
        if (trackingObject == null)
        {
            enemyPointAnim.Play("EnemyTP ExitAnim");
            Destroy(gameObject, 0.5f);
            return;
        }
        base.PointTrackingFunction();
    }
}
