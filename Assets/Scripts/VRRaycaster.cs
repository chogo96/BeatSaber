using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRRaycaster : MonoBehaviour
{
    public Transform rightHandAnchor; // RightHandAnchor를 연결하세요.
    public OVRInput.Button interactionButton = OVRInput.Button.SecondaryIndexTrigger; // 인덱스 트리거를 클릭 버튼으로 설정
    public LayerMask uiLayerMask; // UI 레이어 마스크

    private LineRenderer lineRenderer;
    private RaycastHit hit;
    private void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;

        // Ensure uiLayerMask is set to the correct layer
        uiLayerMask = LayerMask.GetMask("UI");

    }
    void Start()
    {
        //lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.startWidth = 0.01f;
        //lineRenderer.endWidth = 0.01f;

        //// Ensure uiLayerMask is set to the correct layer
        //uiLayerMask = LayerMask.GetMask("UI");
       
    }

    void Update()
    {
        //// 레이캐스트 초기화
        //lineRenderer.SetPosition(0, rightHandAnchor.position);
        //Ray ray = new Ray(rightHandAnchor.position, rightHandAnchor.forward);
        //lineRenderer.SetPosition(1, ray.GetPoint(10));

        //Debug.Log("Ray direction: " + ray.direction);
        //Debug.Log("uiLayerMask: " + uiLayerMask.value);

        //if (Physics.Raycast(ray, out hit, 10, uiLayerMask))
        //{
        //    lineRenderer.SetPosition(1, hit.point);
        //    Debug.Log(hit.point); 
        //    if (OVRInput.GetDown(interactionButton))
        //    {
        //        Debug.Log("Hit");
        //        var button = hit.transform.GetComponent<Button>();
        //        if (button != null)
        //        {
        //            button.onClick.Invoke();
        //            Debug.Log("ButtonHit!");
        //        }
        //    }
        //}
        lineRenderer.SetPosition(0, rightHandAnchor.position);
        Ray ray = new Ray(rightHandAnchor.position, rightHandAnchor.forward);
        lineRenderer.SetPosition(1, ray.GetPoint(10));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, uiLayerMask))
        {
            lineRenderer.SetPosition(1, hit.point);
            Debug.Log("Hit point: " + hit.point);
            if (OVRInput.GetDown(interactionButton))
            {
                Debug.Log("Hit");
                var button = hit.transform.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke();
                    Debug.Log("ButtonHit!");
                }
                else
                {
                    Debug.Log("No Button component found on hit object");
                }
            }
        }
        else
        {
            Debug.Log("No hit detected");
        }
    }
}
