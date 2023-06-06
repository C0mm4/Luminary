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
    public static UIState uiState;

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

    FSMManager _fsmManager = new FSMManager();
    public static FSMManager FSM { get { return gm_Instance._fsmManager; } }

    public struct SerializedGameData
    {
        public List<Resolution> resolutionList;
        // �ػ� ���
        public Resolution resolution;
        // ���� �ػ�
        public bool isFullscreen;
        public float gameVolume;
        // ��ü ����
        public float musicVolume;
        // ������� ����
        public float effectVolume;
        // ȿ���� ����
    }

    public static SerializedGameData gameData;
    public GameObject persistentObject;
    // == this

    public AudioSource audioSourceBGM;
    private bool isPaused = false;
    // �ý��� ����

    
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

        Application.targetFrameRate = 30;
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
        Debug.Log("GameManager Awake Init");
        // Spell ��ü�� �ε��ϰ� ����� ��ü �ʱ�ȭ
        Spells.init();
        FSM.init();
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
        SceneChangeAction?.Invoke();
        
        sceneTransition.sceneLoad(targetScene);
    }

    public void transitionInit(string targetScene)
    {
        sceneSetClear();
        setCanvas();
        Debug.Log("Scene Load in GameManager : " + targetScene);
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
        uiManager.ChangeState(UIState.Lobby);
        gameState = GameState.InPlay;
    }
    public void stageSceneInit()
    {
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        cameraManager.camera = mainCamera;
        if(gameState == GameState.Loading)
        {
            mapgen();
            playerGen();
        }
        gameState = GameState.InPlay;
        uiManager.ChangeState(UIState.InPlay);
    }

    public void gameStart()
    {
        mapgen();
        playerGen();
        StageC.moveRoom(0);
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
                GameObject go = Resource.Instantiate("UI/Pause", canvas.transform);
                go.name = "pause";
            }
            else
            {
                GameObject go = GameObject.Find("pause");
                Resource.Destroy(go);
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
        player.transform.position = new Vector3(0, 0, 1);
        player.name = "PlayerbleChara";
        cameraManager.setCamera(player.transform);
        PlayerDataManager.interactionDistance = 1000.0f;
    }


    public void setCanvas()
    {
        Debug.Log("Set Canvas");
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
