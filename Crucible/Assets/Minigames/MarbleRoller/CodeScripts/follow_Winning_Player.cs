using UnityEngine;

namespace MarbleRoller
{
    public class follow_Winning_Player : MonoBehaviour
    {
        // Marks Start and finish of map
        public Transform startMarker;
        public Transform endMarker;
        public Transform player1;
        public Transform player2;
        public Transform EndGoal;

        private float averageY;


        private Vector3 winningPlayerPosition;
        private Vector3 losingPlayerPosition;

        private float distanceBetween;

        public float positionInJourney;
        private float mapLength;
        private float minCameraSize;
        private float maxCameraSize;


        private Movement movingObject;
        private Camera gameCamera;
        private BoxCollider2D despawner;


        // Start is called before the first frame update
        void Start()
        {
            minCameraSize = 5;
            maxCameraSize = 10;
            gameCamera = GetComponent<Camera>();
            despawner = GetComponent<BoxCollider2D>();
            mapLength = Vector3.Distance(startMarker.position, endMarker.position);
        }

        // Update is called once per frame
        void Update()
        {

            distanceBetween = (player1.transform.position - player2.transform.position).magnitude; //
            //distanceBetween = (player1.transform.position.x - player2.transform.position.x);//only factor y axis
            gameCamera.orthographicSize = Mathf.Clamp (distanceBetween,minCameraSize, maxCameraSize);
            despawner.transform.localScale = new Vector3(1, 1, 1) * (gameCamera.orthographicSize / minCameraSize);


            if ( Vector3.Distance(EndGoal.position,player1.position) < Vector3.Distance(EndGoal.position, player2.position))
            { winningPlayerPosition = player1.position; losingPlayerPosition = player2.position;}
            else
            { winningPlayerPosition = player2.position; losingPlayerPosition = player1.position; }  
            positionInJourney = Vector3.Distance(winningPlayerPosition, startMarker.position) / mapLength;

            //will make camera follow as player on x axis, but focuses on the players for y axis
            Vector3 targetPoint = Vector3.Lerp(losingPlayerPosition, winningPlayerPosition, 0.75f) + new Vector3(0,0,-100);
            transform.position = Vector3.Lerp(transform.position, targetPoint, 0.1f);
        }
    }
}
