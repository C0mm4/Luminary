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
    public static SceneTransition sceneTransition;
    public static CameraManager cameraManager;
    public static PlayerDataManager playerDataManager;
    public static GameObject player;
    public enum GameState { Loading, InPlay, Pause };

    public static GameState gameState;



    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return gm_Instance._resource; } }

    MapGen _mapGen = new MapGen();
    public static MapGen MapGen { get { return gm_Instance._mapGen; } }

    RandomEncounter _randomEncounter = new RandomEncounter();
    public static RandomEncounter Random { get { return gm_Instance._randomEncounter; } }

    StageController _stageController = new StageController();
    public static StageController StageC { get { return gm_Instance._stageController; } }

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

    
    [SerializeField]
    public Canvas canvas;

    public UIManager uiManager;


    private void Awake()
    {
        DontDestroyOnLoad(persistentObject);
        gm_Instance = this;
        sceneTransition = GameObject.Find("GameManager").GetComponent<SceneTransition>();
        cameraManager = GameObject.Find("GameManager").GetComponent<CameraManager>();
        playerDataManager = GameObject.Find("GameManager").GetComponent<PlayerDataManager>();
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


        if(canvas == null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            if(canvas.gameObject.GetComponent<UIManager>() == null)
            {
                canvas.gameObject.AddComponent<UIManager>();
            }

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            canvas.planeDistance = 10;
        }
        init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            if (canvas.gameObject.GetComponent<UIManager>() == null)
            {
                canvas.gameObject.AddComponent<UIManager>();
            }

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            canvas.planeDistance = 10;
        }
    }

    public void init()
    {

        // Spell 객체를 로드하고 만드는 객체 초기화
        Spells.init();
        
        // Spell 슬롯 관리 객체 초기화 = Spell 객체 로드 먼저 해야함.
        Random.init("");
        MapGen.init();
        StageC.init();
        // 다른 객체 영향을 받기에 항상 마지막에 실행해야 함.
        uiManager.init();

    }

    public void loadData()
    {
        playerDataManager.loadKeySetting();

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

    public void sceneControl(string targetScene)
    {
        gameState = GameState.Loading;
        Debug.Log("Transition Start to " + targetScene);
        sceneTransition.sceneLoad(targetScene);
    }

    public void transitionInit(string targetScene)
    {
        switch (targetScene)
        {
            case "LobbyScene":
                lobbySceneInit();
                break;
            default:
                break;
        }
    }
    public void lobbySceneInit()
    {
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        SpriteRenderer lobbyField = GameObject.Find("LobbyField").GetComponent<SpriteRenderer>();
        cameraManager.camera = mainCamera;
        cameraManager.background = lobbyField;
        playerGen();
        sceneInit();
        gameState = GameState.InPlay;
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

   
    public static void mapgen()
    {
        clear();
        StageC.gameStart();
    }

    public static void clear()
    {
        StageC.clear();
        MapGen.clear();
    }

    public void playerGen()
    {
        player = Resource.Instantiate("PlayerbleChara");
        player.transform.position = new Vector3(0, 0, -1);
        player.name = "PlayerbleChara";
        cameraManager.setCamera(player.transform);
        PlayerDataManager.interactionDistance = 1000.0f;
        Resource.Instantiate("Mobs/TestMob");
    }

    public void sceneInit()
    {
        uiManager.init();
    }
}
