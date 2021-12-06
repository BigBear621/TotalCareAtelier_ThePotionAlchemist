using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBehaviour : MonoBehaviour, IAttackFunc, IGetHitFunc
{
    private void Start()
    {
        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        while (true)
        {
            int randomAppear = Random.Range(30, 40);
            yield return new WaitForSeconds(randomAppear);
            // GameObject.FindGameObjectWithTag("Devil").SetActive(true);
            Attack();
        }
    }

    bool CheckAniClip(string clipname)
    {
        if (this.GetComponent<Animation>().GetClip(clipname) == null)
            return false;
        else if (this.GetComponent<Animation>().GetClip(clipname) != null)
            return true;

        return false;
    }

    public void Attack()
    {
        if (CheckAniClip("attack_short_001") == false) return;
        {
            GetComponent<Animation>().CrossFade("attack_short_001", 0.0f);
            GetComponent<Animation>().CrossFadeQueued("idle_combat");
            // �÷��̾� hit�� hp�� �پ��� �ڵ� �ʿ�
        }
    }

    public void GetHit()
    {
        // ������ hp�� �پ��� �ڵ� �ʿ�
    }
}