using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    //SE—p
    [SerializeField]
    AudioSource DefaultAudioSource;

    [SerializeField]
    AudioSource BossAudioSource;

    bool BG;

    // Start is called before the first frame update
    void Start()
    {
        BG = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(BG)
        {
            DefaultAudioSource.Stop();
            BossAudioSource.Play();
        }
        BG = false;
    }
}
