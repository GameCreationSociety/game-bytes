using UnityEngine;

namespace Bombastic
{
    public class cameraFollow : MonoBehaviour
    {
        GameObject player1;
        GameObject player2;

        // How many units should we keep from the players
        float zoomFactor = 1.5f;
        float followTimeDelta = 0.8f;

        // sets a minimum and maximum size of camera to prevent zooming too far in or out
        public float minOrthoSize = 5.0f;
        public float maxOrthoSize = 10.0f;

        // objects that set bounds for camera to stay within
        public GameObject leftWall;
        public GameObject rightWall;
        public GameObject ground;
        public GameObject ceiling;

        // Start is called before the first frame update
        void Start()
        {
            player1 = GameObject.Find("Player");
            player2 = GameObject.Find("Player 2");
        }

        // Update is called once per frame
        void Update()
        {
            FixedCameraFollowSmooth(Camera.main, player1.transform, player2.transform);
        }

        // adapted from: https://answers.unity.com/questions/1142089/moving-camera-with-2-players.html
        // Follow Two Transforms with a Fixed-Orientation Camera
        public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
        {
            float rightBound = rightWall.transform.position.x - 0.1f;
            float leftBound = leftWall.transform.position.x + 0.1f;
            float topBound = ceiling.transform.position.y - 0.1f;
            float bottomBound = ground.transform.position.y;

            float halfHeight = cam.orthographicSize;
            float halfWidth = cam.aspect * halfHeight;

            float camX = Mathf.Clamp((t1.position.x + t2.position.x) / 2f, leftBound + halfWidth, rightBound - halfWidth);
            float camY = Mathf.Clamp((t1.position.y + t2.position.y) / 2f, bottomBound + halfHeight, topBound - halfHeight);

        


            // Midpoint we're after
            Vector3 midpoint = (t1.position + t2.position) / 2f;

            // Distance between objects
            float distance = (t1.position - t2.position).magnitude;        
        
            // Adjust ortho size
            if (cam.orthographic)
            {
                // only change the size if it will be an acceptable size
                if (distance > minOrthoSize && distance < maxOrthoSize)
                {
                    cam.orthographicSize = distance;
                } //else set it to max or min
                else if (distance < minOrthoSize)
                {
                    cam.orthographicSize = minOrthoSize;
                }
                else if (distance > maxOrthoSize)
                {
                    cam.orthographicSize = maxOrthoSize;
                }
            }

            //cam.transform.position = new Vector3(camX, camY, cam.transform.position.z);
            cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3(camX, camY, cam.transform.position.z), followTimeDelta);
            AspectUtility.SetCamera();
        }
    }
}
