using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  private float moveSpeed = 4f;
  
  [SerializeField]
  public FixedJoystick fixedJoystick;
  

  [SerializeField]
  private GameObject arrow;
  [Space]
  
  private float playerMovementX;
  private GameObject canShoot;
    //public static bool isMobile;

  





  void HandlePlayerMovement()
  {
      playerMovementX = Mobile.CheckPlatform() ? fixedJoystick.Horizontal : Input.GetAxisRaw("Horizontal");
      
      var position = transform.position;
      position += new Vector3(playerMovementX, 0f, 0f) * (moveSpeed * Time.deltaTime);
      
      float playerWidth = transform.localScale.x;
      float screenLimitX = Camera.main.aspect * Camera.main.orthographicSize - playerWidth / 2f;
      
      float playerX = Mathf.Clamp(position.x, -screenLimitX +1f, screenLimitX-0.5f); // +1.5f// -1
      position = new Vector3(playerX, position.y, position.z);
      transform.position = position;
  }
  
  void HandleFacingDirection()
  {
     var tempScale = transform.localScale;

      if (playerMovementX > 0)
          tempScale.x = Mathf.Abs(tempScale.x);
      else if (playerMovementX < 0)
          tempScale.x = -Mathf.Abs(tempScale.x);

      transform.localScale = tempScale;
  }

  public void Fire()
  {
      if (canShoot == null)
      {
          var newArrow= Instantiate(arrow, transform.position ,Quaternion.identity);
          canShoot = newArrow;
      }
  }
    void Update()
    {
        HandlePlayerMovement();
        HandleFacingDirection();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Fire();
        }
    }
    
}
