using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadman : MonoBehaviour
{
    public GameObject bird;//birdを取得
    public GameObject cane;//caneを取得
    public GameObject player;//playerを取得

    private int Headman_HP;//村長のHP
    public int HP = 100;    //最大HP

    //主人公の攻撃
    private int rushdamage = 10;    //突進の攻撃力
    private int buresball = 30;     //火球の攻撃力


    private bool inDamage = false;  //ダメージ判定
    private float reactionDistance = 10.0f;//反応距離

    bool isActive = false;//動き出しフラグ
    private bool OnAttack = false;

    //bird攻撃time関係
    private float create_bird_count = 0.0f;
    private float create_bird_time = 5.0f;

    //cane攻撃time関係
    private float create_cane_count = 0.0f;
    private float create_cane_time = 3.0f;


    //アニメション
    Animator animator; //アニメーター
    public string attackanime = "EnemyHeadmanAttack";//鳥攻撃のモーション
    public string Caneanime = "EnemyHeadmanCane";    //杖攻撃のモーション
    public string Stopanime = "EnemyHeadmanStop";    //静止モーション

    GameObject[] tagObjects;//オブジェクトカウント用

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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive && Headman_HP > 0)
            {

                create_bird_count += Time.deltaTime;//カウント
                create_cane_count += Time.deltaTime;//カウント
                //Debug.Log(create_bird_count);
                if (create_bird_count > create_bird_time)
                {   
                    StartCoroutine(Bird_attack());//コルーチン開始
                }
                else if(create_cane_count > create_cane_time)
                {
                    StartCoroutine(Cane_attack());//コルーチン開始
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
            isActive = false;//非アクティブ
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
            //Debug.Log("鳥生成");
            float vecX = Random.Range(135f, 150f);//ランダムを（）範囲で設定
            var t = Instantiate(bird) as GameObject;//オブジェクトを作成

            //弾のプレハブの位置を敵の位置にする
            t.transform.position = new Vector3(vecX, 10.0f, 0);

            t.AddComponent<HeadmanBird>();//birdの動きを決める
            tagObjects = GameObject.FindGameObjectsWithTag("bird");//数を数える
            if (tagObjects.Length >= 1)
            {
                OnAttack = false;
                create_bird_count = 0.0f;//リセット
                create_cane_count = 2.0f;//リセット
            }
        }
    }
    //コルーチン
    private IEnumerator Bird_attack()
    {
        //アニメション
        animator.Play(attackanime);
        yield return new WaitForSeconds(0.2f);//0.2静止

        OnAttack = true;//攻撃（bird）のフラグを上げる

        yield return new WaitForSeconds(0.5f);//0.5静止

        //Debug.Log("終了");
        yield break;//コルーチン終了
        
    }
    private IEnumerator Cane_attack()
    {
        //アニメーション
        animator.Play(Caneanime);
        yield return new WaitForEndOfFrame();//１フレーム静止
        var pos = this.gameObject.transform.position + transform.up *2.0f - transform.right*2.0f; //位置設定
        Instantiate(cane, pos, Quaternion.identity);//作成

        create_cane_count =0.0f;//リセット

        yield return new WaitForSeconds(0.5f);//０．５静止
        animator.Play(Stopanime);

        yield break;//コルーチン終了

    }
    //接触管理
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
            Destroy(collider.gameObject);
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
