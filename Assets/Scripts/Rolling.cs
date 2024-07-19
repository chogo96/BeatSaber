using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Rolling : MonoBehaviour
{
    [SerializeField]private Transform[] _transform;
    public bool _canMove = false;
    public bool _moveLeft = false;
    public bool _moveRight = false;
    [SerializeField]private float _speed = 2;
    public int _currentIndex = 0;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _spawner;
    [SerializeField] private Transform _colider;
    [SerializeField] private Transform _redColider;
    [SerializeField] private DamageController _damageController;

    public bool done = false;
    private void Awake()
    {
        transform.position = _transform[_currentIndex].position;
        
    }


    private void Update()
    {
        EvadeMotion();
    }
    private void EvadeMotion()
    {
        Vector2 leftStickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        //������ üũ
        if (UIManager.Instance.isUIImageOn)
        {
            return;
        }
        //�������� ��ƽ�� ��￴�� ��
        if (_damageController.Stamina >= 20)
        {


            if (leftStickInput.x < -0.5f && !_canMove && !UIManager.Instance.isUIImageOn) // ���� �̵�
            {
                {
                    
                    _moveLeft = true;
                    _spawner.transform.Rotate(0, 90, 0);
                    _colider.transform.Rotate(0, 90, 0);
                    _redColider.transform.Rotate(0, 90, 0);
                    _damageController.Stamina -= 20;
                    _damageController._staminaSlider.value = _damageController.Stamina / _damageController.StaminaMax;

                    if (_currentIndex == 0)
                    {
                        _currentIndex = _transform.Length - 1; // 0���� �������� �̵� �� ������ �ε����� �̵�
                    }
                    else
                    {
                        _currentIndex--;

                    }

                    _canMove = true;
                }
            }
            else if (leftStickInput.x > 0.5f && !_canMove && !UIManager.Instance.isUIImageOn) // ������ �̵�
            {
                
                _moveRight = true;
                _spawner.transform.Rotate(0, -90, 0);
                _colider.transform.Rotate(0, -90, 0);
                _redColider.transform.Rotate(0, -90, 0);
                _damageController.Stamina -= 20;
                _damageController._staminaSlider.value = _damageController.Stamina / _damageController.StaminaMax;
                if (_currentIndex == _transform.Length - 1)
                {
                    _currentIndex = 0; // ������ �ε������� ���������� �̵� �� ù ��° �ε����� �̵�
                }
                else
                {
                    _currentIndex++;

                }
                _canMove = true;
            }
        }
        if (_canMove && !UIManager.Instance.isUIImageOn)
        {
            done = true;
            transform.position = Vector3.MoveTowards(transform.position, _transform[_currentIndex].position, _speed * Time.deltaTime);
            transform.LookAt(_transform[_currentIndex].position);
            if (transform.position == _transform[_currentIndex].position)
            {
                done = false;
                _canMove = false;
                _moveRight = false;
                _moveLeft = false;
            }
        }
    }
}