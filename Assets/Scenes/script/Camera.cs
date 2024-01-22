using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public BoxCollider2D bx;
    GameObject player;
    //画面制御用
    private float   CPosleftx = -2.79f;
    private float CPosright = 131.5f;
    private float   PPosleftx = -2.79f;
    private float CScrollx = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("主人公");
        bx = GetComponent<BoxCollider2D>();
        bx.enabled = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (PlayerController.pose == false)
        {
            Vector3 playerPos = this.player.transform.position;//playerのポジションを取得

            if (transform.position.x < CPosleftx)
            {
                transform.position = new Vector3
               (transform.position.x, transform.position.y, transform.position.z);//スタート画面を固定
            }
            else if (BGM.BossStart)
            {
                if (transform.position.x > CPosright)
                {
                    transform.position = new Vector3
                   (transform.position.x, transform.position.y, transform.position.z);//ボス画面を固定
                }
                else
                {
                    bx.enabled = true;
                    transform.position = new Vector3
                    (transform.position.x + CScrollx, transform.position.y, transform.position.z);//画面をスクロール
                }
            }
            else if (playerPos.x > PPosleftx && BGM.BossStart == false)
            {
                transform.position = new Vector3
               (playerPos.x, transform.position.y, transform.position.z);//playerに追従
            }


            this.player = GameObject.Find("主人公");
        }

    }
}
