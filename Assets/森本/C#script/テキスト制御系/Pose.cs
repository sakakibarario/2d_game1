using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pose : MonoBehaviour
{
    public GameObject PoseText;
    public GameObject restartText;
    public GameObject ToTitle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeCounter.isPose == true)
        {
            PoseText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);
            ToTitle.gameObject.SetActive(true);
        }
        else
        {
            PoseText.gameObject.SetActive(false);
            restartText.gameObject.SetActive(false);
            ToTitle.gameObject.SetActive(false);

        }

    }


}
