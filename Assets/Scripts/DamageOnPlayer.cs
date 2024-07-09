using UnityEngine;

public class DamageOnPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.instance.DamageOn = true;
    }
}
