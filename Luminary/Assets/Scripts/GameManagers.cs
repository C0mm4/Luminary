using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    // 게임에 사용될 싱글톤 오브젝트 관리


    static GameManagers s_Instance;
    public static GameManagers Instance { get { init(); return s_Instance; } }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return s_Instance._resource; } }

    MapGen _mapGen = new MapGen();
    public static MapGen MapGen { get { return s_Instance._mapGen; } }

    RandomEncounter _randomEncounter = new RandomEncounter();
    public static RandomEncounter Random { get { return s_Instance._randomEncounter; } }

    StageController _stageController = new StageController();
    public static StageController StageC { get { return s_Instance._stageController; } }


    private void Start()
    {
        s_Instance = this;
        init();
    }


    // 전역으로 되어있는 유일한 인스턴스를 가져오자.
    static void init()
    {
        // 인스턴스가 비어있다면
        if (s_Instance == null)
        {
            // 게임오브젝트의 이름을 통해서 접근
            GameObject go = GameObject.Find("@GameManagers");
            // go 라는
            if (go == null)
            {
                Debug.Log("Game Managers doesn't exists.");
                // 싱글톤 -> 하나의 Manager만을 가지고 싶다 -> 자기자신을 인스턴드에 할당
                go = new GameObject { name = "@GameManagers" };
                go.AddComponent<GameManagers>();

            }
            // 왠만하여서는(심지어 씬이동시에도) 삭제되지 않음
            DontDestroyOnLoad(go);
            
            // Intance에 유일한 Manager Script 장착.
            s_Instance = go.GetComponent<GameManagers>();

        }
    }

    public static void mapgen()
    {
        clear();
        StageC.init();
    }
    
    public static void clear()
    {
        StageC.clear();
        MapGen.clear();
    }

}