using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Animator), typeof(Material))]
public class HoldBlock : MonoBehaviour
{
    Animator animator;
    BoxCollider boxcollider;
    AudioSource source;
    
    public Material material;

    [SerializeField]
    private float ReturnTime;

    bool disappear;
    bool nowShake;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider>();
        source = GetComponent<AudioSource>();

        disappear = false;

        Color color = material.color;
        color.a = 1;
        material.color = color;

        StartCoroutine(nameof(AnimationPause));
    }
    /// <summary>
    /// �ִϸ����� ���� �� ���
    /// </summary>
    /// <returns></returns>
    private IEnumerator AnimationPause()
    {
        while (true)
        {
            if (GameManager.instance.isPause)
                animator.speed = 0;
            else
                animator.speed = 1;

            yield return null;
        }
    }
    /// <summary>
    /// �ڽ� ����� ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator Disappear()
    {
        float count = 0;

        while (true)
        {
            while (GameManager.instance.isPause)
                yield return null;

            if (count >= ReturnTime)
                break;

            count += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        animator.SetTrigger("Appear");
        boxcollider.enabled = true;
    }
    /// <summary>
    /// Ư�� �ִϸ��̼� ���
    /// </summary>
    public void TriggerOn()
    {
        if (!nowShake && !disappear)
        {
            nowShake = true;
            animator.SetTrigger("Shake");
        }
    }
    /// <summary>
    /// �ִϸ��̼� ���� ���� Ȯ�� �� �� ���� �� ����
    /// </summary>
    public void animationFin()
    {
        animator.SetTrigger("Fin");

        if (!disappear)
        {
            if (source.mute)
                source.mute = false;

            source.Play();

            disappear = true;
            boxcollider.enabled = false;
            nowShake = false;
        }
        else
        {
            disappear = false;

            Color color = material.color;
            color.a = 1;
            material.color = color;
        }
    }
    /// <summary>
    /// �ڽ� ����� ����
    /// </summary>
    public void StateFales()
    {
        Color color = material.color;
        color.a = 0;
        material.color = color;

        StartCoroutine(nameof(Disappear));
    }
}
