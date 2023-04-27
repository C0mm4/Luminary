using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static GameManager gm_Instance;
    public static GameManager Instance
    {
        get
        {
            // if instance is NULL create instance
            if (!gm_Instance)
            {
                gm_Instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (gm_Instance == null)
                    Debug.Log("instance is NULL_GameManager");
            }
            return gm_Instance;
        }
    }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return gm_Instance._resource; } }

    MapGen _mapGen = new MapGen();
    public static MapGen MapGen { get { return gm_Instance._mapGen; } }

    RandomEncounter _randomEncounter = new RandomEncounter();
    public static RandomEncounter Random { get { return gm_Instance._randomEncounter; } }

    StageController _stageController = new StageController();
    public static StageController StageC { get { return gm_Instance._stageController; } }

    SkillSlotManager _skillSlotManager = new SkillSlotManager();
    public static SkillSlotManager SkillSlot { get { return gm_Instance._skillSlotManager; } }

    SpellManager _spellManager = new SpellManager();
    public static SpellManager Spells { get { return gm_Instance._spellManager; } }

    public struct SerializedGameData
    {
        public List<Resolution> resolutionList;
        // 해상도 목록
        public Resolution resolution;
        // 현재 해상도
        public bool isFullscreen;
        public float gameVolume;
        // 전체 볼륨
        public float musicVolume;
        // 배경음악 볼륨
        public float effectVolume;
        // 효과음 볼륨
    }

    public static SerializedGameData gameData;
    public GameObject persistentObject;
    // == this

    public AudioSource audioSourceBGM;
    private bool isPaused = false;
    // 시스템 변수

    CameraManager _camera;
    [SerializeField]
    public Canvas canvas;

    public GameObject _inventory;
    public UIManager uiManager;

    private void Awake()
    {
        DontDestroyOnLoad(persistentObject);
        gm_Instance = this;
        bool isFirstRun = PlayerPrefs.GetInt("isFirstRun", 1) == 1;
        if (isFirstRun)
        {
            // is first
        }
        else
        {
            // else
        }


        //reset game
        loadData();
        Screen.SetResolution(gameData.resolution.width, gameData.resolution.height, gameData.isFullscreen);


        init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void init()
    {
        _inventory = Resource.Instantiate("Inventory");

        Camera cmr  = GameObject.Find("Main Camera").GetComponents<Camera>()[0];
        _camera = cmr.GetComponent<CameraManager>();
        _camera.GetComponent<CameraManager>().init();

        // Spell 객체를 로드하고 만드는 객체 초기화
        Spells.init();
        
        // Spell 슬롯 관리 객체 초기화 = Spell 객체 로드 먼저 해야함.
        SkillSlot.init();
        Random.init("");
        MapGen.init();

        // 다른 객체 영향을 받기에 항상 마지막에 실행해야 함.
        uiManager.init();

    }

    public void loadData()
    {
        gameData.resolution.width = PlayerPrefs.GetInt("resolutionW", Screen.currentResolution.width);
        gameData.resolution.height = PlayerPrefs.GetInt("resolutionH", Screen.currentResolution.height);
        gameData.isFullscreen = PlayerPrefs.GetInt("isFullscreen", Screen.fullScreen ? 1 : 0) == 1;
        // load resolution

        gameData.gameVolume = PlayerPrefs.GetFloat("gameVolume", 0.3f);
        gameData.effectVolume = PlayerPrefs.GetFloat("effectVolume", 0.3f);
        gameData.musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.3f);
        // load volume
    }
    public void saveData()
    {

    }

    public void gameStart()
    {
        mapgen();
        playerGen();
    }

    public void gameOver()
    {
        // end
    }

    public void pauseGame()
    {
        {
            if (!isPaused)
            {
                Time.timeScale = 0f; // Pause Game
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1f; // Resume Game
                isPaused = false;
            }
        }

        /*if (!isPaused)
        {
            // Pause Game
            Time.timeScale = 0f;
            isPaused = true;

            // Pause all audio sources except BGM
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audioSource in allAudioSources)
            {
                if (audioSource != audioSourceBGM)
                {
                    audioSource.Pause();
                }
            }
        }
        else
        {
            // Resume Game
            Time.timeScale = 1f;
            isPaused = false;

            // Resume all audio sources
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audioSource in allAudioSources)
            {
                audioSource.UnPause();
            }
        }*/
    }

    public void playerDie()
    {

    }

    public void gameEnd()
    {

    }

    public void stageEnd()
    {

    }

    public Type GetTypeFromAssemblies(string TypeName)
    {
        // null 반환 없이 Type이 얻어진다면 얻어진 그대로 반환.
        var type = Type.GetType(TypeName);
        if (type != null)
            return type;

        // 프로젝트에 분명히 포함된 클래스임에도 불구하고 Type이 찾아지지 않는다면,
        // 실행중인 어셈블리를 모두 탐색 하면서 그 안에 찾고자 하는 Type이 있는지 검사.
        var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
        foreach (var assemblyName in referencedAssemblies)
        {
            var assembly = System.Reflection.Assembly.Load(assemblyName);
            if (assembly != null)
            {
                // 찾았다 요놈!!!
                type = assembly.GetType(TypeName);
                if (type != null)
                    return type;
            }
        }

        // 못 찾았음;;; 클래스 이름이 틀렸던가, 아니면 알 수 없는 문제 때문이겠지...
        return null;
    }

    //testcase
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

    public void playerGen()
    {
        GameObject player = Resource.Instantiate("PlayerbleChara");
        player.transform.position = new Vector3(0, 0, 0);
        player.name = "PlayerbleChara";
        _camera.setCamera(player.transform);
    }
}
