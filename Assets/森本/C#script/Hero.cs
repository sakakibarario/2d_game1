using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public GameObject Point1;
    public GameObject Point2;
    public GameObject Point3;
    public GameObject thunder;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int rnd;

        for(int i = 0;i<5;i++)
        {
            if (Player.transform.position.x >0)
            {
                 rnd= Random.Range(1, 4);

                if(rnd == 1)
                {
                    Point1.transform.position = thunder.transform.position;
                }
                if (rnd == 2)
                {
                    Point2.transform.position = thunder.transform.position;
                }
                if (rnd == 3)
                {
                    Point3.transform.position = thunder.transform.position;
                }
            }

        }



    }
}
