using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("load audio");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger:"+other.tag);
        if (other.tag == "badminton")
        {
            Debug.Log("badminton!");
            audioSource.Play();
        }
    }
}
