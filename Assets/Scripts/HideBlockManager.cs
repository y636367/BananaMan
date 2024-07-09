using UnityEngine;

public class HideBlockManager : MonoBehaviour
{
    [SerializeField]
    private HideBlock[] PurPle;

    [SerializeField]
    private HideBlock[] Gray;

    [SerializeField]
    private HideBlock[] Yellow;

    [SerializeField]
    private HideBlock[] Orange;

    [SerializeField]
    private HideBlock[] Brown;

    public void CheckNumberAppear(int number)
    {
        switch(number)
        {
            case 0:
                foreach (var item in PurPle)
                {
                    item.StopCoroutines();
                    item.StartStartAppear();
                }
                break;
            case 1:
                foreach (var item in Gray)
                {
                    item.StopCoroutines();
                    item.StartStartAppear();
                }
                break;
            case 2:
                foreach (var item in Yellow)
                {
                    item.StopCoroutines();
                    item.StartStartAppear();
                }
                break;
            case 3:
                foreach (var item in Orange)
                {
                    item.StopCoroutines();
                    item.StartStartAppear();
                }
                break;
            case 4:
                foreach (var item in Brown)
                {
                    item.StopCoroutines();
                    item.StartStartAppear();
                }
                break;
        }
    }
    public void CheckNumberHide(int number)
    {
        switch (number)
        {
            case 0:
                foreach (var item in PurPle)
                {
                    item.StopCoroutines();
                    item.StartHide();
                }
                break;
            case 1:
                foreach (var item in Gray)
                {
                    item.StopCoroutines();
                    item.StartHide();
                }
                break;
            case 2:
                foreach (var item in Yellow)
                {
                    item.StopCoroutines();
                    item.StartHide();
                }
                break;
            case 3:
                foreach (var item in Orange)
                {
                    item.StopCoroutines();
                    item.StartHide();
                }
                break;
            case 4:
                foreach (var item in Brown)
                {
                    item.StopCoroutines();
                    item.StartHide();
                }
                break;
        }
    }
}
