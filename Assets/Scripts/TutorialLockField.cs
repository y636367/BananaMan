using System;
using UnityEngine;

public class TutorialLockField : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Wall;

    private void Awake()
    {
        foreach(var w in Wall)
        {
            w.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (var w in Wall)
            {
                w.SetActive(true);
                try
                {
                    w.GetComponent<AudioSource>().mute = false;
                }
                catch (MissingComponentException) { }
            }
        }
    }
}
