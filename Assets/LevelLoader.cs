using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 5f;
    public void LoadNextGameScene()
    {
        LoadGameScene(false);
    }
    public void LoadLastGameScene()
    {
        LoadGameScene(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadGameScene(bool inversedOperation)
    {
        if (inversedOperation) StartCoroutine(LoadSceneInitializer(SceneManager.GetActiveScene().buildIndex - 1));
        else StartCoroutine(LoadSceneInitializer(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadSceneInitializer(int sceneIndex)
    {
        transition.SetTrigger("StartSceneLoad");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
