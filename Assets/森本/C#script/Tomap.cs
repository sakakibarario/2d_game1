using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tomap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        retry.Sougenretry = true;
        retry.Muraretry = false;
        retry.Siroretry = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("map");
    }

}
