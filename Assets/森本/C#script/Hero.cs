using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public GameObject Point1;
    public GameObject Point2;
    public GameObject Point3;
    public GameObject thunder;
    public GameObject Player;
    public GameObject DangerArea1;
    public GameObject DangerArea2;
    public GameObject DangerArea3;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int rnd = 1;

        if (collision.gameObject.tag == "Player")//Playerに当たったら
        {
            rnd = Random.Range(1, 4);

            if (rnd == 1)
            {
                //危険エリア表示
                DangerArea1.gameObject.SetActive(true);
                //コルーチン呼び出し
                StartCoroutine(Thunder1());
            }
            if (rnd == 2)
            {
                //危険エリア表示
                DangerArea2.gameObject.SetActive(true);
                //コルーチン呼び出し
                StartCoroutine(Thunder2());
            }
            if (rnd == 3)
            {   //危険エリア表示
                DangerArea3.gameObject.SetActive(true);
                //コルーチン呼び出し
                StartCoroutine(Thunder3());
            }
        }
    } 
    private IEnumerator Thunder1()
    {
        yield return new WaitForSeconds(3.0f);

        //Point1の座標を変数posに保存
        var pos = Point1.gameObject.transform.position;
        //弾のプレハブを作成
        var t = Instantiate(thunder) as GameObject;
        //弾のプレハブの位置を敵の位置にする
        t.transform.position = pos;
        //危険エリア非表示
        DangerArea1.gameObject.SetActive(false);

    }
    private IEnumerator Thunder2()
    {
        yield return new WaitForSeconds(3.0f);
        //Point1の座標を変数posに保存
        var pos = Point2.gameObject.transform.position;
        //弾のプレハブを作成
        var t = Instantiate(thunder) as GameObject;
        //弾のプレハブの位置を敵の位置にする
        t.transform.position = pos;
        //危険エリア非表示
        DangerArea2.gameObject.SetActive(false);
    }
    private IEnumerator Thunder3()
    {
        yield return new WaitForSeconds(3.0f);
        //Point1の座標を変数posに保存
        var pos = Point3.gameObject.transform.position;
        //弾のプレハブを作成
        var t = Instantiate(thunder) as GameObject;
        //弾のプレハブの位置を敵の位置にする
        t.transform.position = pos;
        //危険エリア非表示
        DangerArea3.gameObject.SetActive(false);
    }

}
