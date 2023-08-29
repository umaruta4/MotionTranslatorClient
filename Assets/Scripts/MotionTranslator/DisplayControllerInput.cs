using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

namespace MotionTranslator {

    [RequireComponent(typeof(VRInput))]
    public class DisplayControllerInput : MonoBehaviour
    {
        // Left Controller
        public TextMeshProUGUI leftTriggerTouchValue;
        public TextMeshProUGUI leftTriggerPressedValue;
        public TextMeshProUGUI leftGripPressedValue;
        public TextMeshProUGUI leftThumbTouchValue;
        public TextMeshProUGUI leftPosXValue;
        public TextMeshProUGUI leftPosYValue;
        public TextMeshProUGUI leftPosZValue;
        public TextMeshProUGUI leftVelXValue;
        public TextMeshProUGUI leftVelYValue;
        public TextMeshProUGUI leftVelZValue;
        public TextMeshProUGUI leftQuaternionWValue;
        public TextMeshProUGUI leftQuaternionXValue;
        public TextMeshProUGUI leftQuaternionYValue;
        public TextMeshProUGUI leftQuaternionZValue;

        // Right Controller
        public TextMeshProUGUI rightTriggerTouchValue;
        public TextMeshProUGUI rightTriggerPressedValue;
        public TextMeshProUGUI rightGripPressedValue;
        public TextMeshProUGUI rightThumbTouchValue;
        public TextMeshProUGUI rightPosXValue;
        public TextMeshProUGUI rightPosYValue;
        public TextMeshProUGUI rightPosZValue;
        public TextMeshProUGUI rightVelXValue;
        public TextMeshProUGUI rightVelYValue;
        public TextMeshProUGUI rightVelZValue;
        public TextMeshProUGUI rightQuaternionWValue;
        public TextMeshProUGUI rightQuaternionXValue;
        public TextMeshProUGUI rightQuaternionYValue;
        public TextMeshProUGUI rightQuaternionZValue;

        private VRInput _controller;

        private void Start()
        {
            _controller = GetComponent<VRInput>();
        }
        // Update is called once per frame
        void Update()
        {
            if(_controller._leftController.isValid){
                DisplayLeftController();
            }

            if(_controller._rightController.isValid){
                DisplayRightController();
            }
        }

        void DisplayLeftController()
        {
            leftTriggerTouchValue.text = _controller.leftController.getTriggerTouch().ToString();
            leftTriggerPressedValue.text = _controller.leftController.getTriggerButton().ToString();
            leftGripPressedValue.text = _controller.leftController.getGrip().ToString();
            leftThumbTouchValue.text = _controller.leftController.getThumbTouch().ToString();

            Vector3 position = _controller.leftController.getPosition();
            leftPosXValue.text = position.x.ToString();
            leftPosYValue.text = position.y.ToString();
            leftPosZValue.text = position.z.ToString();

            Vector3 velocity = _controller.leftController.getVelocity();
            leftVelXValue.text = velocity.x.ToString();
            leftVelYValue.text = velocity.y.ToString();
            leftVelZValue.text = velocity.z.ToString();

            Quaternion quaternion = _controller.leftController.getRotation();
            leftQuaternionWValue.text = quaternion.w.ToString();
            leftQuaternionXValue.text = quaternion.x.ToString();
            leftQuaternionYValue.text = quaternion.y.ToString();
            leftQuaternionZValue.text = quaternion.z.ToString();
        }

        void DisplayRightController()
        {
            rightTriggerTouchValue.text = _controller.rightController.getTriggerTouch().ToString();
            rightTriggerPressedValue.text = _controller.rightController.getTriggerButton().ToString();
            rightGripPressedValue.text = _controller.rightController.getGrip().ToString();
            rightThumbTouchValue.text = _controller.rightController.getThumbTouch().ToString();

            Vector3 position = _controller.rightController.getPosition();
            rightPosXValue.text = position.x.ToString();
            rightPosYValue.text = position.y.ToString();
            rightPosZValue.text = position.z.ToString();

            Vector3 velocity = _controller.rightController.getVelocity();
            rightVelXValue.text = velocity.x.ToString();
            rightVelYValue.text = velocity.y.ToString();
            rightVelZValue.text = velocity.z.ToString();

            Quaternion quaternion = _controller.rightController.getRotation();
            rightQuaternionWValue.text = quaternion.w.ToString();
            rightQuaternionXValue.text = quaternion.x.ToString();
            rightQuaternionYValue.text = quaternion.y.ToString();
            rightQuaternionZValue.text = quaternion.z.ToString();
        }
    }
}