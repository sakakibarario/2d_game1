using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesIwa : MonoBehaviour
{
    public GameObject Iwa;

    //ブロックが消えるまでの時間
    private float targetTime = 2.0f;

    //カウントしている時間を入れる変数
    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IwaCrash.Iwakieflag)
        {
            Debug.Log("岩フラグtrue");
            currentTime += Time.deltaTime;
            Iwa.gameObject.SetActive(false);

            if (targetTime < currentTime)//2.0f
            {
                Iwa.gameObject.SetActive(true);
                Debug.Log("復活");
                IwaCrash.Iwakieflag = false;
                IwaCrash.Iwaflag = false;
                currentTime = 0.0f;
            }
        }
    }
}
