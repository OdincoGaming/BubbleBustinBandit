using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClickCaster : MonoBehaviour
{
    public InputAction action;
    public LayerMask layerMask;
    public BubbleDamagerBehaviour BubbleDamager;

    public DamageTypeEnum damageType;
    [SerializeField] private DamageTypeColorsSO damageTypeColors;
    [SerializeField] private List<Image> damageColorIndicators;

    private void OnEnable()
    {
        action = new InputAction(binding: "<Pointer>/Press");
        action.performed += OnClick;
        action.Enable();
    }

    private void OnDisable()
    {
        action.performed -= OnClick;
        action.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask))
        {
            BubbleDamager.limiter = 0;
            BubbleDamager.transform.position = hit.point;
            BubbleDamager.transform.LookAt(Camera.main.transform);
            BubbleDamager.gameObject.SetActive(true);
        }
    }

    public void SetDamageType(DamageTypeEnum dt)
    {
        damageType = dt;
    }
    public void SetDamageType(string s)
    {
        if(Enum.TryParse(s, out DamageTypeEnum result))
        {
            SetDamageType(result);
        }
        BubbleDamager.DamageType = damageType;

        switch (damageType)
        {
            case DamageTypeEnum.basic:
                SetIndicatorColors(damageTypeColors.basic);
                break;
            case DamageTypeEnum.blue:
                SetIndicatorColors(damageTypeColors.blue);
                break;
            case DamageTypeEnum.red:
                SetIndicatorColors(damageTypeColors.red);
                break;
            case DamageTypeEnum.green:
                SetIndicatorColors(damageTypeColors.green);
                break;
            case DamageTypeEnum.yellow:
                SetIndicatorColors(damageTypeColors.yellow);
                break;
            case DamageTypeEnum.pink:
                SetIndicatorColors(damageTypeColors.pink);
                break;
            case DamageTypeEnum.orange:
                SetIndicatorColors(damageTypeColors.orange);
                break;
            case DamageTypeEnum.cyan:
                SetIndicatorColors(damageTypeColors.cyan);
                break;
            default:
                break;
        }
    }

    private void SetIndicatorColors(Color c)
    {
        foreach(Image i in damageColorIndicators)
        {
            i.color = c;
        }
    }
}
