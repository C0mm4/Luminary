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
    [SerializeField]
    public static PlayerDataManager playerDataManager;
    public static GameObject player;
    public static InputManager inputManager;

    public static GameState gameState;
    [SerializeField]
    public UIState uiState;

    public Action SceneChangeAction;

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
    [SerializeField]
    public UIManager uiManager;



    private void Awake()
    {
        DontDestroyOnLoad(persistentObject);
        gm_Instance = this;
        sceneTransition = GameObject.Find("GameManager").GetComponent<SceneTransition>();
        cameraManager = GameObject.Find("GameManager").GetComponent<CameraManager>();
        playerDataManager = GameObject.Find("GameManager").GetComponent<PlayerDataManager>();
        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
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

        SceneChangeAction += GameObjectReSet;
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
            uiManager = canvas.GetComponent<UIManager>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            canvas.planeDistance = 10;
        }

    }

    public void GameObjectReSet()
    {
        Resource.Destroy(GameObject.FindGameObjectsWithTag("Player"));
        Resource.Destroy(GameObject.FindGameObjectsWithTag("Item"));
        Resource.Destroy(GameObject.FindGameObjectsWithTag("Mob"));
        Resource.Destroy(GameObject.FindGameObjectsWithTag("SpellEffect"));
    }

    public void init()
    {

        // Spell 객체를 로드하고 만드는 객체 초기화
        Spells.init();
        Random.init("");
        MapGen.init();
        StageC.init();
        playerDataManager.playerDataInit();

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
        SceneChangeAction?.Invoke();
        
        sceneTransition.sceneLoad(targetScene);
    }

    public void transitionInit(string targetScene)
    {
        sceneSetClear();

        switch (targetScene)
        {
            case "LobbyScene":
                lobbySceneInit();
                break;
            case "StageScene":
                stageSceneInit();
                break;
            default:
                break;
        }
    }

    public void sceneSetClear()
    {
        PlayerDataManager.interactionObject = null;
        PlayerDataManager.interactionDistance = 5.5f;
    }


    public void lobbySceneInit()
    {
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        SpriteRenderer lobbyField = GameObject.Find("LobbyField").GetComponent<SpriteRenderer>();
        cameraManager.camera = mainCamera;
        cameraManager.background = lobbyField;
        playerGen();
        gameState = GameState.InPlay;
        uiManager.ChangeState(UIState.InPlay);
    }
    public void stageSceneInit()
    {
        mapgen();
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

    public void interaction()
    {
        switch (PlayerDataManager.interactionObject)
        {
            case "InitPlayObject":
                Debug.Log("InteractionObject is InitPlayObject");
                sceneControl("StageScene");
                break;
            default:
                Debug.Log("InteractionObject is null");
                break;
        }
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
        Debug.Log("Trigger");
        Resource.Instantiate("Mobs/TestMob");
    }
    public void sceneinit()
    {
//        uiManager.init();
    }
}
