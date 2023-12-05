using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMouseSE : MonoBehaviour
{
    [SerializeField]
    AudioSource MouseAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        MouseAudioSource = GetComponent<AudioSource>();
        //MouseAudioSource.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStartButton()
    {
        MouseAudioSource.mute = !MouseAudioSource.mute;
    }
}
