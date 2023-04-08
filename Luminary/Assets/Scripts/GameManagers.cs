using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    // ���ӿ� ���� �̱��� ������Ʈ ����


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


    // �������� �Ǿ��ִ� ������ �ν��Ͻ��� ��������.
    static void init()
    {
        // �ν��Ͻ��� ����ִٸ�
        if (s_Instance == null)
        {
            // ���ӿ�����Ʈ�� �̸��� ���ؼ� ����
            GameObject go = GameObject.Find("@GameManagers");
            // go ���
            if (go == null)
            {
                Debug.Log("Game Managers doesn't exists.");
                // �̱��� -> �ϳ��� Manager���� ������ �ʹ� -> �ڱ��ڽ��� �ν��ϵ忡 �Ҵ�
                go = new GameObject { name = "@GameManagers" };
                go.AddComponent<GameManagers>();

            }
            // �ظ��Ͽ�����(������ ���̵��ÿ���) �������� ����
            DontDestroyOnLoad(go);
            
            // Intance�� ������ Manager Script ����.
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