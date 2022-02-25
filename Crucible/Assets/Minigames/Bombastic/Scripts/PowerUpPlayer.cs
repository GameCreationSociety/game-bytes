using TMPro;
using UnityEngine;

namespace Bombastic
{
    public class PowerUpPlayer : MonoBehaviour
    {

        //Store current power up; 0 = None, 1 = Speed Boost, 2 = Jump Boost
        public float powerUpSpeedScalar = 1.75f;
        public float powerUpJumpScalar = 1.5f;
        public TextMeshProUGUI powerUpText;
        public float decayTime;
        public bool hasPowerUp = false;

        MovementController movementController;
        int playerNumber;

        // Start is called before the first frame update
        void Start()
        {
            //Get the movement controller script
            movementController = this.gameObject.GetComponent<MovementController>();
            playerNumber = movementController.playerNumber;
            powerUpText.SetText("");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void setPowerUp(int powerUpType)
        {
            switch (powerUpType)
            {
                case 1:
                    if (!hasPowerUp)
                    {
                        movementController.setMoveSpeed(movementController.defaultMoveSpeed * powerUpSpeedScalar);
                        powerUpText.SetText("Speed boost!");
                        Invoke("cleansePowerUps", decayTime);
                        //hasPowerUp = true; set in sfx player
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 2:
                    if (!hasPowerUp)
                    {
                        movementController.setJumpForce(movementController.defaultJumpForce * powerUpJumpScalar);
                        powerUpText.SetText("Jump Boost!");
                        Invoke("cleansePowerUps", decayTime);
                        //hasPowerUp = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 3:
                    if (!hasPowerUp)
                    {
                        movementController.hasDoubleJump = true;
                        powerUpText.SetText("Double Jump!");
                        Invoke("cleansePowerUps", decayTime);
                        //hasPowerUp = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 4:
                    if (!hasPowerUp)
                    {
                        movementController.hasJetPack = true;
                        powerUpText.SetText("Jet pack!");
                        Invoke("cleansePowerUps", decayTime);
                        //hasPowerUp = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                case 5:
                    if (!hasPowerUp)
                    {
                        movementController.hasDash = true;
                        powerUpText.SetText("Dash!");
                        Invoke("cleansePowerUps", decayTime);
                        //hasPowerUp = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                default:
                    break;
            }

        }

        //Reset power ups
        public void cleansePowerUps()
        {
            movementController.setMoveSpeed(movementController.defaultMoveSpeed);
            movementController.setJumpForce(movementController.defaultJumpForce);
            movementController.hasDoubleJump = false;
            movementController.hasJetPack = false;
            movementController.hasDash = false;
            powerUpText.SetText("");
            hasPowerUp = false;
        }
    }
}
