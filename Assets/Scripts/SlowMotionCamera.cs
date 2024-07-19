using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SlowMotionCamera : MonoBehaviour
{
    public float slowMotionFactor = 0.5f;
    public float slowMotionDuration = 1f;
    public bool isActive = false;
    //슬로우 모션중 일시정지를 했을 때 남은 시간을 체크하기 위함
    public float slowMotionLastTime;

    CinemachineBlendListCamera blendListCamera;
    GameObject virtualCamObj1;
    GameObject virtualCamObj2;
    CinemachineVirtualCamera virtualCamera1;
    CinemachineVirtualCamera virtualCamera2;
    public GameObject _leftHand;
    public GameObject _rightHand;
    [SerializeField] private GameObject SliderUI;
    [SerializeField] private GameObject Collider;
    public UIFollowCamera UIFollowCamera;
    public ComboCount _comboCount;
    public RedRockCheck _redRockCheck;
    [SerializeField] private DamageController DamageController;
    [SerializeField] private AudioSource audioSource;
    public CinemachineDollyCart dollyCart;
    public CinemachineSmoothPath dollyPath;
    public Transform dollyTrack;
    public Rolling rolling;
    public int totalWaypoints = 5;
    private int currentWaypoint = 0;
    private float pathPositionIncrement;
    private CinemachineTrackedDolly trackedDolly;
    [SerializeField] private CinemachineVirtualCamera back;
    private void Awake()
    {
        dollyCart.m_Position = 0;
        slowMotionDuration = 1f;
        slowMotionFactor = 0.5f;
        blendListCamera = GetComponent<CinemachineBlendListCamera>();
        blendListCamera.m_Loop = false;
        

        virtualCamObj2 = GameObject.Find("Front");
        virtualCamObj1 = GameObject.Find("Back");
        if (virtualCamObj1 != null && virtualCamObj2 != null)
        {
            virtualCamera1 = virtualCamObj1.GetComponent<CinemachineVirtualCamera>();
            virtualCamera2 = virtualCamObj2.GetComponent<CinemachineVirtualCamera>();
        }
        //_leftHand = GameObject.Find("RightHandAnchor");
        //_rightHand = GameObject.Find("LeftHandAnchor");
    }
    private void Start()
    {
        trackedDolly = back.GetCinemachineComponent<Cinemachine.CinemachineTrackedDolly>();
        trackedDolly.m_PathPosition = 0;

        pathPositionIncrement = 1f;
    }
    private void Update()
    {
        Vector2 leftStickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (DamageController.Stamina >= 20)
        {
            if (leftStickInput.x < -0.5f && !rolling._canMove)
            {
                Debug.Log("왼쪽 이동");
               // if(currentWaypoint !=4 && isTrack)
                {
                    Move(-2);
                }
                
                //else if(currentWaypoint != 0 && !isTrack)
                //{
                //   Move(-2);
                //}
                //else if (currentWaypoint == 0 && !isTrack)
                //{
                //    isTrack = true;
                //    dollyTrack.Rotate(0, 180, 0);
                //    currentWaypoint = 4;
                //    Move(-2);

                //}
                //else if (currentWaypoint == 0 && isTrack)
                //{
                //    isTrack = false;
                //    dollyTrack.Rotate(0, 180, 0);
                //    currentWaypoint = 4;
                //    Move(-2);
                //}
               
                StartCoroutine(SlowMotionControl());
            }
            else if (leftStickInput.x > 0.5f && !rolling._canMove)
            {
                Debug.Log("오른쪽 이동");
                //if(currentWaypoint != 4&& !isTrack)
                {
                    Move(2);
                }

               

                //else if (currentWaypoint != 4 && isTrack)
                //{
                //    Move(2);
                //}

                //else if (currentWaypoint == 4 && isTrack)
                //{
                //    isTrack = false;
                //    dollyTrack.Rotate(0, 180, 0);
                //    currentWaypoint = 0;
                //   Move(2);

                //}
                //else if(currentWaypoint == 4 && !isTrack)
                //{
                //    isTrack = true;
                //    dollyTrack.Rotate(0, 180, 0);
                //    currentWaypoint= 0;
                //    Move(2);
                //}
                StartCoroutine(SlowMotionControl());
            }
        }

    }
    //만약 현재위치가 0일떄 왼쪽으로 가면, 트랙을 뒤집고, 오른쪽으로 가면 냅둔다. 처음에 오른쪽
    //갈떄랑 왼쪽으로 간다음에 오른쪽 갈떄를 구분짓는 변수 만들어서.(트랙이 뒤집힘?)
    //
    // 0 1 
    public bool isTrack = false;
    void Move(int steps)
    {
        float targetPosition = trackedDolly.m_PathPosition + steps * pathPositionIncrement;

        // 최단 경로로 이동하도록 steps 값을 조정
        StartCoroutine(SmoothMove(targetPosition));
    }
    IEnumerator SmoothMove(float targetPosition)
    {
       
        float startPosition = trackedDolly.m_PathPosition;
        float elapsedTime = 0f;
        float duration = slowMotionDuration+1f; // 이동에 걸리는 시간

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;


            trackedDolly.m_PathPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / duration);
            
            yield return null;
        }

        trackedDolly.m_PathPosition = targetPosition;
    }

    private void BackViewCamera()
    {
        if (UIManager.Instance.isUIImageOn)
        {
            return;
        }
        isActive = true;
        SliderUI.SetActive(false);
        Collider.SetActive(false);
        virtualCamObj1.transform.SetParent(this.transform);
        virtualCamObj2.transform.SetParent(this.transform);
        _leftHand.SetActive(false);
        _rightHand.SetActive(false);

        if (!_redRockCheck.DontResetCombo)
        {
            _comboCount.ComboCountReset();
        }
        if (blendListCamera.m_Instructions.Length > 1)
        {
            blendListCamera.m_Instructions[0].m_VirtualCamera = virtualCamera2;
            blendListCamera.m_Instructions[1].m_VirtualCamera = virtualCamera1;

            blendListCamera.m_Instructions[1].m_Blend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
            blendListCamera.m_Instructions[1].m_Blend.m_Time = 0.5f;
            blendListCamera.m_Instructions[1].m_Hold = 1.0f;
        }
        //UIFollowCamera.cameraTransform = virtualCamObj1.transform;
    }

    private void SlowMotionCameraMove()
    {
        if (UIManager.Instance.isUIImageOn)
        {
            return;
        }
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        audioSource.pitch = slowMotionFactor;
    }

    private void NormalMotion()
    {
        if (UIManager.Instance.isUIImageOn)
        {
            return;
        }
        Debug.Log("NormalMotion");
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        audioSource.pitch = 1.0f;
        
       // UIFollowCamera.cameraTransform = Camera.main.transform;
    }

    private void FirstPersonCamera()
    {
        if (UIManager.Instance.isUIImageOn)
        {
            return;
        }
        virtualCamObj1.transform.SetParent(this.transform);
        virtualCamObj2.transform.SetParent(this.transform);
        _leftHand.SetActive(true);
        _rightHand.SetActive(true);

        if (blendListCamera.m_Instructions.Length > 1)
        {
            blendListCamera.m_Instructions[0].m_VirtualCamera = virtualCamera1;
            blendListCamera.m_Instructions[1].m_VirtualCamera = virtualCamera2;

            blendListCamera.m_Instructions[1].m_Blend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
            blendListCamera.m_Instructions[1].m_Blend.m_Time = 0.5f;
            blendListCamera.m_Instructions[1].m_Hold = 0.5f;
        }
        Debug.Log("FirstPersonCamera");
    }

    IEnumerator SlowMotionControl()
    {
        
        BackViewCamera();
        SlowMotionCameraMove();
        yield return new WaitForSeconds(slowMotionDuration);
        FirstPersonCamera();
        NormalMotion();
        yield return new WaitForSeconds(slowMotionDuration);
        SliderUI.SetActive(true);
        Collider.SetActive(true);
        isActive = false;
    }
}
