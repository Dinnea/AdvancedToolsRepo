using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHand : MonoBehaviour
{
    [SerializeField] InputActionProperty _pinchAnimationAction;
    [SerializeField] InputActionProperty _gripAnimationAction;
    [SerializeField] Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        pinch(); 
        grip();
    }

    void pinch()
    {
        float triggerValue = _pinchAnimationAction.action.ReadValue<float>();
       _animator.SetFloat("Trigger", triggerValue);
    }

    void grip()
    {
        float gripvalue = _gripAnimationAction.action.ReadValue<float>();
        _animator.SetFloat("Grip", gripvalue);

    }


}
