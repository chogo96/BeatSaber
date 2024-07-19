using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationController : MonoBehaviour
{
    Animator _animator;
    public SlowMotionCamera slowMotionCamera;
    public Rolling rolling;
    bool _isPlaying = false;
    
    [SerializeField] private DamageController _damageController;
    // Start is called before the first frame update
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 leftStickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (_damageController.Stamina >= 20)
        {


            if (leftStickInput.x < -0.5f && !rolling._canMove && !_isPlaying && !UIManager.Instance.isUIImageOn) // ���� �̵�
            {
                _isPlaying = true;
                SkinnedMeshRenderer _playerBody = gameObject.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();
                _playerBody.enabled = true;
                StartCoroutine(DodgeMoves());
            }
            else if (leftStickInput.x > 0.5f && !rolling._canMove && !UIManager.Instance.isUIImageOn) // ������ �̵�
            {
                _isPlaying = true;

                SkinnedMeshRenderer _playerBody = gameObject.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();
                _playerBody.enabled = true;
                StartCoroutine(DodgeMoves());
            }
        }
    }

    IEnumerator DodgeMoves()
    {
        _animator.SetTrigger("Dodge");
        yield return new WaitForSeconds(slowMotionCamera.slowMotionDuration+0.5f);
        SkinnedMeshRenderer _playerBody = gameObject.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();
        _playerBody.enabled = false;
        _isPlaying = false;

    }
}
