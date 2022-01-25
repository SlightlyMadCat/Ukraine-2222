using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

/*
 * Moved to the separate editor window helper tool for the each scene sounds management.
 * Allows user to spawn/destroy manager, spawn folders in the resources and attach
 * audio files to the sounds list.
 */

public class SoundsManagerTool : EditorWindow
{
    [MenuItem("505-Tools/Sounds Manager Tool")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<SoundsManagerTool>("Sounds Manager Tool");
    }

    private int popupFolderIndex = 0;   //selected scene dropdown index
    private string newFolderName = "";  //input field text
    
    private void OnGUI()
    {
        GUILayout.Label("New Folder Name");
        newFolderName = GUILayout.TextField(newFolderName, 25);

        if (newFolderName.Length > 0 && !ScanFolders().ToList().Contains(newFolderName))
        {
            if (GUILayout.Button("Create New Folder"))
            {
                SoundManagerGUIExtender.CreateFolder(newFolderName);
            }
        }
        
        GUILayout.Space(20f);
        
        //only one sounds manager can be spawned on the each scene
        if (!FindObjectOfType<SoundsManager>())
        {
            if (GUILayout.Button("Spawn Sounds Manager"))
            {
                Instantiate(Resources.Load("Prefabs/SOUNDS-MANAGER") as GameObject);
            }
        }
        else
        {
            if (GUILayout.Button("Destroy Sounds Manager"))
            {
                DestroyImmediate(FindObjectOfType<SoundsManager>().gameObject);
            }
        }

        GUILayout.Space(20f);

        if (!FindObjectOfType<SoundsManager>()) return; //hide other options if none sounds manager is spawned

        var _options = ScanFolders();
        popupFolderIndex = EditorGUILayout.Popup("Selected Folder: ", popupFolderIndex, _options);

        if (GUILayout.Button("Assign Sounds From Selected Folder"))
        {
            var _folder = _options[popupFolderIndex];
            var _path = Application.dataPath + "/Resources/" + _folder;
            var filesWithoutMeta =
                SoundManagerGUIExtender.GetFilesWithoutMeta(_path); // files without meta (sounds files)
            var _audioClips = SoundManagerGUIExtender.GetAudioClips(filesWithoutMeta, _folder);

            if (_audioClips.Count == 0)
                Debug.LogError("Folder is empty: " + _folder);
            else
                FindObjectOfType<SoundsManager>().AddNewSounds(_audioClips);
        }

        if (FindObjectOfType<SoundsManager>().SoundsListIsNotEmpty())
        {
            if (GUILayout.Button("Clear Sounds Manager List"))
            {
                FindObjectOfType<SoundsManager>().ClearSounds();
            }
        }
    }

    //scan for the all folders in resources
    private static string[] ScanFolders()
    {
        var fileInfos = SoundManagerGUIExtender.ScanForResourcesFolders(Application.dataPath + "/Resources");
        return fileInfos.ToArray();
    }
}
