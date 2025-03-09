using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    [Header("Dialogue Defaults")]
    public Sound defaultDialogueSound;
    private Dictionary<string, Dictionary<DialogueType, Sound>> _clientDialogueMap;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.playOnAwake = false;
        }
        
        InitializeDialogueSystem();
        Play("Background 1");
    }

    public void Play(string sound)
    {
        var s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();

    }
    
    public void Stop(string soundName)
    {
        Sound s = Array.Find(sounds, item => item.name == soundName);
        if (s != null && s.source != null)
        {
            s.source.Stop();
        }
    }
    
    
    private void InitializeDialogueSystem() {
        _clientDialogueMap = new Dictionary<string, Dictionary<DialogueType, Sound>>();

        // Load all dialogue audio clips
        foreach (ClientName client in System.Enum.GetValues(typeof(ClientName))) {
            string clientPath = $"Sounds/Dialogue/{client}";
            AudioClip[] clips = Resources.LoadAll<AudioClip>(clientPath);

            var dialogueDict = new Dictionary<DialogueType, Sound>();
            
            foreach (var clip in clips) {
                DialogueType type = ParseDialogueType(clip.name);
                if (type == DialogueType.None) continue;

                Sound sound = CreateDialogueSound(clip);
                dialogueDict[type] = sound;
            }

            _clientDialogueMap[client.ToString()] = dialogueDict;
        }
    }
    
    private DialogueType ParseDialogueType(string clipName) {
        string typeString = clipName.Split('_')[1]; // Get part after client name
        return System.Enum.TryParse(typeString, out DialogueType type) ? type : DialogueType.None;
    }
    
    private Sound CreateDialogueSound(AudioClip clip) {
        Sound newSound = new Sound {
            name = clip.name,
            clip = clip,
            volume = defaultDialogueSound.volume,
            pitch = defaultDialogueSound.pitch,
            volumeVariance = defaultDialogueSound.volumeVariance,
            pitchVariance = defaultDialogueSound.pitchVariance,
            loop = false
        };

        newSound.source = gameObject.AddComponent<AudioSource>();
        newSound.source.clip = newSound.clip;
        newSound.source.outputAudioMixerGroup = defaultDialogueSound.mixerGroup;
        newSound.source.playOnAwake = false;
        
        return newSound;
    }
    
    public void PlayDialogue(ClientName client, DialogueType type) {
        string clientKey = client.ToString();
        if (!_clientDialogueMap.ContainsKey(clientKey)) {
            Debug.LogError($"No dialogue found for {client}");
            return;
        }

        if (_clientDialogueMap[clientKey].TryGetValue(type, out Sound sound)) {
            sound.source.Play();
            WalkieTalkie.Instance.WalkieTalkieVoice(sound.source.clip.length);
        }
        else
        {
            Debug.LogError("Error playing");
        }
    }
    
    public float GetSoundDuration(string soundName)
    {
        var s = Array.Find(sounds, item => item.name == soundName);
        if (s == null || s.source == null || s.source.clip == null) return 0;
        return s.source.clip.length / s.source.pitch;
    }
    
    public float GetDialogueDuration(ClientName client, DialogueType type)
    {
        var clientKey = client.ToString();
        if (!_clientDialogueMap.ContainsKey(clientKey)) return 0;
        if (!_clientDialogueMap[clientKey].TryGetValue(type, out var sound)) return 0;
        return sound.source.clip.length / sound.source.pitch;
    }
}