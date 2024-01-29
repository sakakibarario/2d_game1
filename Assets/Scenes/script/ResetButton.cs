using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{    public void OnClickStartButton()//リセットボタンを押したら
    {
        //全ての攻撃フラグをfalseにする
        PlayerController.SougenBoss     = false;
        PlayerController.VillageBoss    = false;
        PlayerController.CastleBoss     = false;
    }
}
