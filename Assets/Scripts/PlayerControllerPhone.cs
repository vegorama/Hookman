using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerPhone : MonoBehaviour
{

    [Header("Player Settings")]
    public float moveSpeed;
    public float jumpForce;
    public float jumpTime;
    public float jumpTimeCounter;

    [Header("Button Refs")]
    public Button leftButton;
    public Button rightButton;

    [Header("Grounded Settings")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public bool isGrounded;
    public LayerMask groundLayerRef;
    private bool stoppedJumping;

    [Header("Speed Increase Settings")]
    private float speedMilestoneCount;
    public float speedIncreaseMilestone;
    public float speedMultiplier;

    private float speedMilestoneCountStore;
    private float moveSpeedStore;
    private float speedIncreaseMilestoneStore;

    [Header("Rope Shoot Settings")]
    public bool hasRope;
    public bool isSwinging;
    public GameObject ropeShooter;
    public GameObject ropeLocation;
    public GameObject ropePivot;
    private Vector3 ropeHitPoint;
    public SpringJoint2D rope;
    public int maxRopeFrameCount;
    private int ropeFrameCount;
    public LineRenderer lineRenderer;
    private bool ropeSwingandaMiss;

    [Header("Air Dash Settings")]
    public bool canAirDash;
    public GameObject topOfHeadRef;
    public CameraController camControllerRef;
    public GameObject dashSprite;
    public Vector3[] dashSpriteSpawnPos;

    [Header("Animation Settings")]
    public Animator spriteAnimatorRef;
    private Rigidbody2D myRigidBody;
    public ObjectPooler airDashSpritePool;
    public GameObject sparkleRef;
    public GameObject dashSparkleRef;

    [Header("Sound Refs")]
    public AudioSource jumpSound;
    public AudioSource deadSound;
    public AudioSource flaunchSound;
    public AudioSource ropeHit;
    public AudioSource ropeMiss;

    [Header("Other References")]
    public GameManager gameManager;
    [SerializeField]
    private Rigidbody2D weemanRigidbody;
    public bool gameRunning;
    public GameObject circleBurst;


    // Use this for initialization
    void Start()
    {
        jumpTimeCounter = jumpTime;
        myRigidBody = GetComponent<Rigidbody2D>();


        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        stoppedJumping = true;
        isSwinging = false;
        canAirDash = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Set isGrounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerRef);

        //Set Animator
        spriteAnimatorRef.SetFloat("Speed", myRigidBody.velocity.x);
        spriteAnimatorRef.SetBool("IsGrounded", isGrounded);
        spriteAnimatorRef.SetBool("IsSwinging", isSwinging);
        spriteAnimatorRef.SetBool("CanAirDash", canAirDash);


        //Update when game is running:
        if (gameRunning == true)
        {
            //Speed increase over time
            if (transform.position.x > speedMilestoneCount)
            {
                speedMilestoneCount += speedIncreaseMilestone;

                speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
                moveSpeed = moveSpeed * speedMultiplier;
            }

            //Running and variables
            if (isGrounded)
            {
                weemanRigidbody.velocity = new Vector2(moveSpeed, weemanRigidbody.velocity.y);
                jumpTimeCounter = jumpTime;

                if (canAirDash == true)
                {
                    canAirDash = false;
                }
                if (hasRope == false)
                {
                    hasRope = true;
                }
            }

            //Rope shooting
            if (Input.GetKeyDown(KeyCode.Z) || rightButton.GetComponent<ButtonListener>()._pressed)
            {
                if (hasRope)
                    RopeShoot();
            }

            //Air Dashing
            if (Input.GetKeyDown(KeyCode.Space) || leftButton.GetComponent<ButtonListener>()._pressed)
            {
                if (!isGrounded && canAirDash && !isSwinging)
                {
                    AirDash();
                    stoppedJumping = false;
                    canAirDash = false;
                }

                if (isGrounded || isSwinging)
                {
                    if (isSwinging)
                    {
                        isSwinging = false;
                        GameObject.DestroyImmediate(rope);
                    }

                    weemanRigidbody.velocity = new Vector2(weemanRigidbody.velocity.x, jumpForce);
                    stoppedJumping = false;
                    jumpSound.Play();
                }
            }

            //AirDash Sparkles
            if (canAirDash == true && sparkleRef.activeSelf == false)
            {
                sparkleRef.SetActive(true);
            }
            else if (canAirDash == false && sparkleRef.activeSelf == true)
            {
                sparkleRef.SetActive(false);
            }

            //Jumping
            if ((Input.GetKey(KeyCode.Space) || leftButton.GetComponent<ButtonListener>()._pressed) && !stoppedJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    weemanRigidbody.velocity = new Vector2(weemanRigidbody.velocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space) || !leftButton.GetComponent<ButtonListener>()._pressed)
            {
                jumpTimeCounter = 0;
                stoppedJumping = true;
            }
        }
    }

    private void LateUpdate()
    {
        if (rope != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, ropePivot.transform.position);
            lineRenderer.SetPosition(1, ropeHitPoint);
        }
        else if ((rope = null) && (ropeSwingandaMiss == false))
        {
            lineRenderer.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Killbox")
        {
            deadSound.Play();
            gameManager.RestartGame();
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            speedMilestoneCount = speedMilestoneCountStore;
            moveSpeed = moveSpeedStore;

            GameObject burstAnimation = Instantiate(circleBurst, transform.position, Quaternion.identity) as GameObject;
            burstAnimation.GetComponent<SpriteRenderer>().color = new Color32(88, 80, 248, 255);
        }
    }

    private void RopeShoot()
    {
        Vector3 ropeTarget = ropeLocation.transform.position;
        Vector3 position = ropePivot.transform.position;
        Vector3 direction = ropeTarget - position;

        int layerMask = 1 << 8;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, Mathf.Infinity, layerMask);
        Debug.DrawRay(position, direction, Color.white, 5f);

        if (hit.collider != null)
        {
            //play Sound
            ropeHit.Play();

            //hasRope = false;
            isSwinging = true;
            jumpTimeCounter = jumpTime;
            canAirDash = true;

            ropeHitPoint = hit.point;

            //Debug.Log("Hit result" + hit.transform.gameObject);
            SpringJoint2D newRope = ropeShooter.AddComponent<SpringJoint2D>();
            newRope.enableCollision = true;
            newRope.frequency = 6f;
            newRope.connectedAnchor = hit.point;
            newRope.enabled = true;

            GameObject.DestroyImmediate(rope);
            rope = newRope;
            ropeFrameCount = 0;
        }

        else if (hit.collider == null)
        {
            ropeMiss.Play();
            StartCoroutine(RopeMissAnimation());
        }
    }

    private IEnumerator RopeMissAnimation()
    {
        ropeSwingandaMiss = true;

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, ropePivot.transform.position);
        lineRenderer.SetPosition(1, ropeLocation.transform.position);

        yield return new WaitForSeconds(0.5f);

        lineRenderer.enabled = false;
        ropeSwingandaMiss = false;
    }

    private void AirDash()
    {
        //Play sound
        flaunchSound.Play();

        //Set AirDash bool for Camera Controller
        camControllerRef.AirDashCam();
        Vector3 startPosition = ropeShooter.transform.position;

        int layerMask = 1 << 8;

        Vector3 direction = transform.TransformDirection(transform.right);

        //Raycast from top and bottom of player for collision check.
        RaycastHit2D hit = Physics2D.Raycast(ropeShooter.transform.position, direction, 4, layerMask);
        Debug.DrawRay(ropeShooter.transform.position, direction, Color.yellow, 5f);

        RaycastHit2D headHit = Physics2D.Raycast(topOfHeadRef.transform.position, direction, 4, layerMask);
        Debug.DrawRay(topOfHeadRef.transform.position, direction, Color.yellow, 5f);


        float distanceTeleported = 0;

        if (hit.collider)
        {
            ropeShooter.transform.position = hit.point;
            distanceTeleported = hit.distance;
        }
        else if (headHit.collider)
        {
            //ropeShooter.transform.position += ropeShooter.transform.right * headHit.distance;
            ropeShooter.transform.position = headHit.point;
            distanceTeleported = headHit.distance;
        }
        else
        {
            ropeShooter.transform.position += ropeShooter.transform.right * 4;
            distanceTeleported = 4f;
        }

        for (int i = 0; i < dashSpriteSpawnPos.Length; i++)
        {
            dashSpriteSpawnPos[i] = startPosition + direction * ((i * distanceTeleported) / dashSpriteSpawnPos.Length);

            //spawn images from Obj Pool
            GameObject dashImage = airDashSpritePool.GetPooledObject();
            dashImage.transform.position = dashSpriteSpawnPos[i];
            dashImage.SetActive(true);
            dashImage.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, ((float)i / dashSpriteSpawnPos.Length) * 0.5f);
            dashImage.GetComponent<AirDashFadeDestroy>().Fade();
        }

        //Show Dash Sparkles
        dashSparkleRef.GetComponent<SparkleAutoSelfDisable>().CallEnableDisable();

    }

    public void CallOnDeath()
    {
        canAirDash = false;
        isGrounded = false;
        sparkleRef.SetActive(false);
        dashSparkleRef.SetActive(false);
        gameRunning = false;
        isSwinging = false;
        lineRenderer.enabled = false;
        gameObject.SetActive(false);
    }

}