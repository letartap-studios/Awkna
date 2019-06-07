using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Sprite openedDoor;
    public Sprite closedDoor;

    public void CloseDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = closedDoor;
        gameObject.GetComponent<Collider2D>().enabled = true;

        GameObject light = gameObject.transform.GetChild(0).gameObject;
        light.SetActive(true);
    }

    public void OpenDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = openedDoor;
        gameObject.GetComponent<Collider2D>().enabled = false;
        //light
        GameObject go = gameObject.transform.GetChild(0).gameObject;
        go.SetActive(false);
    }
}
