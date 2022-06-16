using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Script and Function References")]
    public PlayerManager player;
    public Animator anim;
    public FileManager fileManager;
    public KillResponse killresponse;

    [Space]
    [Header("HUD Stats Elements")]
    public Text currentAmmo;
    public Text ammoPool;
    public Slider currentAmmoBar;
    public Slider ammoPoolBar;
    public Text fireModeIndicator;
    public Text gadgetIndicator;
    public Slider healthbar;
    public Slider armorbar;
    public GameObject compassTrackingPointHolder;
    public GameObject compassUI;

    [Space]
    [Header("HUD Miscellaneous Elements")]
    public GameObject levelUpIndicator;
    public Text levelUpIndicatorNumber;
    public GameObject hitmarker;
    public GameObject entityIndicator;
    public GameObject crosshair;
    public GameObject interactionIndicator;
    public Text entityIndicatorText;
    public Text roundCount;
    public Text roundRestDurationTimer;
    public TextMeshProUGUI deathMessageStats;

    [Space]
    [Header("HUD Compass Tracking Point Prefabs")]
    public GameObject enemyPoint;

    [Space]
    [Header("Options Elements")]
    public GameObject optionsUI;
    public GameObject deathMenu;
    public Slider mouseSensitivitySlider;

    private const int xpBase = 500;
    private int xpLimit = 0;
    public float hitmarkerTimePassed = 0;
    private float hitmarkerTimeLimit = 0;

    public bool openedOptions = false;
    void Start(){
        openedOptions = false;
        optionsUI.SetActive(false);
        xpLimit = fileManager.JSONplayerdata.xpLevel * xpBase;
        hitmarker.SetActive(false);
    }

    void Update(){
        if (player.stats.health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            if(!deathMenu.activeSelf) deathMenu.SetActive(true);
        }
        else
        {
            if (deathMenu.activeSelf) deathMenu.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && player.stats.health > 0){
            if(openedOptions){
                CloseMenu();
            }else{
                OpenMenu();
            }
        }
        if (xpLimit == fileManager.JSONplayerdata.xp) StartCoroutine(LevelUp());
        if (Input.GetKeyDown("i")) StartCoroutine(LevelUp());
        armorbar.value = Mathf.Lerp(armorbar.value, player.stats.armor, Time.deltaTime * 5f);
        healthbar.value = Mathf .Lerp(healthbar.value, player.stats.health, Time.deltaTime * 5f);
        hitmarkerTimePassed += Time.deltaTime;
        if (hitmarkerTimePassed >= hitmarkerTimeLimit)
        {
            hitmarker.SetActive(false);
        }
        else
        {
            hitmarker.SetActive(true);
        }
        if (Input.GetKeyDown("l")) Debug.Log(Cursor.lockState);
    }
    IEnumerator LevelUp()
    {
        levelUpIndicator.SetActive(true);
        anim.Play("LevelUpIndicator");
        xpLimit = fileManager.JSONplayerdata.xpLevel * xpBase;
        fileManager.JSONplayerdata.xpLevel += 1;
        fileManager.JSONplayerdata.xp = 0;
        levelUpIndicatorNumber.text = fileManager.JSONplayerdata.xpLevel.ToString();
        fileManager.ApplyPlayerDataToFile();
        yield return new WaitForSeconds(3f);
        levelUpIndicator.SetActive(false);
    }
    public void OpenMenu()
    {
        mouseSensitivitySlider.value = player.stats.mouseSensitivity;
        Time.timeScale = 0;
        Debug.Log("Opened Options UI ");
        Cursor.lockState = CursorLockMode.None;
        openedOptions = true;
        optionsUI.SetActive(true);
    }
    public void CloseMenu()
    {
        Time.timeScale = 1;
        Debug.Log("Closed Options UI ");
        Cursor.lockState = CursorLockMode.Locked;
        openedOptions = false;
        optionsUI.SetActive(false);
    }
    public void EnforceHitmarker(float time)
    {
        hitmarkerTimePassed = 0f;
        hitmarkerTimeLimit = time;
    }
    public void KilledEnemy()
    {
        StartCoroutine(killresponse.ResetKillMessage(killresponse.InvokeKillMessage()));
    }

    public void SpawnEnemyCompassTrackingPoint(GameObject trackingObject)
    {
        GameObject temp = Instantiate(enemyPoint,compassTrackingPointHolder.transform);
        temp.GetComponent<EnemyPointTracker>().trackingObject = trackingObject;
    }
}
