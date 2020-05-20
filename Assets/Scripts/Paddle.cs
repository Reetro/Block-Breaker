using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float ScreenWidthInUnits = 16f;
    [SerializeField] float MaxPaddleMovementX = 15f;
    [SerializeField] float MinPaddleMovementX = 1f;

    GameSession gameSession;
    Ball myBall;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        myBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 PaddlePos = new Vector2(transform.position.x, transform.position.y);

        // Move to mouse position x but clamp the movement to screen
        PaddlePos.x = Mathf.Clamp(GetXPostion(), MinPaddleMovementX, MaxPaddleMovementX);
        transform.position = PaddlePos;
    }
    private float GetXPostion()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return myBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * ScreenWidthInUnits;
        }
    }
}
