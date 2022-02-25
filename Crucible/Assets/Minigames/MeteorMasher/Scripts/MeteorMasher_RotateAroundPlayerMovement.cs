using UnityEngine;

// Script used to control the position of the player ship
namespace MeteorMasher
{
    [ExecuteInEditMode]
    public class MeteorMasher_RotateAroundPlayerMovement : MonoBehaviour
    {
        // Parameters controlling the movement and player number. Editable in inpector.
        [SerializeField] private int PlayerNum = 1;
        [SerializeField] private Transform RotationCenter = null;
        [SerializeField] private float RadiusAwayFromCenter = 10;
        [SerializeField] private float RotationSpeedDegreePerSecond = 10;
        [SerializeField] private float StartAngleDegrees = 0;

        // The current angle of the player from the center
        private float CurrentAngleDegrees;

        // Start is called before the first frame update
        void Start()
        {
            CurrentAngleDegrees = StartAngleDegrees;
        }

        // Update is called once per frame
        void Update()
        {
            // For preview purposes, update current angle to the start angle in editor only
            if(!Application.isPlaying)
            {
                CurrentAngleDegrees = StartAngleDegrees;
            }   

            // Set position based on Current Angle
            Vector2 OffsetVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad*CurrentAngleDegrees), Mathf.Sin(Mathf.Deg2Rad*CurrentAngleDegrees));
            transform.position = RotationCenter.position + (Vector3)OffsetVector * RadiusAwayFromCenter;

            //Set rotation based on Current Angle
            transform.rotation = Quaternion.Euler(0, 0, CurrentAngleDegrees);

            // Update the Current Angle to move to target direction
            Vector2 TargetVector = new Vector2(MinigameInputHelper.GetHorizontalAxis(PlayerNum), MinigameInputHelper.GetVerticalAxis(PlayerNum)).normalized;
            if (Mathf.Abs(TargetVector.x) > 0 || Mathf.Abs(TargetVector.y) > 0)
            {
                float TargetAngleDegrees = Mathf.Rad2Deg * Mathf.Atan2(TargetVector.y, TargetVector.x);
                float AngleDistance = Mathf.DeltaAngle(CurrentAngleDegrees, TargetAngleDegrees);
                float AngleDirection = Mathf.Sign(AngleDistance);
                if (Mathf.Abs(AngleDistance) < RotationSpeedDegreePerSecond)
                {
                    CurrentAngleDegrees = TargetAngleDegrees;
                }
                else
                { 
                    CurrentAngleDegrees += RotationSpeedDegreePerSecond * AngleDirection;
                }
            }
        }
    }
}