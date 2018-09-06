using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip GunSound;
    AudioSource MyAudio;

    static public SoundManager instance;

    void Awake()
    {
        if(SoundManager.instance==null)
        {
            SoundManager.instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        MyAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound()
    {
        MyAudio.PlayOneShot(GunSound);
    }
}
