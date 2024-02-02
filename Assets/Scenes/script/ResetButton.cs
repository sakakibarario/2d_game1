using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    static public bool Tutflag = true;
    public void OnClickStartButton()//リセットボタンを押したら
    {
        //全ての攻撃フラグをfalseにする
        PlayerController.SougenBoss     = false;
        PlayerController.VillageBoss    = false;
        PlayerController.CastleBoss     = false;
        Tutflag = true;
        AniTutorialc.cnt = 0;
        AniTutorialc.OyaF = false;
        OyaDra.OsaF = false;
        OyaDra.OsaDs = false;
        SougenDra.ToHome = false;
        SpawnText.ones = true;
        SpawnText.onev = true;
        Initiate.Fade(sceneName, fadeColor, fadeSpeed);
    }
}
