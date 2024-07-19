using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Transform cameraTransform; // VR 카메라의 Transform
    public float distanceFromCamera; // UI 패널과 카메라 사이의 거리
    public Vector3 offset = new Vector3(0, 0, 0); // UI 패널의 오프셋
    private readonly SlowMotionCamera motionCamera;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (cameraTransform != null)
        {
            // 카메라의 앞쪽 일정 거리로 UI 패널 이동
            Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera + offset;
            transform.position = targetPosition;

            // UI 패널이 항상 카메라를 바라보도록 회전
            transform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
        }
    }
}
