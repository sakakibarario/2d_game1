using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{
    public BoxCollider2D bx;
    GameObject player;
    //画面制御用
    private int CPosleftx = -3;
    private float CPosright = 224.5f;
    private int PPosleftx = 7;
    private float i = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("主人公");
        bx = GetComponent<BoxCollider2D>();
        bx.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.pose == false)
        {
            Vector3 playerPos = this.player.transform.position;//playerのポジションを取得

            if (transform.position.x < CPosleftx)
            {
                transform.position = new Vector3
               (transform.position.x, transform.position.y, transform.position.z);//画面を固定
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
                    (transform.position.x + i, transform.position.y, transform.position.z);//画面をスクロール
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
