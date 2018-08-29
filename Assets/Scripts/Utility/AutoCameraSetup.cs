using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AutoCameraSetup : MonoBehaviour
{
    public bool autoSetupCameraFollow = true;
    public string cameraFollowGameObjectName = "MainCat";
    CinemachineVirtualCamera cam;
    public Player playerRef;

    void Awake()
    {
        if (!autoSetupCameraFollow)
            return;

        cam = GetComponent<CinemachineVirtualCamera>();

        if (cam == null)
            throw new UnityException("Virtual Camera was not found, default follow cannot be assigned.");

        //we manually do a "find", because otherwise, GameObject.Find seem to return object from a "preview scene" breaking the camera as the object is not the right one
        var rootObj = gameObject.scene.GetRootGameObjects();
        GameObject cameraFollowGameObject = null;
        foreach (var go in rootObj)
        {
            if (go.name == cameraFollowGameObjectName)
                cameraFollowGameObject = go;
            else
            {
                var t = go.transform.Find(cameraFollowGameObjectName);
                if (t != null)
                    cameraFollowGameObject = t.gameObject;
            }

            if (cameraFollowGameObject != null) break;
        }

        if (cameraFollowGameObject == null)
            throw new UnityException("GameObject called " + cameraFollowGameObjectName + " was not found, default follow cannot be assigned.");

        if (cam.Follow == null)
        {
            cam.Follow = cameraFollowGameObject.transform;
        }

    }

    private void Start()
    {
        playerRef = GameObject.Find(cameraFollowGameObjectName).GetComponent<Player>();
    }

    private void Update()
    {
       
        // TODO: Increase camera look when leaping.

        // If we are leaping, increase the yandx offset. (set a new look target ahead in the direction of the mouse)
        if (playerRef.leaping == true)
        {
            

        }
        
    }
}