using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class NPCInfo : MonoBehaviour
{
    
    private Quaternion baseRotation;

    public Image HumorImage1;
    public Image HumorImage2;
    public Image TargetedImage;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        baseRotation = this.transform.rotation;
        if (HumorImage1 != null) HumorImage1.enabled = false;  
        if (HumorImage2 != null) HumorImage2.enabled = false;
    }

    public void SetHumorTypes(HumorTaste humorTaste1, HumorTaste humorTaste2)
    {
        if (humorTaste1.humorType != null && HumorImage1 != null)
        {
            HumorImage1.enabled = true;;
            HumorImage1.sprite = humorTaste1.taste == EHumorType.Classy ? humorTaste1.humorType.ClassySprite : humorTaste1.humorType.CrassSprite;
        }
        if (humorTaste2.humorType != null && HumorImage2 != null)
        {
            HumorImage2.enabled = true;
            HumorImage2.sprite = humorTaste2.taste == EHumorType.Classy ? humorTaste2.humorType.ClassySprite : humorTaste2.humorType.CrassSprite;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        if (spriteRenderer!= null) spriteRenderer.sprite = sprite;
    }

    public void SetAnimator(RuntimeAnimatorController animatorController)
    {
        if (animator!= null) animator.runtimeAnimatorController = animatorController;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        // transform.rotation = baseRotation;
    }
}
