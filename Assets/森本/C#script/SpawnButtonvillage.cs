using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButtonvillage : MonoBehaviour
{
    public GameObject SpawnButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ƒ{ƒX‚ª“|‚³‚ê‚½‚ç‚ÌğŒ‚ğ‘‚­
    public void OnClickStartButton()
    {
        SpawnButton.gameObject.SetActive(true);

    }

}
