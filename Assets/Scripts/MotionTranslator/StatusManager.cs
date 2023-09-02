using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatusManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty recordActions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(recordActions.action.WasPressedThisFrame()){
            
        }
    }
}
