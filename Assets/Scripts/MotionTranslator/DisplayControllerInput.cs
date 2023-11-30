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
            Controller vrHeadset = _controller.HMD;

            Vector3 headsetPosition = vrHeadset.getPosition();
            Vector3 headsetVelocity = vrHeadset.getVelocity();
            Quaternion headsetRotation = vrHeadset.getRotation();
            
            leftTriggerTouchValue.text = _controller.leftController.getTriggerTouch().ToString();
            leftTriggerPressedValue.text = _controller.leftController.getTriggerButton().ToString();
            leftGripPressedValue.text = _controller.leftController.getGrip().ToString();
            leftThumbTouchValue.text = _controller.leftController.getThumbTouch().ToString();

            Vector3 position = _controller.leftController.getPosition();
            Vector3 transformedLeftPosition = headsetRotation * (position - headsetPosition);
            leftPosXValue.text = transformedLeftPosition.x.ToString();
            leftPosYValue.text = transformedLeftPosition.y.ToString();
            leftPosZValue.text = transformedLeftPosition.z.ToString();

            Vector3 velocity = _controller.leftController.getVelocity();
            Vector3 transformedLeftVelocity = velocity - headsetVelocity;
            leftVelXValue.text = transformedLeftVelocity.x.ToString();
            leftVelYValue.text = transformedLeftVelocity.y.ToString();
            leftVelZValue.text = transformedLeftVelocity.z.ToString();

            Quaternion quaternion = _controller.leftController.getRotation();
            Quaternion transformedLeftRotation = Quaternion.Inverse(headsetRotation) * quaternion;
            leftQuaternionWValue.text = transformedLeftRotation.w.ToString();
            leftQuaternionXValue.text = transformedLeftRotation.x.ToString();
            leftQuaternionYValue.text = transformedLeftRotation.y.ToString();
            leftQuaternionZValue.text = transformedLeftRotation.z.ToString();
        }

        void DisplayRightController()
        {
            Controller vrHeadset = _controller.HMD;

            Vector3 headsetPosition = vrHeadset.getPosition();
            Vector3 headsetVelocity = vrHeadset.getVelocity();
            Quaternion headsetRotation = vrHeadset.getRotation();

            rightTriggerTouchValue.text = _controller.rightController.getTriggerTouch().ToString();
            rightTriggerPressedValue.text = _controller.rightController.getTriggerButton().ToString();
            rightGripPressedValue.text = _controller.rightController.getGrip().ToString();
            rightThumbTouchValue.text = _controller.rightController.getThumbTouch().ToString();

            Vector3 position = _controller.rightController.getPosition();
            Vector3 transformedRightPosition = headsetRotation * (position - headsetPosition);
            rightPosXValue.text = transformedRightPosition.x.ToString();
            rightPosYValue.text = transformedRightPosition.y.ToString();
            rightPosZValue.text = transformedRightPosition.z.ToString();

            Vector3 velocity = _controller.rightController.getVelocity();
            Vector3 transformedRightVelocity = velocity - headsetVelocity;
            rightVelXValue.text = transformedRightVelocity.x.ToString();
            rightVelYValue.text = transformedRightVelocity.y.ToString();
            rightVelZValue.text = transformedRightVelocity.z.ToString();

            Quaternion quaternion = _controller.rightController.getRotation();
            Quaternion transformedRightRotation = Quaternion.Inverse(headsetRotation) * quaternion;
            rightQuaternionWValue.text = transformedRightRotation.w.ToString();
            rightQuaternionXValue.text = transformedRightRotation.x.ToString();
            rightQuaternionYValue.text = transformedRightRotation.y.ToString();
            rightQuaternionZValue.text = transformedRightRotation.z.ToString();
        }
    }
}