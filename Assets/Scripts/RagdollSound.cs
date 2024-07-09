using UnityEngine;

public class RagdollSound : MonoBehaviour
{
    PlayerSoundPack PSP;
    private void Start()
    {
        PSP = Player.instance.PSP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PSP.Random_Ragdoll();
    }
}
