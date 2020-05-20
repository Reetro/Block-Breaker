using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle Paddle1 = default;
    [SerializeField] float xVelocity = 2f;
    [SerializeField] float yVelocity = 15f;
    [SerializeField] AudioClip[] ballSounds = null;
    [SerializeField] float randomFactor = 0.2f;

    // Distance from paddle to ball
    Vector2 paddleToBallVector;
    Rigidbody2D myRigidbody2D;

    bool bLaunched = false;

    AudioSource myAudioSorce;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - Paddle1.transform.position;

        myAudioSorce = GetComponent<AudioSource>();

        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!bLaunched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bLaunched = true;
            myRigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
        }
    }

    private void LockBallToPaddle()
    {
        // Current position of the paddle
        Vector2 PaddlePos = new Vector2(Paddle1.transform.position.x, Paddle1.transform.position.y);
        // Get new paddle position
        transform.position = PaddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0, randomFactor), Random.Range(0, randomFactor));

        if (bLaunched)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];

            myAudioSorce.PlayOneShot(clip);

            myRigidbody2D.velocity += velocityTweak;
        }
    }
}
