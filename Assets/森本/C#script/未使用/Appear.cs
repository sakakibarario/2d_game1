using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    [SerializeField] GameObject home;
    [SerializeField] GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        //ホームへは最初、非表示false
        //獲得テキストは最初、表示true

        home.SetActive(false);
        text.SetActive(true);
        Invoke("homeSet", 2.0f);//←ホームへが出現するまでの秒数
    }

    void homeSet()
    {
        home.SetActive(true);
        Invoke("textSet", 2.0f);
    }

    void textSet()
    {
        text.SetActive(true);
    }
}
