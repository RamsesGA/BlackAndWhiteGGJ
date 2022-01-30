using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private enum eCurrentScene
    {
        menu,
        tutorial,
        level
    }

    private static AudioManager m_Instance;

    public static AudioManager Instance
    {
        get
        {
            if (m_Instance == null) { m_Instance = new AudioManager(); }

            return m_Instance;
        }

        private set
        {
            m_Instance = value;
        }
    }

    private AudioManager() { }

    public Sound[] menuSounds;
    public Sound[] tutorialSounds;
    public Sound[] levelSounds;

    private void Awake()
    {
        // Keep playing sound through scenes
        DontDestroyOnLoad(gameObject);

        // Populate audio manager
        foreach (Sound s in menuSounds)
        {
            s.source        = gameObject.AddComponent<AudioSource>();
            s.source.clip   = s.clip;
            s.source.volume = s.volume;
            s.source.pitch  = s.pitch;
            s.source.loop   = s.loop;
        }

        foreach (Sound s in tutorialSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in levelSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        PlaySceneAudio();

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        StopAll();
    }

    public void PlayRandomSound(string array)
    {
        Sound s = null;

        switch (array)
        {
            case "menu":
                s = menuSounds[UnityEngine.Random.Range(0, menuSounds.Length)];

                break;

            case "tutorial":
                s = tutorialSounds[UnityEngine.Random.Range(0, tutorialSounds.Length)];

                break;

            case "level":
                s = levelSounds[UnityEngine.Random.Range(0, levelSounds.Length)];

                break;

            default:
                Debug.LogWarning("Array " + array + "not found in Audio Manager");

                return;
        }

        // Sound not found
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "not found in" + array + " from Audio Manager");

            return;
        }

        s.source.Play();

        if (!s.loop)
        {
            Invoke("PlaySceneAudio", s.clip.length);
        }
    }

    private void PlaySceneAudio()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

        switch (scene)
        {
            case (int)eCurrentScene.menu:
                PlayRandomSound("menu");

                break;

            case (int)eCurrentScene.tutorial:
                PlayRandomSound("tutorial");

                break;

            case (int)eCurrentScene.level:
                PlayRandomSound("level");

                break;

            default:
                Debug.LogWarning("Current scene doesnt contain audios in Audio Manager");

                break;
        }
    }

    private void StopAll()
    {
        foreach (Sound s in menuSounds)
        {
            s.source.Stop();
        }

        foreach (Sound s in tutorialSounds)
        {
            s.source.Stop();
        }

        foreach (Sound s in levelSounds)
        {
            s.source.Stop();
        }
    }
}
