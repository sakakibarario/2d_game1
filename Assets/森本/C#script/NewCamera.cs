using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{
    GameObject player;
    //画面制御用
    private int CPosleftx = -3;
    private int PPosleftx = 7;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("主人公");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;//playerのポジションを取得
       
        if(transform.position.x < CPosleftx)
        {
            transform.position = new Vector3
           (transform.position.x, transform.position.y, transform.position.z);//画面を固定
        }
        else if(playerPos.x > PPosleftx)
        {
            transform.position = new Vector3
           (playerPos.x, transform.position.y, transform.position.z);//playerに追従
        }

        this.player = GameObject.Find("主人公");
    }
}
