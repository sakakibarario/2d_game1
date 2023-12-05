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
    bool one;

    // Start is called before the first frame update
    void Start()
    {
        BG = true;
        one = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(BG)
        {
            if (collision.gameObject.tag == "Player")//Player‚É“–‚½‚Á‚½‚ç
            {
                DefaultAudioSource.Stop();
                BossAudioSource.Play();
                one = true;
            }
        }
        if(one)
        {
            BG = false;
        }
    }
}
