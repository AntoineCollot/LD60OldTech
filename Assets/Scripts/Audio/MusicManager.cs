using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource source;
    bool isMuted;

    [SerializeField] MachineButton muteButton;

    public static MusicManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();

        muteButton.onValueChanged += OnValueChanged;
        muteButton.SetOnWithoutNotifying(false);
    }

    private void OnDestroy()
    {
        if(muteButton!=null)
            muteButton.onValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(bool isOn)
    {
        Mute(isOn);
    }

    public void Mute(bool value)
    {
        isMuted = value;
        source.mute = isMuted;
    }

    public void ToggleMute()
    {
        Mute(!isMuted);
    }

    public void PlayMusic()
    {
        source.Play();
    }

    public void StopMusic()
    {
        source.Stop();
    }
}
