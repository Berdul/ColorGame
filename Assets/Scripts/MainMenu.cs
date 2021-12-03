using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadingMenu;
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    public GameObject loadingBar;

    public void startGame() {
        mainMenu.SetActive(false);
        loadingMenu.SetActive(true);

        scenesToLoad.Add(SceneManager.LoadSceneAsync("Base scene"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Level2Tiled", LoadSceneMode.Additive));
        StartCoroutine(loadingScreen());
    }

    public void exitGame() {

    }

    IEnumerator loadingScreen() {
        float progress = 0;
        for (int i = 0; i < scenesToLoad.Count; ++i) {
            while(!scenesToLoad[i].isDone) {
                progress += scenesToLoad[i].progress;
                loadingBar.GetComponent<Slider>().value = progress/scenesToLoad.Count;
                yield return null;
            }
        }
    } 
}
