using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    //プレイヤーオブジェクト
    public GameObject player;

    //弾のプレハブオブジェクト
    public GameObject tama;

    //1秒ごとに弾を発射するためのもの
    private float targetTime = 3.0f;
    private float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        //1秒経つごとに弾を発射
        currentTime += Time.deltaTime;

        if(targetTime<currentTime)
        {
            currentTime = 0;
            //敵の座標を変数posに保存
            var pos = this.gameObject.transform.position;

            //弾のプレハブを作成
            var t = Instantiate(tama) as GameObject;

            //弾のプレハブの位置を敵の位置にする
            t.transform.position = pos;

            //敵からプレイヤーに向かうベクトルを作る
            //プレイヤーの位置から敵の位置(弾の位置)を引く
            Vector2 vec = player.transform.position - pos;
            
            //弾のRigidBody2Dコンポネントのvelocityにさっき求めたベクトルを入れて力を加える
            t.GetComponent<Rigidbody2D>().velocity = vec;
        }
    }
}
