using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace MotionTranslator {
    public class Controller
    {
        private InputDevice _controller;
        public Controller(InputDevice controller)
        {
            _controller = controller;
        }

        public InputDevice getController()
        {
            return _controller;
        }

        public bool getThumbTouch()
        {
            bool thumbTouch = (getJoyStickTouch() || getPrimaryTouch() || getSecondaryTouch());
            return thumbTouch;
        }

        public bool getTriggerTouch()
        {
            
            if (_controller.TryGetFeatureValue(new ("TriggerTouch"), out bool triggerTouch))
            {
                return triggerTouch;
            }

            return false;
        }

        public bool getTriggerButton()
        {
            if (_controller.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButton))
            {
                return triggerButton;
            }

            return false;
        }

        public float getGrip()
        {
            if (_controller.TryGetFeatureValue(CommonUsages.grip, out float grip))
            {
                return grip;
            }

            return 0.0f;
        }

        public Vector3 getVelocity()
        {
            if (_controller.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 deviceVelocity))
            {
                return deviceVelocity;
            }

            return new Vector3();
        }

        public Quaternion getRotation()
        {
            if (_controller.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion deviceRotation))
            {
                return deviceRotation;
            }

            return new Quaternion();
        }

        public Vector3 getPosition()
        {
            if (_controller.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 devicePosition))
            {
                return devicePosition;
            }

            return new Vector3();
        }

        public bool getJoyStickTouch()
        {
            if (_controller.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool joyStickTouch))
            {
                return joyStickTouch;
            }

            return false;
        }

        public bool getPrimaryTouch()
        {
            if (_controller.TryGetFeatureValue(CommonUsages.primaryTouch, out bool primaryTouch))
            {
                return primaryTouch;
            }

            return false;
        }

        public bool getSecondaryTouch()
        {
            if (_controller.TryGetFeatureValue(CommonUsages.secondaryTouch, out bool secondaryTouch))
            {
                return secondaryTouch;
            }

            return false;
        }
    }
}

