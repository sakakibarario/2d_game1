using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{
    //秒カウントダウン
    public float countdownsecond = 0;

    //分カウントダウン
    public int countdownminute = 3;


    //時間を表示するText型の変数
    public Text timeText;

    //ポーズしてるかどうか
    public static bool isPose = false;
    //ボスを倒したときに返すフラグ
    static public bool BossdownT = false;

    private void Start()
    {
        BossdownT = true;
    }

    //Uodate is called once per frame
    void Update()
    {
        if (BossdownT)
        {
            //クリックされたとき
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                //ポーズ中にクリックされたとき
                if (isPose)
                {
                    //ポーズ状態を解除する
                    isPose = false;
                    PlayerController.pose = false;
                }
                //進行中にクリックされたとき
                else
                {
                    //ポーズ状態にする
                    isPose = true;
                    PlayerController.pose = true;
                }
            }
        }
        else
        {
            return;
        }

        //ポーズ中かどうか
        if(isPose)
        {
            //ポーズ中であることを表示
            //timeText.text = "ポーズ中";
          

            //カウントダウンしない
            return;
        }
        
            //時間をカウントする
            countdownsecond -= Time.deltaTime;

            if (countdownsecond <=0 && countdownminute != 0)
            {
                countdownminute--;
                countdownsecond = 60.0f;
            }

            //時間を表示する
            timeText.text = countdownminute.ToString("00") + ":" + countdownsecond.ToString("f2");

        if(countdownsecond <10)
        {
            timeText.text = countdownminute.ToString("00") + ":0" + countdownsecond.ToString("f2");
        }
        

        //countdownが0以下になったとき
        if (countdownsecond<=0 && countdownminute <= 0)
        {
            timeText.text = "GAME OVER";
            PlayerController.gameState = "gameover";
            SceneManager.LoadScene("Gameover");
        }
    }
}
