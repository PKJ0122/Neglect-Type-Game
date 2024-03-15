using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Animator animator;
    public Text damageText;

    public void SetDamageText(int damage)
    {
        animator.SetTrigger("On");
        damageText.text = $"{string.Format("{0:#,###}", damage)}";
    }

    public void SetDamageText(int damage, bool gold)
    {
        animator.SetTrigger("On");
        damageText.text = $"+{string.Format("{0:#,###}", damage)}";
    }

    public void OffObject()
    {
        gameObject.SetActive(false);
    }
}
