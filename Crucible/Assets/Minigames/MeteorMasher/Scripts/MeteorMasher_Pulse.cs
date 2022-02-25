using System.Collections.Generic;
using UnityEngine;

// Pulse used to bounce away meteors
namespace MeteorMasher
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MeteorMasher_Pulse : MonoBehaviour
    {
        // Parameters of the pulse editable in the inspector
        [SerializeField] private int PlayerNum = 1;
        [SerializeField] private float MaxScale = 2;
        [SerializeField] private float MinImpulseForce = 30;
        [SerializeField] private float MaxImpulseForce = 60;
        [SerializeField] private Color PulseColor = Color.clear;
        [SerializeField] private float PulseDuration = 0.5f;

        private float CurrentPulseTime;
        private bool IsPulsing; 
        private SpriteRenderer PulseSprite;
        private Color InitalColor;
    
        private List<Rigidbody2D> HitBodies;

        // Get a number form 0 to 1, where 0 is beginning of pulse and 1 is the end of pulse. Handy for lerping.
        public float GetPulseRatio()
        {
            return CurrentPulseTime / PulseDuration;
        }

        // Start is called before the first frame update
        void Start()
        {
            HitBodies = new List<Rigidbody2D>();
            PulseSprite = GetComponent<SpriteRenderer>();
            InitalColor = PulseSprite.color;
        }

        // Update the pulse sprite depending on how much we have pulsed already
        void UpdatePulseEffects(float lerpVal)
        {
            PulseSprite.color = Color.Lerp(InitalColor, PulseColor, lerpVal);
            transform.localScale = new Vector3(1,1,1) * Mathf.Lerp(0, MaxScale, lerpVal);
        } 

        // Update is called once per frame
        void Update()
        {
            // If player presses button 1, try to start a new pulse
            if (MinigameInputHelper.IsButton1Down(PlayerNum) && !IsPulsing)
            {
                CurrentPulseTime = 0;
                IsPulsing = true;
                HitBodies.Clear();
            }

            // Update the pulse time if we're pulsing
            if (IsPulsing)
            {
                CurrentPulseTime += Time.deltaTime;
                if (CurrentPulseTime > PulseDuration)
                {
                    IsPulsing = false;
                    CurrentPulseTime = 0;
                }
            }

            //Update the visual effects of the pulse
            UpdatePulseEffects(CurrentPulseTime / PulseDuration);
        }

        // Bounce Meteors away when they collide with an active pulse
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Rigidbody2D ColBody = collision.GetComponent<Rigidbody2D>();
            if (ColBody && IsPulsing)
            {
                bool AlreadyHit = false;
                for(int i = 0; i < HitBodies.Count; i++)
                {
                    if( ColBody == HitBodies[i])
                    {
                        AlreadyHit  = true;
                        break;
                    }
                }

                if (!AlreadyHit)
                {
                    ColBody.velocity = ((collision.transform.position - transform.position).normalized * Mathf.Lerp(MaxImpulseForce, MinImpulseForce, GetPulseRatio()) );
                    HitBodies.Add(ColBody);
                }
            }
        }
    }
}
