using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace MotionTranslator {
    public class VRInput : MonoBehaviour
    {
        public InputDevice _rightController;
        public InputDevice _leftController;
        public InputDevice _HMD;

        public Controller rightController;
        public Controller leftController;
        public Controller HMD;

        void Update()
        {
            if (!_rightController.isValid || !_leftController.isValid || !_HMD.isValid)
                InitializeInputDevices();
        }
        private void InitializeInputDevices()
        {
            
            if(!_rightController.isValid)
                InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController, ref rightController);
            if (!_leftController.isValid) 
                InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref _leftController, ref leftController);
            if (!_HMD.isValid) 
                InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref _HMD, ref HMD);

        }

        private void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice, ref Controller controller)
        {
            List<InputDevice> devices = new List<InputDevice>();
            //Call InputDevices to see if it can find any devices with the characteristics we're looking for
            InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

            //Our hands might not be active and so they will not be generated from the search.
            //We check if any devices are found here to avoid errors.
            if (devices.Count > 0)
            {
                inputDevice = devices[0];
                controller = new Controller(inputDevice);
            }
        }

    }
}