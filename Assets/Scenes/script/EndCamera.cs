using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCamera : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("主人公");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;//playerのポジションを取得
        transform.position = new Vector3
                 (playerPos.x, transform.position.y, transform.position.z);//playerに追従
    }
}
