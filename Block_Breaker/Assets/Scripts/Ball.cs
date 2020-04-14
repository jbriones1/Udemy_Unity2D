using UnityEngine;

public class Ball : MonoBehaviour
{
    // parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float pushX =2f;
    [SerializeField] float pushY =15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 3f;

    // state 
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    GameSession ballGameSession;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        ballGameSession = FindObjectOfType<GameSession>();
        CheckAutoPlay();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        else
        {
            ResetBall();
        }
    }

    private void CheckAutoPlay()
    {
        if (ballGameSession.IsAutoPlayEnabled());
        {
            StartSession();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSession();
        }
    }

    private void StartSession()
    {
        myRigidBody2D.velocity = new Vector2(pushX, pushY);
        hasStarted = true;
    }

    // testing purposes
    private void ResetBall()
    {
        if (Input.GetMouseButtonDown(1))
        {
            hasStarted = false;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddleToBallVector + paddlePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor), 
            Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range( 0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}
