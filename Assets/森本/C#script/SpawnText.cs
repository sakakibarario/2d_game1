using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnText : MonoBehaviour
{
    public GameObject KakyuText;
    public GameObject HisyouText;
    static public bool SpTextS;
    static public bool SpTextV = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.SougenBoss)//ëêå¥CLEAR
        {
            if(SpTextS && DropBall.oneS)
            {
                KakyuText.gameObject.SetActive(true);
                HisyouText.gameObject.SetActive(false);
                //DropBall.oneS = false;
            }

            if (PlayerController.VillageBoss)//ë∫CLEAR
            {
                if(SpTextV && EnemyHeadman.one)
                {                   
                    HisyouText.gameObject.SetActive(true);
                    KakyuText.gameObject.SetActive(false);
                }
                EnemyHeadman.one = false;
            }
        }
    }
}
