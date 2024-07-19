using UnityEngine;

public class RockLifeCycle : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject,10f);
    }
}
