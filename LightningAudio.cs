using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAudio : MonoBehaviour
{
    private AudioSource lightnightAudio;
    private ParticleSystem thisParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        lightnightAudio = this.gameObject.GetComponent<AudioSource>();
        thisParticleSystem = this.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Currently not working
        if (thisParticleSystem.isEmitting)
        {
            Debug.Log("IS EMITTING");
            lightnightAudio.Play();
        }
    }
}
