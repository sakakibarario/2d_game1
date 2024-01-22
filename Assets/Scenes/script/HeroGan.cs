using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGan : MonoBehaviour
{
    Rigidbody2D rb1;
    [SerializeField] float moveSpeed = 5.0f;

    float x;
    float y;
    // Start is called before the first frame update
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        x = player.transform.position.x - this.transform.position.x;
        y = player.transform.position.y - this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {     
        //transform.localScale = new Vector2(1, 1);
        if(Deletethunder.HeroDown)
        {
            Destroy(gameObject);
        }
        Vector3 moveVec1 = new Vector3(x, y, 0).normalized;
        rb1.velocity = moveVec1 * moveSpeed;
    }
}
