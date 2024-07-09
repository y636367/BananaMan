using UnityEngine;

public class PlayerSoundPack : MonoBehaviour
{
    [Tooltip("플레이어 소리 모음")]
    [SerializeField]
    private string Jump;

    [SerializeField]
    private string Landing_S;

    [SerializeField]
    private string Landing_B;

    [SerializeField]
    private string Death;

    [SerializeField]
    private string Ragdolls;

    [Space(10f)]
    public int Ragdoll_S_Count;

    int DollIsNum;
    
    public void Jump_S()
    {
        SoundManager.Instance.PlaySoundEffect(Jump);
    }
    public void Landing_S_S()
    {
        SoundManager.Instance.PlaySoundEffect(Landing_S);
    }
    public void Landing_B_B()
    {
        SoundManager.Instance.PlaySoundEffect(Landing_B);
    }
    public void Death_S()
    {
        SoundManager.Instance.PlaySoundEffect(Death);
    }
    public void Random_Ragdoll()
    {
        DollIsNum = Random.Range(1, Ragdoll_S_Count + 1);

        string NowRag = Ragdolls + DollIsNum;
        SoundManager.Instance.PlaySoundEffect(NowRag);
    }
}
