using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Script placed on the async transition scene
 * Can display loading % text in the ui
 */

public class GameLoadingManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI percentageText;

    private void Start()
    {
        StartCoroutine(LoadAsync(SelectedSceneInfo.selectedSceneName));
    }

    IEnumerator LoadAsync(string _sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);

        while (!operation.isDone)
        {
            float prog = Mathf.Clamp01(operation.progress / .9f);
            slider.value = prog;
            percentageText.text = prog * 100f + "%";

            if(prog * 100f == 100)
                slider.value = 1;

            yield return null;
        }
    }
}
