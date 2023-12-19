using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTime : MonoBehaviour
{
    bool kari = false;
    bool kariK = false;
    public GameObject TossinCoolTime;
    public GameObject KakyuCoolTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.isTimeOver == true)//クールタイムじゃない時は通常
        {
            TossinCoolTime.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        else if(PlayerController.isTimeOver == false)//クールタイム中は薄くする
        {
            TossinCoolTime.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 130);
        }

        if(PlayerController.SougenBoss)//１ステージをCLEARしていないと火球のクールタイムグラフィックを表示させない
        {
            KakyuCoolTime.SetActive(true);

            if (PlayerController.K_isTimeOver == true)//クールタイムじゃない時は通常
            {
                KakyuCoolTime.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
            else if (PlayerController.K_isTimeOver == false)//クールタイム中は薄くする
            {
                KakyuCoolTime.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 130);
            }

        }
        else
        {
            KakyuCoolTime.SetActive(false);
        }
    }
}
