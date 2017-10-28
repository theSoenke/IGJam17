using UnityEngine;

public class PumpkinBomb : MonoBehaviour
{
    public float countdown = 5;

    private float elapsed;

    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= countdown)
        {
            // TODO BOOM
        }
    }
}
