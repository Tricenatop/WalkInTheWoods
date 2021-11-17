using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public GameObject handPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedHandModel;
    private Animator handAnimator;
    
    void Start()
    {
        TryInitialize();
        
    }
    void TryInitialize() 
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            spawnedHandModel = Instantiate(handPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();

        }
    }
    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }


        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }

    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        UpdateHandAnimation();
        ////Detects when you use the primary Button and stores the value in primaryButtonValue
        //targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        //if (primaryButtonValue)
        //    Debug.Log("Pressing Button");

        ////Detects when you use the trigger and stores the value in triggerValue
        //targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        //if (triggerValue > 0.1f)
        //    Debug.Log("Pressing Button " + triggerValue);

        ////Detects when you use the touchpad and stores the value in primary2DAxisValue
        //targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        //if (primary2DAxisValue != Vector2.zero)
        //    Debug.Log("Pressing Touchpad " + primary2DAxisValue);

    }
}
