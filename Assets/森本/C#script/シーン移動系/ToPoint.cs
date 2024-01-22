using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPoint : MonoBehaviour
{
    public GameObject Player;
    public GameObject Point;
    public GameObject SougenPoint;
    public GameObject MuraPoint;

    bool sougen;
    bool mura;
    void Start()
    {
        sougen  = PlayerController.SougenBoss;
        mura    = PlayerController.VillageBoss;

        if(sougen)
        {
            Player.transform.position = SougenPoint.transform.position;
        }
        if(mura)
        {
            Player.transform.position = MuraPoint.transform.position;
        }
    }

    public void OnClickStartButton()
    {
        //主人公のpositionがポイントのpositionになる
        Player.transform.position = Point.transform.position;
    }

}
