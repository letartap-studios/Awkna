using UnityEngine;

public class GemTileHealth : MonoBehaviour
{

    public int health;
    public GameObject gem;

    public void DestroyGemTile()
    {
        Instantiate(gem, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //AstarPath.active.Scan();
    }
}
