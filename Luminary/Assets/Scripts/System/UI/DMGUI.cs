using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DMGUI : MonoBehaviour
{
    public TMP_Text text;
    public Vector3 dir;
    public float x;
    public Vector3 pos;

    float genTime;
    
    // set direction bouncing
    void Start()
    {
        dir = new Vector2(GameManager.Random.getGeneralNext(-5, 5), 10);
        x = dir.x;
        genTime = Time.time;
    }

    // Set transforms
    public void Set()
    {
        Func.SetRectTransform(gameObject);
        GetComponent<RectTransform>().localPosition = GameManager.cameraManager.camera.WorldToScreenPoint(pos) - new Vector3(GameManager.cameraManager.camera.pixelWidth/2, GameManager.cameraManager.camera.pixelHeight/2);
    }

    // Update is called once per frame
    void Update()
    {
        // position reset 
        Func.SetRectTransform(gameObject, GameManager.cameraManager.camera.WorldToScreenPoint(pos) - new Vector3(GameManager.cameraManager.camera.pixelWidth / 2, GameManager.cameraManager.camera.pixelHeight / 2) + dir);


       
        dir += new Vector3(x, - (Time.time - genTime - 0.3f) * (Time.time-genTime - 0.3f) * 40 + 4);

        // after 1 second, destory it
        if(Time.time  - genTime > 1f)
        {
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
