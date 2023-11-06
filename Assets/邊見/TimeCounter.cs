using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{
    //秒カウントダウン
    private float countdownsecond = 0;

    //分カウントダウン
    public int countdownminute = 1;


    //時間を表示するText型の変数
    public Text timeText;

    //ポーズしてるかどうか
    private bool isPose = false;

    //Uodate is called once per frame
    void Update()
    {
        //クリックされたとき
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            //ポーズ中にクリックされたとき
            if(isPose)
            {
                //ポーズ状態を解除する
                isPose = false;
                PlayerController.pose = false;
            }
            //進行中にクリックされたとき
            else{
                //ポーズ状態にする
                isPose = true;
                PlayerController.pose = true;
            }
        }

        //ポーズ中かどうか
        if(isPose)
        {
            //ポーズ中であることを表示
            timeText.text = "ポーズ中";
          

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
        

        //countdownが0以下になったとき
        if (countdownsecond<=0 && countdownminute <= 0)
        {
            timeText.text = "GAME OVER";
            PlayerController.gameState = "gameover";
            SceneManager.LoadScene("Gameover");
        }
    }
}
