using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : BaseSound
{
    private static SoundManager instance;
    public static SoundManager Instance => instance;

    [SerializeField] private AudioClip backgroundClip;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
        InitChannel();
        //PlayMusic(backgroundClip);
    }
}
