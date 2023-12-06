using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneraMap3 : MonoBehaviour
{
    public BoxCollider2D bxleft;
    public BoxCollider2D bxup;
    GameObject player;
    //画面制御用
    //private int CPosleftx = -20;
    private float CPosright = 217.0f;
    private int PPosleftx = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("主人公");
        bxleft = GetComponent<BoxCollider2D>();
        bxleft.enabled = false;
        bxup = GetComponent<BoxCollider2D>();
        bxup.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;//playerのポジションを取得

        if (transform.position.x > CPosright)
        {
            transform.position = new Vector3
           (transform.position.x, 0, transform.position.z);//ボス画面を固定
            bxleft.enabled = true;
            Debug.Log("false2");
        }
        else if (playerPos.x > PPosleftx || playerPos.y > 8)
        {
            transform.position = new Vector3
           (playerPos.x, playerPos.y + 3.5f, transform.position.z);//playerに追従
            Debug.Log("false3");
        }
       
        else
        {
           transform.position = new Vector3
           (transform.position.x, 0, transform.position.z);//画面を固定
           
        }


        this.player = GameObject.Find("主人公");
    }
}
