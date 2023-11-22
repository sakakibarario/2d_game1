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
                //敵の座標を変数posに保存
                var pos = Point1.gameObject.transform.position;
                //弾のプレハブを作成
                var t = Instantiate(thunder) as GameObject;
                //弾のプレハブの位置を敵の位置にする
                t.transform.position = pos;

            }
            if (rnd == 2)
            {
                //敵の座標を変数posに保存
                var pos = Point2.gameObject.transform.position;
                //弾のプレハブを作成
                var t = Instantiate(thunder) as GameObject;
                //弾のプレハブの位置を敵の位置にする
                t.transform.position = pos;
            }
            if (rnd == 3)
            {
                //敵の座標を変数posに保存
                var pos = Point3.gameObject.transform.position;
                //弾のプレハブを作成
                var t = Instantiate(thunder) as GameObject;
                //弾のプレハブの位置を敵の位置にする
                t.transform.position = pos;
            }
        }
    } 
}
