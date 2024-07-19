using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Transform cameraTransform; // VR ī�޶��� Transform
    public float distanceFromCamera; // UI �гΰ� ī�޶� ������ �Ÿ�
    public Vector3 offset = new Vector3(0, 0, 0); // UI �г��� ������
    private readonly SlowMotionCamera motionCamera;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (cameraTransform != null)
        {
            // ī�޶��� ���� ���� �Ÿ��� UI �г� �̵�
            Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera + offset;
            transform.position = targetPosition;

            // UI �г��� �׻� ī�޶� �ٶ󺸵��� ȸ��
            transform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
        }
    }
}
