using UnityEngine;

public class LookAtController : MonoBehaviour
{
    public Transform objectTolookAt;
    public float headWeight;
    public float bodyWeight;
    private Animator animator;

    private void Start()
    {
            animator = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(objectTolookAt.position);
        animator.SetLookAtWeight(1, bodyWeight, headWeight);
    }
}
