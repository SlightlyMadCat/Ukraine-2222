using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

/**
 * This script provide static expressions and logic for the sound manager tools operations
 */

[RequireComponent(typeof(SoundsManager))]
public class SoundManagerGUIExtender : MonoBehaviour
{
    private static string pathToResources;
    private static string pathToAudio;

    // Generate path to folder with sounds 
    private static string GeneratePathToAudioFolder(string _name)
    {
        var _path = Application.dataPath + "/Resources/" + _name;
        return _path;
    }
    
    // Generate path to resources folder
    private static string GeneratePathToResourcesFolder()
    {
        var _path = Application.dataPath + "/Resources";
        return _path;
    }

    // call from GUILayout, create folders (resources and sound-folder)
    public static void CreateFolder(string _name)
    {
        // Create Resources folder
        pathToResources = GeneratePathToResourcesFolder();
        if(!Directory.Exists(pathToResources))
            Directory.CreateDirectory(pathToResources);
           
        // Create Audio folder    
        pathToAudio = GeneratePathToAudioFolder(_name);
        Directory.CreateDirectory(pathToAudio);
    }

    public static List<FileInfo> GetFilesWithoutMeta(string _path)
    {
        var dir = new DirectoryInfo(_path);
        var AllFiles = dir.GetFiles("*.*"); // all files
        var MetaFiles = dir.GetFiles("*.meta*"); // meta files 
        var filesWithoutMeta = new List<FileInfo>(); // files without meta (sounds files)
        
        // remove meta file .meta 
        foreach (var file in AllFiles)
        {
            var _isContains = false;
            foreach (var metaFile in MetaFiles)
            {
                if (metaFile.Name == file.Name)
                    _isContains = true;
            }
            
            if(!_isContains)
                filesWithoutMeta.Add(file);
        }
        return filesWithoutMeta;
    }

    // Get list with all sounds from folder
    public static List<AudioFile> GetAudioClips(List<FileInfo> filesWithoutMeta, string _folderName)
    {
        var _audio = new List<AudioFile>();
        foreach (var file in filesWithoutMeta) 
        { 
            var newName = file.Name.Split('.');
            var audioClip = Resources.Load<AudioClip>(_folderName +"/" +  newName[0]);
            var audioFile = new AudioFile(newName[0], audioClip);
            _audio.Add(audioFile);
        }
        return _audio;
    }

    public static List<string> ScanForResourcesFolders(string _path)
    {
        var dir = new DirectoryInfo(_path);
        var AllFiles = dir.GetFiles(); // all files
        var _folders = new List<string>();

        foreach (var VARIABLE in AllFiles)
        {
            var _name = VARIABLE.Name.Split('.');

            if (Directory.Exists(Application.dataPath + "/Resources/" + _name[0]))
            {
                //it's really folder
                _folders.Add(_name[0]);
            }
        }
        
        // remove meta file .meta 
        return _folders;
    }
    
    [Serializable]
    public class AudioFile
    {
       [SerializeField] private string name;
       [SerializeField] private AudioClip audioClip;

        public void SetName(string _name)
        {
            this.name = _name;
        }
        
        public string GetName()
        {
            return this.name;
        }

        public void SetAudioClip(AudioClip _audio)
        {
            audioClip = _audio;
        }
        
        public AudioClip GetAudioClip()
        {
            return audioClip;
        }

        public AudioFile(string name, AudioClip clip)
        {
            this.name = name;
            this.audioClip = clip;
        }
    }
}


