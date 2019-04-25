using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    private float horizontalMove = 0f;//controll the horizontal movement
    private float verticalMove; //controll the vertical movement

    public float runSpeed = 40f;
    public float climbSpeed = 30f;

    private bool jump = false;
    private bool crouch = false;

    public GameObject projectile;
    public GameObject bomb;

    void Update()
    {

        #region Destroy environment
      
        #region Projectile
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
        #endregion

        #region Bomb
        if (Input.GetButtonDown("Bomb") && jump == false)
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
        }
        #endregion

        #endregion

        #region Move Horizontal and Vertical
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        verticalMove = Input.GetAxisRaw("Vertical") * climbSpeed; // climb on ladder
        #endregion

        #region Jump
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        #endregion

        #region Crouch
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))  //crouch until the crouch button is unpressed
        {
            crouch = false;
        }
        #endregion

        #region Switch Gravity

        

        #endregion
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, crouch, jump); // function from script CharacterController2D
        jump = false;
    }
}
