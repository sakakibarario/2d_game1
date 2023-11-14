using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerboll2 : MonoBehaviour
{
    Rigidbody2D rb2;
    [SerializeField] float moveSpeed = 5.0f;


    public int vec_x_pra = 1;
    public int vec_x_mai = -1;
    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(-5, 5);
        Vector3 moveVec2 = new Vector3(vec_x_mai, 0, 0).normalized;
            rb2.velocity = moveVec2 * moveSpeed; 
    }
}
