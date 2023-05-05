using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera camera;

    public Transform player;
    public SpriteRenderer background;

    float cameraHeight;
    float cameraWidth;


    void Start()
    {
        
    }
    void Update()
    {
        if(camera != null)
        {
            cameraHeight = camera.orthographicSize;
            cameraWidth = cameraHeight * camera.aspect;
        }
    }

    public void init()
    {
        
    }

    public void setCamera(Transform trf)
    {
        player = trf;
    }

    void LateUpdate()
    {
        if (player != null && background != null)
        {
            Vector3 targetPos = new Vector3(player.position.x, player.position.y, camera.transform.position.z);

            float minX = background.bounds.min.x + cameraWidth;
//            Debug.Log("minX" + minX);
            float maxX = background.bounds.max.x - cameraWidth;
//            Debug.Log("maxX" + maxX);
            float minY = background.bounds.min.y + cameraHeight;
//            Debug.Log("minY" + minY);
            float maxY = background.bounds.max.y - cameraHeight;
//            Debug.Log("maxY" + maxY);
            //Limit camera movement range

            targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
//            Debug.Log("targetPos.x : " + targetPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);
//            Debug.Log("targetPos.y : " + targetPos.y);
            camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, Time.deltaTime * 5f);
            // Relatively smooth tracking of playr positions
        }

    }
}