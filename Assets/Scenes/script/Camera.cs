using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("ålŒö");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3
            (playerPos.x, transform.position.y, transform.position.z);

        this.player = GameObject.Find("ålŒö");
    }
}
