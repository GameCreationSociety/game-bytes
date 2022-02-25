using UnityEngine;

namespace MeteorMasher
{
    public class MeteorMasher_Planet : MonoBehaviour
    {
        // We set the sprite that will be used for shield in the inspector
        [SerializeField] private SpriteRenderer ShieldSprite = null;
        private bool HasShield = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Did we hit a meteor?
            if (collision.GetComponent<MeteorMasher_Meteor>())
            {   
                // Destroy the meteor that hit us so that we don't collide with it again
                Destroy(collision.gameObject);
                if (HasShield)
                {
                    //Hide shield sprite since our shield is being turned off
                    ShieldSprite.enabled = false;

                    // Remove shield so next hit kills us
                    HasShield = false;
                }
                else
                {
                    // Lose the game!
                    MinigameController.Instance.FinishGame(LastMinigameFinish.LOST);
                }
            }
        }
    }
}
