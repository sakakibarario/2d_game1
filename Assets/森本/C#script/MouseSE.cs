using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSE : MonoBehaviour
{
    [SerializeField]
    AudioSource MouseAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        MouseAudioSource.Play();
    }
}
