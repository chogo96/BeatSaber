using Oculus.Platform;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DamageController: MonoBehaviour
{
    public float PlayerHp;
    public float PlayerHpMax;
    public float PlayerHpMin;
    public float EnemyHp;
    public float EnemyHpMax;
    public float EnemyHpMin;
    public float Stamina;
    public float StaminaMax;
    public float StaminaMin;

    [SerializeField] private Animator animator;
    private ComboCount _comboCount;
    [SerializeField] private Slider _enemyHPSlider;
    [SerializeField] private Slider _playerHPSlider;
    [SerializeField] private TMP_Text gameState;
    [SerializeField] private PlayableDirector PlayableDirector; 

    public Slider _staminaSlider;
    public void Awake()
    {
        PlayerHp = 100f;
        PlayerHpMax = PlayerHp;
        PlayerHpMin = 0f;

        Stamina = 100f;
        StaminaMax = 100f;
        StaminaMin = 0f;

        EnemyHp = 100f;
        EnemyHpMax = EnemyHp;  
        EnemyHpMin = 0f;
        gameState.enabled = false;
        
    }
    
    //��尡 �ı����� ���� ä�� �������ų�, ��ź�� ���ų�, �ڱ��忡 ������ ȣ���� �Լ�
    public void PlayerDamaged()
    {
        if(EnemyHp > 0)
        {
            PlayerHp -= 10f;

        }
        _comboCount.ComboCountReset();
        _comboCount.ComboCountUI();
        if ( PlayerHp <= 0)
        {
            PlayerHp = 0f;
            Debug.Log($"플레이어 사망");
            GameStateUI("Game\nOver");
            animator.SetTrigger("Victory");
        }
        _playerHPSlider.value = PlayerHp / PlayerHpMax;
    }
    //��带 �ı������� ȣ���� �Լ�
    public void EnemyDamaged()
    {
        if(PlayerHp > 0 )
        {
            EnemyHp -= 0.4f + _comboCount.comboCount * 0.002f;
        }
        
        _comboCount.ComboCountPlus();
        _comboCount.ComboCountUI();
        Stamina += 1f + _comboCount.comboCount * 0.001f; 
        if(Stamina >= 100)
        {
            Stamina = 100f;
        }

        if (EnemyHp <= 0)
        {
            EnemyHp = 0f;
            Debug.Log($"적 사망");
            GameStateUI("Game\nClear");
            animator.SetTrigger("Die");
        }
        _staminaSlider.value = Stamina / StaminaMax;
        _enemyHPSlider.value = EnemyHp / EnemyHpMax;
    }
    public void SetComboCount(ComboCount comboCount)
    {
        _comboCount = comboCount;
    }
    private void GameStateUI(string message)
    {
        gameState.enabled = true;
        gameState.text = message;
        PlayableDirector.Pause();

    }
    
}
