using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject Player;
    public GameObject SougenPoint;
    public GameObject VillagePoint;

    bool spawn1 = false;
    bool spawn2 = false;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerController.SougenBoss)
        {
            spawn1 = true;
        }
        if(PlayerController.VillageBoss)
        {
            spawn2 = true;
        }
        if(spawn1)
        {
            Player.transform.position = SougenPoint.transform.position;
            spawn1 = false;
        }
        if (spawn2)
        {
            Player.transform.position = VillagePoint.transform.position;
            spawn2 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
