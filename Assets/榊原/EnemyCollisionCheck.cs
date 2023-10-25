using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionCheck : MonoBehaviour
{
    public bool isOn;

    private string groundTag = "Ground";
    private string enemyTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOn == false)
        {
            if (collision.tag == groundTag || collision.tag == enemyTag)
            {
                isOn = true;
                Debug.Log("”½“]");
            }
        }
        else if (isOn == true)
        {
            if (collision.tag == groundTag || collision.tag == enemyTag)
            {
                isOn = false;
                Debug.Log("”½“]!");
            }
        }
    }
}

 
