using System;
using System.Collections;
using System.Net.Http;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using Newtonsoft.Json;

namespace MotionTranslator {

    [RequireComponent(typeof(VRInput))]
    public class RecordingStatus : MonoBehaviour
    {
        
        public TMP_InputField inputField;
        public TextMeshProUGUI content;
        public TextMeshProUGUI statusText;

        private List<List<float>> _leftControllerRecordData;
        private List<List<float>> _rightControllerRecordData;
        private VRInput _controller;
        private bool _recording;
        private bool _prevButtonState = false;

        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<VRInput>();
            _recording = false;
            _leftControllerRecordData = new List<List<float>>();
            _rightControllerRecordData = new List<List<float>>();

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
            }else{
                if(_leftControllerRecordData.Count > 0 || _rightControllerRecordData.Count > 0){
                    ShowSendingRecordMessage();
                    SendControllerRecordDataThroughNetwork();
                    _leftControllerRecordData.Clear();
                    _rightControllerRecordData.Clear();
                }
                ShowNotRecordingMessage();
            }
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
            statusText.text = "Recording...";
            content.text = "Text : " + inputField.text;
        }

        void ShowSendingRecordMessage()
        {
            statusText.text = "Sending Record Data Through Network...";
        }

        void ShowNotRecordingMessage()
        {
            statusText.text = "Not Recording...";
            content.text = "Text : ";
        }

        void RecordControllerData()
        {
            Vector3 leftPosition = _controller.leftController.getPosition();
            Vector3 leftVelocity = _controller.leftController.getVelocity();
            Quaternion leftQuaternion = _controller.leftController.getRotation();

            _leftControllerRecordData.Add(new List<float> {
                _controller.leftController.getTriggerTouch() ? 1.0f : 0.0f,
                _controller.leftController.getTriggerButton() ? 1.0f : 0.0f,
                _controller.leftController.getGrip(),
                _controller.leftController.getThumbTouch() ? 1.0f : 0.0f,
                leftPosition.x,
                leftPosition.y,
                leftPosition.z,
                leftVelocity.x,
                leftVelocity.y,
                leftVelocity.z,
                leftQuaternion.w,
                leftQuaternion.x,
                leftQuaternion.y,
                leftQuaternion.z
            });

            Vector3 rightPosition = _controller.rightController.getPosition();
            Vector3 rightVelocity = _controller.rightController.getVelocity();
            Quaternion rightQuaternion = _controller.rightController.getRotation();

            _rightControllerRecordData.Add(new List<float> {
                _controller.rightController.getTriggerTouch() ? 1.0f : 0.0f,
                _controller.rightController.getTriggerButton() ? 1.0f : 0.0f,
                _controller.rightController.getGrip(),
                _controller.rightController.getThumbTouch() ? 1.0f : 0.0f,
                rightPosition.x,
                rightPosition.y,
                rightPosition.z,
                rightVelocity.x,
                rightVelocity.y,
                rightVelocity.z,
                rightQuaternion.w,
                rightQuaternion.x,
                rightQuaternion.y,
                rightQuaternion.z
            });
        }

        void SendControllerRecordDataThroughNetwork()
        {
            var jsonSender = new JsonSender();

            var jsonObject = new 
            {
                table = "tabel",
                classValue = inputField.text,
                text = inputField.text,
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
                    statusText.text = "Data has been sent successfully!";
                }else{
                    statusText.text = "Failed to send JSON data";
                }
            }catch(Exception ex){
                statusText.text = $"Error sending JSON data : {ex.Message}";
            }
        }
    }
}
