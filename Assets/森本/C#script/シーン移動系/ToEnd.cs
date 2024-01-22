using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToEnd : MonoBehaviour
{

    static public bool END_ANI = false;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//Player‚ª“–‚½‚Á‚½‚ç
        {
            StartCoroutine(EndP());
        }
    }
    IEnumerator EndP()
    {
        END_ANI = true;
        yield return new WaitForSeconds(1.0f);

        Destroy(Player);

        yield break;
    }
}