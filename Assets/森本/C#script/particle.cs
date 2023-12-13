using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    ParticleSystem particlehosi;

    // Start is called before the first frame update
    void Start()
    {
        particlehosi = this.GetComponent<ParticleSystem>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //if(particlehosi.isStopped)//パーティクルが終了したか判別
        //{
        //    Destroy(this.gameObject);//パーティクル用ゲームオブジェクトを削除
        //}
    }
}
