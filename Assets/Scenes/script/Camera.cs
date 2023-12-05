using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public BoxCollider2D bx;
    GameObject player;
    //画面制御用
    private int   CPosleftx = -3;
    private float CPosright = 131.5f;
    private int   PPosleftx = -3;

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
        Vector3 playerPos = this.player.transform.position;//playerのポジションを取得

        if (transform.position.x < CPosleftx)
        {
            transform.position = new Vector3
           (transform.position.x, transform.position.y, transform.position.z);//スタート画面を固定
        }
        else if (transform.position.x > CPosright)
        {
            transform.position = new Vector3
           (transform.position.x, transform.position.y, transform.position.z);//ボス画面を固定
            bx.enabled = true;
        }
        else if (playerPos.x > PPosleftx)
        {
            transform.position = new Vector3
           (playerPos.x, transform.position.y, transform.position.z);//playerに追従
        }
        

        this.player = GameObject.Find("主人公");
    }
}
