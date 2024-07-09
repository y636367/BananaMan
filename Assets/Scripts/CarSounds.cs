using System.Collections;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    [SerializeField]
    private AudioSource Engine;
    [SerializeField]
    private AudioSource CarHorn;

    private void Start()
    {
        StartCoroutine(nameof(Random_Horn));
    }
    private IEnumerator Random_Horn()
    {
        int Random_number = 0;

        while (true)
        {
            Random_number = Random.Range(6, 11);

            if (Random_number / 2 != 0)
            {
                if(CarHorn.mute)
                    CarHorn.mute = false;

                CarHorn.Play();
            }

            yield return new WaitForSeconds(Random_number);
        }
    }
}
