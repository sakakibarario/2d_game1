using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuraimu : MonoBehaviour
{
    public float speed = 0.5f;
    public int hpMax = 10;
    Rigidbody2D rb;
    public float reactionDistance = 4.0f;
    private int rush_damage = 10;
    private bool inDamage = false;

    bool isActive = false;

    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D をとる
        rb = GetComponent<Rigidbody2D>();
        hp = hpMax;
        Debug.Log(rush_damage);
    }

    // Update is called once per frame
    void Update()
    {
        //Player　のゲームオブジェクトを得る
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive && hp > 0)
            {
                // PLAYERの位置を取得
                Vector2 targetPos = player.transform.position;
                // PLAYERのx座標
                float x = targetPos.x;
                // ENEMYは、地面を移動させるので座標は常に0とする
                float y = 0;
                // 移動を計算させるための２次元のベクトルを作る
                Vector2 direction = new Vector2(
                    x - transform.position.x, y).normalized;
                // ENEMYのRigidbody2Dに移動速度を指定する
                rb.velocity = direction * speed;
                //  Debug.Log("スライムムーブ");





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
            rb.velocity = Vector2.zero;
        }
        if (hp <= 0)
        {
            //0.5秒後に消す
            Destroy(gameObject, 0.5f);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rushWall")
        {
            //ダメージ
            hp -= rush_damage;
            if (hp <= 0)
            {             //ゲームオーバー
                Debug.Log("Enemy HP" + hp);
                //あたりを消す
                GetComponent<CircleCollider2D>().enabled = false;
                //移動停止
                rb.velocity = new Vector2(0, 0);
            }
        }




    }
}
