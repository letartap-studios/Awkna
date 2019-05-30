using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public Sprite openedDoor;

    public void OpenDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = openedDoor;
        gameObject.GetComponent<Collider2D>().enabled = false;
        //light
        GameObject go = gameObject.transform.GetChild(0).gameObject;
        go.SetActive(false);
    }
}
