using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public Sprite openedDoor;

    void close()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = openedDoor;

        //light
        GameObject go = this.gameObject.transform.GetChild(0).gameObject;
        go.SetActive(false);
    }
}
