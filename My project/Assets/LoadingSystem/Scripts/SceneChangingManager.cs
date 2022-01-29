using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * Single script on the each scene where scene transition logic
 * can be triggered
 */

public class SceneChangingManager : MonoBehaviour
{
    [SerializeField] private string levelName;

    private string loadingSceneName = "Loading";    //scene used to display scene percentage loading
    private string introSceneName = "Intro";    //unity intro scene name
    
    private float timer = 1;    //cur timer in the intro scene
    private float maxTime = 15; //max time spent on the intro scene
    private float fps;  //cur fps rate
    private float deltaTime = 0.0f;

    [SerializeField] private bool showFPS = false;

    [SerializeField] private GameObject dontDestroyObj;
    
    #region Singleton

    public static SceneChangingManager Instance;
    private void Awake() => Instance = this;

    #endregion

    //used to load scene by the default settings
    public void LoadScene()
    {
        Cursor.visible = true;
        SelectedSceneInfo.selectedSceneName = levelName;
        SceneManager.LoadScene(loadingSceneName);
    }

    //used to load custom scene by the name
    public void LoadCustomScene(string _name)
    {
        Cursor.visible = true;
        SelectedSceneInfo.selectedSceneName = _name;
        SceneManager.LoadScene(loadingSceneName);
    }

    public void AppQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    //added to stop application in the editor mode
#else
        Application.Quit();
#endif
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == introSceneName)
        {
            StartCoroutine(WaitToLoadMainMenu());
            DontDestroyOnLoad(dontDestroyObj);
        }
    }

    IEnumerator WaitToLoadMainMenu()
    {
        yield return new WaitForSeconds(10f);
        LoadScene();
    }

    private void Update()
    {
        if(showFPS)
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }
    
    //fps counter logic && display
    private void OnGUI()
    {
        if (!showFPS) return;
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}

//added to save selected scene name to async loading
public static class SelectedSceneInfo
{
    public static string selectedSceneName { get; set; }
}
