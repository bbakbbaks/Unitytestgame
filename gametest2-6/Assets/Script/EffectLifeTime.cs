using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifeTime : MonoBehaviour {

    public float Lifetime = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Lifetime -= Time.deltaTime;
        if (Lifetime <= 0)
        {
            Destroy(gameObject);
        }
	}
}
