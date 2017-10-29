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
            var explodePos = new Vector3(transform.position.x, transform.position.y, 0);
            GameManager.Instance.mapController.DestroyTile(explodePos);
            isDetonatingWithQuiteSomeBang = true;
            //TODO: takre care to not destroy gameObject before attached animatins are finished
            Destroy(gameObject);
        }
    }
}
