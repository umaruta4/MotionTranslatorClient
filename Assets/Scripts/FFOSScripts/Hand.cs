using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using MotionTranslator;

//Hand is used to help animate
public class Hand : MonoBehaviour
{
    //Stores handPrefab to be Instantiated
    public GameObject handPrefab;
    
    //Allows for hiding of hand prefab if set to true
    public bool hideHandOnSelect = false;
    
    //Stores what kind of characteristics we're looking for with our Input Device when we search for it later
    public InputDeviceCharacteristics inputDeviceCharacteristics;

    //Stores the InputDevice that we're Targeting once we find it in InitializeHand()
    private InputDevice _targetDevice;
    private Animator _handAnimator;
    private SkinnedMeshRenderer _handMesh;
    private Controller _controller;

    public int count;

    public void HideHandOnSelect()
    {
        if (hideHandOnSelect)
        {
            _handMesh.enabled = !_handMesh.enabled;
        }
    }
    private void Start()
    {
        count = 0;
        //InitializeHand();
    }

    private void InitializeHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //Call InputDevices to see if it can find any devices with the characteristics we're looking for
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        //Our hands might not be active and so they will not be generated from the search.
        //We check if any devices are found here to avoid errors.
        if (devices.Count > 0)
        {
            _targetDevice = devices[0];

            // Making sure that it's valid before instantiating the hand. - umaruto 31/08/2023
            if(_targetDevice.isValid){
                Debug.LogWarning("Instantiating Hand");
                GameObject spawnedHand = Instantiate(handPrefab, transform);
                _handAnimator = spawnedHand.GetComponent<Animator>();
                _handMesh = spawnedHand.GetComponentInChildren<SkinnedMeshRenderer>();

                _controller = new Controller(_targetDevice);
            }
        }
    }


    // Update is called once per frame
    private void Update()
    {
        //Since our target device might not register at the start of the scene, we continously check until one is found.
        if(!_targetDevice.isValid)
        {
            // When isValid went false again even though _handAnimator already been instantiated, destroy the hand. - umaruto 31/08/2023
            if(_handAnimator != null){
                DestroyHand();
            }
            InitializeHand();
        }
        else
        {
            UpdateHand();
        }
    }

    private void UpdateHand()
    {
        _handAnimator.SetFloat("Trigger", (_controller.getTriggerButton())? 1.0f : 0.0f);
        _handAnimator.SetFloat("Grip", _controller.getGrip());
        _handAnimator.SetFloat("TriggerTouch", (_controller.getTriggerTouch())? 1.0f : 0.0f);
        _handAnimator.SetFloat("ThumbTouch", (_controller.getThumbTouch())? 1.0f : 0.0f);
    }

    // Adding destroy hand just incase if hand went buggy. - umaruto 31/08/2023
    private void DestroyHand()
    {

        Destroy(_handAnimator.gameObject);

        // Reset references
        _handAnimator = null;
        _handMesh = null;
        _controller = null;
    }
}
