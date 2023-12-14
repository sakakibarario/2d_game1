using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    //パーティクル用
    public GameObject particlehosi;
    public Vector3 particlePoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DropBall.particleon)
        {
            Instantiate(particlehosi, particlePoint, Quaternion.identity);
            DropBall.particleon = false;
        }
        if(EnemyHeadman.particleonV)
        {
            Instantiate(particlehosi, particlePoint, Quaternion.identity);
            EnemyHeadman.particleonV = false;
        }
        if(Hero.particleonC)
        {
            Instantiate(particlehosi, particlePoint, Quaternion.identity);
            Hero.particleonC = false;
        }

    }
}
