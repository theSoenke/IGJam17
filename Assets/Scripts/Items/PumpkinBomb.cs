using UnityEngine;

public class PumpkinBomb : MonoBehaviour
{
    public float countdown = 5;

    private float elapsed;

    private bool isDetonatingWithQuiteSomeBang = false;

    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= countdown &! isDetonatingWithQuiteSomeBang)
        {
            GameManager.Instance.mapController.DestroyTile(transform.position);
            isDetonatingWithQuiteSomeBang = true;
            //TODO: takre care to not destroy gameObject before attached animatins are finished
            Destroy(gameObject);
        }
    }
}
