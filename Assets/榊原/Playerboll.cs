using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerboll : MonoBehaviour
{

    Rigidbody2D rb1;
    Rigidbody2D rb2;
    [SerializeField] float moveSpeed = 5.0f;

    public float deleteTime = 3.0f;

    public int vec_x_pra = 1;
    public int vec_x_mai = -1;
    // Start is called before the first frame update
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(5, 5);
        Vector3 moveVec1 = new Vector3(vec_x_pra, 0, 0).normalized;
            rb1.velocity = moveVec1 * moveSpeed; 
    }  
}
