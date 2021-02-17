using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    private const float SPEED = 20f;
    private const float SIZE = 19.5f;
    private const float OFFSET = 2f;

    public bool isLocal = true;
    public UDPReceive udpReceive;
    public string clientName = "";

    private float pythonX = 0.0f;
    private float pythonY = 0.0f;

    private float x;
    private float width;
    private float xPercent;
    private float xPosition;
    private float y;
    private float height;
    private float yPercent;
    private float yPosition;

    string direction;
    Vector3 move;

    public GameObject mainCamera;
    public GameObject birdCamera;
    public GameObject broccoliCamera;
    public GameObject catCamera;
    private static GameObject targetCamera;
    private static string targetCameraString = "main";
    private static string previousCameraString = "main";
    private static GameObject originalCamera;
    private static GameObject previousCamera;
    private static bool isMainCameraOn = true;
    private static GameObject[] cameras = new GameObject[4];
    private static Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    private int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;
    static float time = 1.0f;

    private bool middleBlock = false;

    public float GetWidthPercentage()
    {
        return transform.position.x / SIZE;
    }

    public float GetHeightPercentage()
    {
        return transform.position.z / SIZE;
    }

    public void setPosition(float widthPercentage, float heightPercentage)
    {
        pythonX = -SIZE + ((SIZE * 2) * widthPercentage);
        pythonY = SIZE - ((SIZE * 2) * heightPercentage);
        print("python " + pythonX + ", " + pythonY);
    }

    void Start()
    {
        targetCamera = new GameObject();
        targetCamera.transform.position = mainCamera.transform.position;
        previousCamera = new GameObject();
        previousCamera.transform.position = mainCamera.transform.position;
        originalCamera = new GameObject();
        originalCamera.transform.position = mainCamera.transform.position;
    }

    void Update()
    {
        float extraX = 1.525f;
        float extraY = 0.65f;
        Vector3 moveDir = Vector3.zero;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -SIZE * extraX, SIZE * extraX),
            transform.position.y,
            Mathf.Clamp(transform.position.z, -SIZE, SIZE)
        );

        x = Input.mousePosition.x;
        width = Screen.width;
        xPercent = Mathf.Clamp(x / width, 0.0f, 1.0f);
        xPosition = -SIZE + (SIZE * 2f * xPercent);
        y = Input.mousePosition.y;
        height = Screen.height;
        yPercent = Mathf.Clamp(y / height, 0.0f, 1.0f);
        yPosition = -SIZE + (SIZE * 2f * yPercent);
        if (isLocal)
        {
            transform.position = new Vector3(
                Mathf.Clamp(xPosition * extraX, -SIZE * extraX, SIZE * extraX),
                transform.position.y,
                Mathf.Clamp(yPosition, -SIZE + 0.65f, SIZE)
            );
            lastPosition = transform.position;
        }
        else
        {
            direction = udpReceive.Data;
            float speed = 10f;
            move = new Vector3(0f, 0f, 0f);
            if (direction.Contains(clientName)) {
                if (direction.Contains("up"))
                {
                    move += new Vector3(0f, 0f, 1f);
                }
                if (direction.Contains("down"))
                {
                    move += new Vector3(0f, 0f, -1f);
                }
                if (direction.Contains("left"))
                {
                    move += new Vector3(-1f, 0f, 0f);
                }
                if (direction.Contains("right"))
                {
                    move += new Vector3(1f, 0f, 0f);
                }
                transform.position += move * speed * Time.deltaTime;

                if (direction.Contains("l3") && !middleBlock)
                {
                    setCamera("feather");
                    middleBlock = true;
                }
                if (!direction.Contains("l3") && middleBlock)
                {
                    middleBlock = false;
                }
            }
        }
        if (Input.GetButtonDown("Fire3"))
        {
            setCamera("local");
        }

        if (!isMainCameraOn)
        {
            if (targetCameraString.Contains("local"))
            {
                targetCamera.transform.position = birdCamera.transform.position;
            }
            if (targetCameraString.Contains("feather"))
            {
                targetCamera.transform.position = broccoliCamera.transform.position;
            }
        }
        time += Time.deltaTime;
        float interpolationRatio = time / 1f;
        Vector3 interpolatedPosition = Vector3.Lerp(previousCamera.transform.position, targetCamera.transform.position, interpolationRatio);
        mainCamera.transform.position = interpolatedPosition;
    }

    private void setCamera(string something)
    {
        time = 0f;
        previousCamera.transform.position = mainCamera.transform.position;
        if (isMainCameraOn && something.Contains("local"))
        {
            targetCameraString = something;
            isMainCameraOn = false;
        }
        else if (isMainCameraOn && something.Contains("feather"))
        {
            targetCameraString = something;
            isMainCameraOn = false;
        }
        else if (!isMainCameraOn && something.Contains("local") && !targetCameraString.Contains("local"))
        {
            targetCameraString = something;
            isMainCameraOn = false;
        }
        else if (!isMainCameraOn && something.Contains("feather") && !targetCameraString.Contains("feather"))
        {
            targetCameraString = something;
            isMainCameraOn = false;
        }
        else
        {
            targetCamera.transform.position = originalCamera.transform.position;
            isMainCameraOn = true;
        }
        //isMainCameraOn = !isMainCameraOn;
    }
}
