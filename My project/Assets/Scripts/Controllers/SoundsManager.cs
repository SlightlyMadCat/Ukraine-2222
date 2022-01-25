using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Audio;

/**
 * All spawned on scene sounds controller
 * New sound sample can be spawned by id from the PlayCustomSoundByID() void
 */

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource sampleSource;    //sample of spawning sources (without loop)
    [SerializeField] private AudioMixerGroup standardChanel;
    [Serializable]
    public class CustomSoundSample
    {
        [SerializeField] private string name;     //action name
        [SerializeField] private AudioClip clip;  //custom clip
        [SerializeField] private AudioMixerGroup desireMixer;  //desire mixer group 

        public AudioClip GetClip()
        {
            return clip;
        }

        public AudioMixerGroup GetMixer()
        {
            return desireMixer;
        }

        public CustomSoundSample(string _name, AudioClip _clip, AudioMixerGroup _desireMixer)
        {
            this.name = _name;
            this.clip = _clip;
            this.desireMixer = _desireMixer;
        }

        public string GetName()
        {
            return name;
        }
    }
    [Header("All custom sounds list")]
    [SerializeField] private List<CustomSoundSample> customSoundSamples = new List<CustomSoundSample>();

    #region Singleton
    public static SoundsManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    //may call from ui
    public void PlayCustomSoundByID(int _id)
    {
        SpawnNewCustomSound(customSoundSamples[_id].GetClip(), customSoundSamples[_id].GetMixer());
    }

    //spawn new sound with custom mixer
    private void SpawnNewCustomSound(AudioClip _clip, AudioMixerGroup _mixer)
    {
        AudioSource _newSound = gameObject.AddComponent<AudioSource>();
        _newSound.clip = _clip;

        //set new sound values as in example
        _newSound.volume = sampleSource.volume;
        _newSound.outputAudioMixerGroup = sampleSource.outputAudioMixerGroup;
        _newSound.spatialBlend = sampleSource.spatialBlend;
        _newSound.spread = sampleSource.spread;
        _newSound.rolloffMode = sampleSource.rolloffMode;
        _newSound.outputAudioMixerGroup = _mixer;

        _newSound.Play();

        StartCoroutine(WaitForDestroySource(_clip.length, _newSound));
    }

    // remove source component after sound is over
    IEnumerator WaitForDestroySource(float _delay, AudioSource _source)
    {
        yield return new WaitForSeconds(_delay);
        Destroy(_source);
    }

    // Add new sounds with check on copy by file-name
    public void AddNewSounds(List<SoundManagerGUIExtender.AudioFile> _newAudio)
    {
        foreach (var newSound in _newAudio)
        {
            // Check new file name in list with old file names 
            var findCopy = false;
            foreach (var customSound in customSoundSamples.ToList().Where(customSound => customSound.GetName() == newSound.GetName()))
            {
                findCopy = true;
            }
            if (findCopy) continue; // name existed
            var _customSound = new CustomSoundSample(newSound.GetName(), newSound.GetAudioClip(), standardChanel);
            customSoundSamples.Add(_customSound);
        }
        Debug.Log("Sounds Added");
    }

    public void ClearSounds()
    {
        customSoundSamples.Clear();
    }

    public bool SoundsListIsNotEmpty()
    {
        return customSoundSamples.Count > 0;
    }
}