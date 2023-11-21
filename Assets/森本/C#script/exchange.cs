using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exchange : MonoBehaviour
{
    public GameObject exchange1;
    public GameObject Button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStartButton()
    {
        exchange1.gameObject.SetActive(true);
        Destroy(gameObject);
        Button.gameObject.SetActive(true);
    }

}
