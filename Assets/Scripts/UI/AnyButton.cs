using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Controls;

public class AnyButton : MonoBehaviour
{
    InputMaster input;
    [SerializeField] GameObject hud;

    [SerializeField, Range(0, 2)] float delay;
    float counter;

    
    void Awake()
    {
        input = new InputMaster();
        
        counter = 0f;

        // Sets m_ButtonPressed as soon as a button is pressed on any device.
        InputSystem.onEvent += (eventPtr, device) => {
                if(!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
                    return;
                var controls = device.allControls;
                var buttonPressPoint = InputSystem.settings.defaultButtonPressPoint;
                for (var i = 0; i < controls.Count; ++i) {
                    var control = controls[i] as ButtonControl;
                    if (control == null || control.synthetic || control.noisy)
                        continue;
                    if (control.ReadValueFromEvent(eventPtr, out var value) && value >= buttonPressPoint) {
                        //m_ButtonPressed = true;
                        if(this != null) { anyButton(); }
                        break;
                    }
                }
            };
    }

    void Update() {
        counter += Time.fixedDeltaTime;
    }
    
    void anyButton() {
        if(counter >= delay) {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

}
