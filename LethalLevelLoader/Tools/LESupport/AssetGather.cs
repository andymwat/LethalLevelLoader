﻿//Copied from LECore
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Audio;

namespace LethalLevelLoader.Tools;

public class AssetGather
{
    private static AssetGather _instance;
    public static AssetGather Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AssetGather();
            }

            return _instance;
        }
    }
    
    

    //Audio Clips
    public Dictionary<String, AudioClip> audioClips = new Dictionary<String, AudioClip>();
    //Audio Mixers
    public Dictionary<String, (AudioMixer, AudioMixerGroup[])> audioMixers = new Dictionary<String, (AudioMixer, AudioMixerGroup[])>();
    //Planet Prefabs
    public Dictionary<String, GameObject> planetPrefabs = new Dictionary<String, GameObject>();
    //Map Objects
    public Dictionary<String, GameObject> mapObjects = new Dictionary<String, GameObject>();
    //Outside Objects
    public Dictionary<String, SpawnableOutsideObject> outsideObjects = new Dictionary<String, SpawnableOutsideObject>();
    //Scraps
    public Dictionary<String, Item> scraps = new Dictionary<String, Item>();
    //Level Ambiances
    public Dictionary<String, LevelAmbienceLibrary> levelAmbiances = new Dictionary<String, LevelAmbienceLibrary>();
    //Enemies
    public Dictionary<String, EnemyType> enemies = new Dictionary<String, EnemyType>();
    //Sprites
    public Dictionary<String, Sprite> sprites = new Dictionary<String, Sprite>();

    private Sprite scrapSprite;
    public AssetBundle mainAssetBundle;
    
    public Sprite GetScrapSprite()
    {
        if (scrapSprite == null)
        {
            scrapSprite = mainAssetBundle.LoadAsset<Sprite>("Assets/Mods/LethalExpansion/Sprites/ScrapItemIcon2.png");
        }
        return scrapSprite;
    }

    public AudioMixerGroup GetDiageticMasterAudioMixer()
    {
        if (!audioMixers.TryGetValue("Diagetic", out var tuple))
        {
            return null;
        }

        return tuple.Item2.First(a => a.name == "Master");
    }

    private void Add<T>(Dictionary<string, T> dictionary, string key, T value)
    {
        if (key == null || value == null)
        {
            return;
        }

        if (dictionary.ContainsKey(key) || dictionary.ContainsValue(value))
        {
            return;
        }

        dictionary[key] = value;
    }

    public void AddAudioClip(AudioClip clip)
    {
        AddAudioClip(clip?.name, clip);
    }

    public void AddAudioClip(string name, AudioClip clip)
    {
        Add(audioClips, name, clip);
    }

    public void AddAudioClip(AudioClip[] clips)
    {
        if (clips == null)
        {
            return;
        }

        foreach (AudioClip clip in clips)
        {
            AddAudioClip(clip);
        }
    }

    public void AddAudioClip(string[] names, AudioClip[] clips)
    {
        if (names == null || clips == null)
        {
            return;
        }

        for (int i = 0; i < clips.Length && i < names.Length; i++)
        {
            AddAudioClip(names[i], clips[i]);
        }
    }

    public void AddAudioMixer(AudioMixer audioMixer)
    {
        if (audioMixer == null || audioMixers.ContainsKey(audioMixer.name))
        {
            return;
        }

        List<AudioMixerGroup> audioMixerGroups = new List<AudioMixerGroup>();
        foreach (AudioMixerGroup audioMixerGroup in audioMixer.FindMatchingGroups(string.Empty))
        {
            if (audioMixerGroup == null || audioMixerGroups.Contains(audioMixerGroup))
            {
                continue;
            }

            audioMixerGroups.Add(audioMixerGroup);
        }

        Add(audioMixers, audioMixer.name, (audioMixer, audioMixerGroups.ToArray()));
    }

    public void AddPlanetPrefabs(GameObject prefab)
    {
        AddPlanetPrefabs(prefab?.name, prefab);
    }

    public void AddPlanetPrefabs(string name, GameObject prefab)
    {
        Add(planetPrefabs, name, prefab);
    }

    public void AddMapObjects(GameObject mapObject)
    {
        Add(mapObjects, mapObject?.name, mapObject);
    }

    public void AddOutsideObject(SpawnableOutsideObject outsideObject)
    {
        Add(outsideObjects, outsideObject?.name, outsideObject);
    }

    public void AddScrap(Item scrap)
    {
        Add(scraps, scrap?.name, scrap);
    }

    public void AddLevelAmbiances(LevelAmbienceLibrary levelAmbiance)
    {
        Add(levelAmbiances, levelAmbiance?.name, levelAmbiance);
    }

    public void AddEnemies(EnemyType enemy)
    {
        Add(enemies, enemy?.name, enemy);
    }

    public void AddSprites(Sprite sprite)
    {
        AddSprites(sprite?.name, sprite);
    }

    public void AddSprites(string name, Sprite sprite)
    {
        Add(sprites, name, sprite);
    }
}