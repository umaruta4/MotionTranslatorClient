using System;
using System.Collections;
using System.Net.Http;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;

namespace MotionTranslator {

    [RequireComponent(typeof(VRInput))]
    public class RecordingStatus : MonoBehaviour
    {
        
        public TMP_InputField textField;
        public TMP_InputField labelField;
        public TextMeshProUGUI content;
        public TextMeshProUGUI recordingText;
        public TextMeshProUGUI statusText;
        public Button cancelButton;

        private List<List<float>> _leftControllerRecordData;
        private List<List<float>> _rightControllerRecordData;
        private VRInput _controller;
        private bool _recording;
        private bool _prevButtonState;
        private bool _cancelling;

        // Start is called before the first frame update
        void Start()
        {
            _recording = false;
            _cancelling = false;
            _prevButtonState = false;
            _leftControllerRecordData = new List<List<float>>();
            _rightControllerRecordData = new List<List<float>>();

            _controller = GetComponent<VRInput>();
            if (cancelButton == null)
            {
                cancelButton = GetComponent<Button>();
            }

            initializeListener();
        }

        // Update is called once per frame
        void Update()
        {
            if(_controller._rightController.isValid){
                CheckRecordingButton();
            }

            if(_recording){
                ShowRecordingMessage();
                RecordControllerData();
                ShowCancelButton();
            }else{
                if((_leftControllerRecordData.Count > 0 || _rightControllerRecordData.Count > 0) && !_cancelling){
                    SendControllerRecordDataThroughNetwork();
                    _leftControllerRecordData.Clear();
                    _rightControllerRecordData.Clear();
                }
                ShowNotRecordingMessage();
                HideCancelButton();
            }
        }

        void initializeListener()
        {
            cancelButton.onClick.AddListener(HandleCancleButtonClick);
        }

        void HandleCancleButtonClick()
        {
            _cancelling = true;
            _recording = false;
            _leftControllerRecordData.Clear();
            _rightControllerRecordData.Clear();
            statusText.text = "Record Cancelled.";
            _cancelling = false;
        }

        void ShowCancelButton()
        {
            cancelButton.gameObject.SetActive(true);
        }

        void HideCancelButton()
        {
            cancelButton.gameObject.SetActive(false);
        }

        void CheckRecordingButton()
        {
            bool currentButtonState = _controller.rightController.getSecondaryButton();
            if (currentButtonState && !_prevButtonState){
                // Flip switch
                _recording = !_recording;
            }

            _prevButtonState = currentButtonState;
        }

        void ShowRecordingMessage()
        {
            recordingText.text = "Recording...";
            content.text = "Text : " + textField.text;
        }

        void ShowNotRecordingMessage()
        {
            recordingText.text = "Not Recording...";
            content.text = "Text : ";
        }

        void RecordControllerData()
        {
            Controller vrHeadset = _controller.HMD;

            // Get the headset's position and rotation
            Vector3 headsetPosition = vrHeadset.getPosition();
            Quaternion headsetRotation = vrHeadset.getRotation();
            Vector3 headsetVelocity = vrHeadset.getVelocity();

            // Get the positions, velocities, and rotations of the left and right controllers
            Vector3 leftPosition = _controller.leftController.getPosition();
            Vector3 leftVelocity = _controller.leftController.getVelocity();
            Quaternion leftQuaternion = _controller.leftController.getRotation();

            Vector3 rightPosition = _controller.rightController.getPosition();
            Vector3 rightVelocity = _controller.rightController.getVelocity();
            Quaternion rightQuaternion = _controller.rightController.getRotation();

            // Transform controller data relative to headset
            Vector3 transformedLeftPosition = headsetRotation * (leftPosition - headsetPosition);
            Vector3 transformedRightPosition = headsetRotation * (rightPosition - headsetPosition);

            Quaternion transformedLeftRotation = Quaternion.Inverse(headsetRotation) * leftQuaternion;
            Quaternion transformedRightRotation = Quaternion.Inverse(headsetRotation) * rightQuaternion;

            Vector3 transformedLeftVelocity = leftVelocity - headsetVelocity;
            Vector3 transformedRightVelocity = rightVelocity - headsetVelocity;

            _leftControllerRecordData.Add(new List<float> {
                _controller.leftController.getTriggerTouch() ? 1.0f : 0.0f,
                _controller.leftController.getTriggerButton() ? 1.0f : 0.0f,
                _controller.leftController.getGrip(),
                _controller.leftController.getThumbTouch() ? 1.0f : 0.0f,
                transformedLeftPosition.x,
                transformedLeftPosition.y,
                transformedLeftPosition.z,
                transformedLeftVelocity.x,
                transformedLeftVelocity.y,
                transformedLeftVelocity.z,
                transformedLeftRotation.w,
                transformedLeftRotation.x,
                transformedLeftRotation.y,
                transformedLeftRotation.z
            });

            _rightControllerRecordData.Add(new List<float> {
                _controller.rightController.getTriggerTouch() ? 1.0f : 0.0f,
                _controller.rightController.getTriggerButton() ? 1.0f : 0.0f,
                _controller.rightController.getGrip(),
                _controller.rightController.getThumbTouch() ? 1.0f : 0.0f,
                transformedRightPosition.x,
                transformedRightPosition.y,
                transformedRightPosition.z,
                transformedRightVelocity.x,
                transformedRightVelocity.y,
                transformedRightVelocity.z,
                transformedRightRotation.w,
                transformedRightRotation.x,
                transformedRightRotation.y,
                transformedRightRotation.z
            });
        }

        void SendControllerRecordDataThroughNetwork()
        {
            statusText.text = "Sending Record Data Through Network...";
            
            var jsonSender = new JsonSender();

            var jsonObject = new 
            {
                table = "data_basic_asl",
                classValue = labelField.text,
                text = textField.text,
                data = new 
                {
                    _leftControllerRecordData,
                    _rightControllerRecordData
                }
            };


            string url = "http://127.0.0.1:5000/api/store";

            try{
                HttpResponseMessage response = jsonSender.SendJson(url, jsonObject);

                if(response == null){
                    statusText.text = "Failed to send JSON data";
                    return;
                }

                if(response.IsSuccessStatusCode){
                    statusText.text = "Record has been sent successfully!";
                }else{
                    statusText.text = "Failed to send JSON data";
                }
            }catch(Exception ex){
                statusText.text = $"Error sending JSON data : {ex.Message}";
            }
        }
    }
}
