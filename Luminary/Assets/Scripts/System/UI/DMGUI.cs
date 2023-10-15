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
    
    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector2(GameManager.Random.getGeneralNext(-5, 5), 10);
        x = dir.x;
        genTime = Time.time;
    }

    public void Set()
    {
        Debug.Log(GameManager.inputManager.mousePos);
        Debug.Log(GameManager.cameraManager.camera.WorldToScreenPoint(transform.position));
        Func.SetRectTransform(gameObject);
        GetComponent<RectTransform>().localPosition = GameManager.cameraManager.camera.WorldToScreenPoint(pos) - new Vector3(GameManager.cameraManager.camera.pixelWidth/2, GameManager.cameraManager.camera.pixelHeight/2);
        Debug.Log(GetComponent<RectTransform>().localPosition);
        
    }

    // Update is called once per frame
    void Update()
    {

        Func.SetRectTransform(gameObject, GameManager.cameraManager.camera.WorldToScreenPoint(pos) - new Vector3(GameManager.cameraManager.camera.pixelWidth / 2, GameManager.cameraManager.camera.pixelHeight / 2) + dir);



        dir += new Vector3(x, - (Time.time - genTime - 0.3f) * (Time.time-genTime - 0.3f) * 40 + 4);

        if(Time.time  - genTime > 1f)
        {
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
