using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class UIManager : SingletonOfT<UIManager>
{
   

    [SerializeField] private Button RestartButton;
    [SerializeField] private Button MainLobbyButton;
    [SerializeField] private GameObject UIImage;
    [SerializeField] private GameObject ReStartPart;
    [SerializeField] private GameObject MainLobbyPart;

    [SerializeField] AudioSource audioSource;
    public bool isUIImageOn;
    public SlowMotionCamera slowMotionCamera;
    public PlayableDirector timeLine;
    public Rolling rolling;
    [SerializeField] private GameObject VRRaycaster;
    private Canvas uiCanvas;
    private BoxCollider reStartPart;
    private BoxCollider mainLobbyPart;
    private void Awake()
    {
        isUIImageOn = false;
        slowMotionCamera = GetComponent<SlowMotionCamera>();
        uiCanvas = UIImage.GetComponent<Canvas>();
        reStartPart = ReStartPart.GetComponent<BoxCollider>();
        mainLobbyPart = MainLobbyPart.GetComponent<BoxCollider>();
        Init();
        VRRaycaster.SetActive(false);
        if (UIImage != null)
        {
            //UIImage.SetActive(false);
            uiCanvas.enabled = false;
            reStartPart.enabled = false;
            mainLobbyPart.enabled = false;
        }
        if (RestartButton != null)
        {
            RestartButton.onClick.AddListener(ClickRestart);
        }
        if (MainLobbyButton != null)
        {
            MainLobbyButton.onClick.AddListener(ClickMainLobby);
        }
        AssignUIElements();

    }

    private void Start()
    {
        //if (RestartButton != null)
        //{
        //    RestartButton.onClick.AddListener(ClickRestart);
        //}
        
        //if (MainLobbyButton != null)
        //{
        //    MainLobbyButton.onClick.AddListener(ClickMainLobby);
        //}
       
    }

    void Update()
    {
        if (!isUIImageOn && OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (UIImage != null )
            {
                Time.timeScale = 0f;
                //UIImage.SetActive(true);
                uiCanvas.enabled = true;
                reStartPart.enabled = true;
                mainLobbyPart.enabled = true;
                VRRaycaster.SetActive(true);
                audioSource.Pause();
                isUIImageOn = true;
            }
        }
        else if (isUIImageOn && OVRInput.GetDown(OVRInput.Button.Two))
        {
            //다시 인터페이스를 껐을때 시간 정상화(이거 근데 슬로우 모션 중에는 슬로우모션의 TimeScale을 따라야함)
            if (!slowMotionCamera.isActive && UIImage != null )
            {
                Time.timeScale = 1f;
                slowMotionCamera.slowMotionLastTime = slowMotionCamera.slowMotionDuration;

                //UIImage.SetActive(false);
                uiCanvas.enabled = false;
                reStartPart.enabled = false;
                mainLobbyPart.enabled = false;
                VRRaycaster.SetActive(false);
                audioSource.UnPause();
                isUIImageOn = false;

            }
            else if (slowMotionCamera.isActive && UIImage != null )
            {
                Time.timeScale = slowMotionCamera.slowMotionFactor;
                slowMotionCamera.slowMotionDuration = slowMotionCamera.slowMotionLastTime;
                //UIImage.SetActive(false);
                uiCanvas.enabled = false;
                reStartPart.enabled = false;
                mainLobbyPart.enabled = false;
                VRRaycaster.SetActive(false);
                audioSource.UnPause();
                isUIImageOn = false;
            }
        }
    }

    public void ClickRestart()
    {
        if (UIImage != null)
        {
            //UIImage.SetActive(false);
            uiCanvas.enabled = false;
            isUIImageOn = false;
            SceneManager.LoadScene(1);
            Time.timeScale = 1f;
        }
    }

    public void ClickMainLobby()
    {
        if (UIImage != null)
        {
            //UIImage.SetActive(false);
            uiCanvas.enabled = false;
            isUIImageOn = false;
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }
    }

    private void AssignUIElements()
    {
        
        if (RestartButton == null)
        {
            RestartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        }

        if (MainLobbyButton == null)
        {
            MainLobbyButton = GameObject.Find("MainLobbyButton").GetComponent<Button>();
        }

        if (UIImage == null)
        {
            UIImage = GameObject.Find("UIImage");
        }

       
        if (slowMotionCamera == null)
        {
            slowMotionCamera = FindFirstObjectByType<SlowMotionCamera>();
        }
    }
}
