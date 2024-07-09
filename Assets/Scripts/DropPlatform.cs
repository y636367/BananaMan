using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    public GameObject bombEffect;

    [SerializeField]
    private string bomb;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject clone = Instantiate(bombEffect);
            clone.transform.position = transform.position;
            SoundManager.Instance.PlaySoundEffect(bomb);
            Destroy(gameObject, 0.7f);
        }
    }

}
