using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;


namespace MotionTranslator {
    public class JsonSender : MonoBehaviour
    {
        private readonly HttpClient _httpClient;

        public JsonSender()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
        }

        public HttpResponseMessage SendJson(string url, object jsonObject)
        {
            // Serialize the JSON object to a JSON string
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);

            // Send a POST request with the JSON data
            var response = _httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;

            return response;
        }
    }
}
