using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    //主人公の攻撃関係
    public static int GRush = 10;
    public static int GBures = 30;

    //主人公回復アイテム
    public static int GRecoveryMeat = 20;

    //clear変数
    public static bool Clear = false;

    //game play　フラグ
    public static bool Splaying = false;
    public static bool Vplaying = false;
    public static bool Cplaying = false;

    //clear時間記憶用
    public static int Sminute =0;
    public static float Ssecond = 0;

    public static int Vminute = 0;
    public static float Vsecond = 0;

    public static int Cminute = 0;
    public static float Csecond = 0;
}
