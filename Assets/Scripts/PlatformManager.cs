using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    public GameObject NowPlatform;

    public void PlatformChange(GameObject t_platform, GameObject t_player)
    {
        if(NowPlatform != t_platform)
        {
            if (NowPlatform != null)
            {
                NowPlatform.GetComponent<MovingPlatform>().Clear();
            }

            NowPlatform = t_platform;
            NowPlatform.GetComponent<MovingPlatform>().Subordination(t_player);
        }
    }
    public void ClearPlatformData()
    {
        if (NowPlatform != null)
        {
            NowPlatform.GetComponent<MovingPlatform>().Clear();
            NowPlatform = null;
        }
    }
}
