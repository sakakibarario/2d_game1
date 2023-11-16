using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArtillery : MonoBehaviour
{
    public GameObject tama;
    //プレイヤーオブジェクト
    public GameObject player;

    private float Count_artillery = 0;
    private float artillery = 3.0f;

    private bool inDamage = false;
    private bool isActive = false;
    private bool move = true;

    private float reactionDistance = 10.0f;//反応距離

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerController.gameState != "playing")
        {
            return;
        }
        if (player != null)
        {
            if (isActive && move)
            {
                //主人公の座標を変数posに保存
                var posL = this.gameObject.transform.position + transform.right * 1.5f + transform.up * 1.5f;
                //弾のプレハブを作成
                Count_artillery += Time.deltaTime;
                if (Count_artillery > artillery)
                {
                    var t = Instantiate(tama) as GameObject;
                    //弾のプレハブの位置を位置にする
                    t.transform.position = posL;
                    t.AddComponent<LeftArtillery>();
                    Count_artillery = 0;
                    Debug.Log("大砲の玉作成");
                }
            }
            else
            {
                //プレイヤーとの距離を求める
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist < reactionDistance)
                {
                    isActive = true; //アクティブにする
                }
            }
        }
        else if (isActive)
        {
            isActive = false;
        }
        if (inDamage)
        {
            //ダメージ中点滅させる
            float val = Mathf.Sin(Time.time * 50);
            // Debug.Log(val);
            if (val > 0)
            {
                //スプライトを表示
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //スプライトを非表示
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;//ダメージ中は操作による移動はさせない
        }
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log("OntriggerEnter2D:" + collider2D.gameObject.name);

        //突進攻撃との接触
        if (collider2D.gameObject.tag == "rushWall")
        {
            //ダメージ
            inDamage = true;
            move = false;
            EnemyDamage();//倒れているか調べる
        }
        //火球攻撃との接触
        if(collider2D.gameObject.tag == "Fireball")
        {
            //ダメージ
            inDamage = true;
            move = false;
            EnemyDamage();//倒れているか調べる
        }
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
            Debug.Log("敵が倒れている");
            Destroy(gameObject, 0.2f);//0.2かけて敵を消す
    }
    void DamageEnd()
    {
        //ダメージフラグOFF
        inDamage = false;
        //スプライト元に戻す
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
