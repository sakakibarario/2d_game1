using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadmanBird : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 20.0f;


    private float vec_x_mai = -1.0f;
    private float vec_y_mai = -1.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVec1 = new Vector3(vec_x_mai, vec_y_mai, 0).normalized;
        rb.velocity = moveVec1 * moveSpeed;
    }
}
