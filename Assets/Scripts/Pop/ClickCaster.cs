using UnityEngine;
using UnityEngine.InputSystem;

public class ClickCaster : MonoBehaviour
{
    public InputAction action;
    public LayerMask layerMask;
    public DamageTypeEnum damageType;
    public BubbleDamagerBehaviour BubbleDamager;

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
        if (Physics.Raycast(ray, out hit, 1000f, ~layerMask))
        {
            BubbleDamager.limiter = 0;
            BubbleDamager.transform.position = hit.point;
            BubbleDamager.transform.LookAt(Camera.main.transform);
            BubbleDamager.gameObject.SetActive(true);
        }
    }
}
