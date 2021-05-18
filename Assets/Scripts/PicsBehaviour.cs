using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicsBehaviour : MonoBehaviour
{
    public bool isPicky = true;
    private Caractercontroller myPlayer;
    private Renderer renderer;
    private void Start()
    {
        myPlayer = FindObjectOfType<Caractercontroller>();
        renderer = GetComponent<Renderer>();
    }
    private void Update()
    {
        isPicky = myPlayer.stepCounter % 2 == 0;
        if (isPicky) { renderer.material.color = new Color(0,1,0,0.85f); }
        else { renderer.material.color = new Color(0, 1, 0, 0.15f); }
    }
}
