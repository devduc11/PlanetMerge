using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseSound : BaseMonoBehaviour
{
    #region Variable
    [SerializeField] private int sfxChannelCount = 1;

    private AudioSource backgroundChannel;
    private List<AudioSource> sfxChannels;

    private bool isMusicOn;
    private bool isSfxOn;
    #endregion

    #region Function
    protected void InitChannel()
    {
        sfxChannels = new List<AudioSource>();

        GameObject bgChannel = new()
        {
            name = "Channel Background"
        };
        backgroundChannel = bgChannel.AddComponent<AudioSource>();
        bgChannel.transform.parent = gameObject.transform;

        for (int i = 0; i < sfxChannelCount; i++)
        {
            GameObject channel = new()
            {
                name = $"Channel SFX {i}"
            };
            AudioSource source = channel.AddComponent<AudioSource>();
            sfxChannels.Add(source);
            channel.transform.parent = gameObject.transform;
        }

        isMusicOn = SaveManager.Instance.DataSave.isMusicOn;
        isSfxOn = SaveManager.Instance.DataSave.isSfxOn;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (isMusicOn && clip)
        {
            backgroundChannel.clip = clip;
            backgroundChannel.volume = 1;
            backgroundChannel.mute = isMusicOn;
            backgroundChannel.Play();
            backgroundChannel.loop = true;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (isSfxOn && clip)
        {
            AudioSource _channel = sfxChannels.Find(_as => !_as.isPlaying);
            if (_channel == null)
            {
                _channel = sfxChannels[0];
                _channel.Stop();
            }
            _channel.clip = clip;
            _channel.mute = isSfxOn;
            _channel.PlayOneShot(_channel.clip);
        }
    }

    public bool ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        if (!isMusicOn)
        {
            backgroundChannel.Stop();
        }
        else
        {
            backgroundChannel.Play();
        }
        SaveManager.Instance.DataSave.isMusicOn = isMusicOn;
        return isMusicOn;
    }

    public bool ToggleSFX()
    {
        isSfxOn = !isSfxOn;
        SaveManager.Instance.DataSave.isSfxOn = isSfxOn;
        return isSfxOn;
    }
    #endregion
}
