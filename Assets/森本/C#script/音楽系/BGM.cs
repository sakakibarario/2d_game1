using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    //SE用
    [SerializeField]
    AudioSource DefaultAudioSource;

    [SerializeField]
    AudioSource BossAudioSource;
    static public bool BossStart = false;//ボス戦開始

    bool BG;
    bool one;

    // Start is called before the first frame update
    void Start()
    {
        BG = true;
        one = false;
        BossStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(BG)
        {
            if (collision.gameObject.tag == "Player")//Playerに当たったら
            {
                DefaultAudioSource.Stop();
                BossAudioSource.Play();
                one = true;
                BossStart = true;//フラグを上げる
            }
        }
        if(one)
        {
            BG = false;
        }
    }
}
