using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPatrollingEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private bool movingUp = true;
    public Transform groundDetection;

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);


        RaycastHit2D wallInfoUp = Physics2D.Raycast(groundDetection.position, Vector2.up, 0.2f,LayerMask.GetMask("Ground"));
        Debug.DrawRay(groundDetection.position, Vector2.up);
        RaycastHit2D wallInfoDown = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));
        //Debug.DrawRay(groundDetection.position, Vector2.down);




        if (movingUp == true && wallInfoUp.collider == true
            && wallInfoUp.collider.gameObject.tag != "Enemy"
            /*&& wallInfoR.collider.IsTouchingLayers(LayerMask.GetMask("Platform"))*/)
        {
            if (movingUp == true)
            {
                transform.eulerAngles = new Vector3(-180, 0, 0);
                movingUp = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingUp = true;
            }


        }
        if (movingUp == false && wallInfoDown.collider == true
            && wallInfoDown.collider.gameObject.tag != "Enemy")
        {
            if (movingUp == true)
            {
                transform.eulerAngles = new Vector3(-180, 0, 0);
                movingUp = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingUp = true;
            }

        }
    }
}
