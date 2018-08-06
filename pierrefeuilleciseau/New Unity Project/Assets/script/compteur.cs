using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compteur : MonoBehaviour {

    private float time = 10;
	// Use this for initialization
	void Start () {
        if(time > 0)
        {
            time = time - 1;
        }
        if(time == 0)
        {

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
