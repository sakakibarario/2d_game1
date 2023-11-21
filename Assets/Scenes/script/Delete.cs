using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timer);

    }

    // Update is called once per frame
    void Update()
    {

    }

}

