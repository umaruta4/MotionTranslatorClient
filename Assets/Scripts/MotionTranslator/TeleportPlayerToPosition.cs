using MotionTranslator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportPlayerToPosition : MonoBehaviour
{
    public Button teleportButton;
    public Transform objectToTeleport;
    public XRBaseController teleportController; // The VR controller used for teleportation
    public GameObject playerHead; // The GameObject representing the player's head or camera

    void Start()
    {
        if (teleportButton == null)
        {
            teleportButton = GetComponent<Button>();
        }

        teleportController = FindObjectOfType<XRBaseController>();
        playerHead = GameObject.Find("Main Camera");

        initializeListener();
    }

    void initializeListener()
    {
        teleportButton.onClick.AddListener(HandleTeleportButton);
    }

    void HandleTeleportButton()
    {   
        // Teleport the player to the destination
        Vector3 destinationPosition = objectToTeleport.position;
        playerHead.transform.position = destinationPosition;

        // Calculate the rotation to make the player look at the destination
        Vector3 lookDirection = objectToTeleport.forward; // Use the forward direction of the destination object
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        playerHead.transform.rotation = lookRotation;
    }
}
