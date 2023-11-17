using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadman : MonoBehaviour
{
    public GameObject bird;//birdを取得
    public GameObject player;

    private int Headman_HP;
    public int HP = 100;

    //主人公の攻撃
    private int rushdamage = 10;    //突進の攻撃力
    private int buresball = 30;     //火球の攻撃力

    private bool inDamage = false;  //ダメージ判定

    private float reactionDistance = 10.0f;//反応距離

    bool isActive = false;//動き出しフラグ
    private bool OnAttack = false;

    private float create_bird_count = 0.0f;
    private float create_time = 5.0f;

    //アニメション
    Animator animator; //アニメーター
    public string attackanime = "EnemyHeadmanAttack";
    public string Caneanime = "EnemyHeadmanCane";
    public string Stopanime = "EnemyHeadmanStop";

    GameObject[] tagObjects;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Headman_HP = HP;//最大HPを設定
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerController.gameState != "playing")
        {
            return;
        }
        //Player　のゲームオブジェクトを得る
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive && Headman_HP > 0)
            {
               
                create_bird_count += Time.deltaTime;
                Debug.Log(create_bird_count);
                if (create_bird_count > create_time)
                {
                   // animator.Play(attackanime);
                    StartCoroutine(bird_attack());
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
    private void FixedUpdate()
    {
        if (OnAttack)
        {
            animator.Play(Stopanime);
            Debug.Log("鳥生成");
            float vecX = Random.Range(-5.0f, 7.0f);
           // Check("bird");
            var t = Instantiate(bird) as GameObject;

            //弾のプレハブの位置を敵の位置にする
            t.transform.position = new Vector3(vecX, 5.0f, 0);

            t.AddComponent<HeadmanBird>();
            tagObjects = GameObject.FindGameObjectsWithTag("bird");
            if (tagObjects.Length >= 1)
            {
                OnAttack = false;
                create_bird_count = 0.0f;
            }

        }
        else
        {
            
        }
    }
    //コルーチン
    private IEnumerator bird_attack()
    {
        //アニメション
        animator.Play(attackanime);
        yield return new WaitForSeconds(0.2f);

        OnAttack = true;

        Debug.Log("終了");
        yield break;
        
    }
    void Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagname);
        if (tagObjects.Length >= 1)
        {
            OnAttack = false;
            create_bird_count = 0.0f;
        }
    }

        private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("OntriggerEnter2D:" + collider.gameObject.name);

        //突進攻撃との接触
        if (collider.gameObject.tag == "rushWall")
        {
            //ダメージ
            Headman_HP -= rushdamage;
            Debug.Log(Headman_HP);
            inDamage = true;
        }
        if(collider.gameObject.tag =="Fireball")
        {
            //ダメージ
            Headman_HP -= buresball;
            Debug.Log(Headman_HP);
            inDamage = true;
        }

        EnemyDamage();//倒れているか調べる
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Headman_HP <= 0)
        {
            Debug.Log("敵が倒れている");
            Destroy(gameObject, 0.2f);//0.2かけて敵を消す
        }
    }
    void DamageEnd()
    {
        //ダメージフラグOFF
        inDamage = false;
        //スプライト元に戻す
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }


}
