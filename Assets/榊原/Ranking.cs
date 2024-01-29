using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    public int Sfastminute=0; 
    public float SfastSecond=0;

    public int Vfastminute=0;
    public float VfastSecond=0;

    public int Cfastminute=0;
    public float CfastSecond=0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.Splaying)//草原ステージ
        {
            //残り時間が大きいほうを記憶
            if (Global.Ssecond > SfastSecond)
            {
                SfastSecond = Global.Ssecond;
                if (Global.Sminute > Sfastminute)
                {
                    Sfastminute = Global.Sminute;
                }
            }
            //時間を表示する
            if (Global.Ssecond < 10)
            {
                Debug.Log(Sfastminute.ToString("00") + ":0" + SfastSecond.ToString("f2"));
            }
            else
            {
                Debug.Log(Sfastminute.ToString("00") + ":" + SfastSecond.ToString("f2"));
            }
        }
        else if (Global.Vplaying)//村ステージ
        {
            //残り時間が大きいほうを記憶
            if (Global.Vsecond > VfastSecond)
            {
                VfastSecond = Global.Vsecond;
                if (Global.Vminute > Vfastminute)
                {
                    Vfastminute = Global.Vminute;
                }
            }
        }
        else if (Global.Cplaying)//城ステージ
        {
            //残り時間が大きいほうを記憶
            if (Global.Csecond > CfastSecond)
            {
                CfastSecond = Global.Csecond;
                if (Global.Cminute > Cfastminute)
                {
                    Cfastminute = Global.Cminute;
                }
            }
        }
        
    }
    
}
