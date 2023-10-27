using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBall : MonoBehaviour
{
    public GameObject ball;
    private float count = 1.0f;
    private int vecX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        if(count <= 0)
        {
            vecX = Random.Range(-9, 10);

            Instantiate(ball, new Vector3(vecX, -4.3f, 0), Quaternion.identity);

            count = 1.0f;
        }
    }
}
