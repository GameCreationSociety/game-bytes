using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tempo
{
    public class SettingsManager : MonoBehaviour
    {
        public int selected = 0;
        public string side = "left";
        public float cooldown = 0.28f;
        public float cooldown2 = 0.28f;
        public GameObject applyButtn;
        public GameObject returnButtn;
        public GameObject[] volumeSliderTool = new GameObject[3];
        public int blnorpble = 0;
        public GameObject[] calibratorTool = new GameObject[3];
        public GameObject eventCap;

        public TextMeshProUGUI t_volume;
        public TextMeshProUGUI t_m_volume;
        public TextMeshProUGUI t_calibration;
        public TextMeshProUGUI t_calibra;

        public Image volume_bar;
        public Image calibration_bar;

        public int masterVolume = 60;
        public static int volume = 60;
        public float calibration = 0.0f;
        public static float calibra = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            masterVolume = volume;
            calibration = calibra;

            applyButtn = GameObject.Find("Apply");    
            returnButtn = GameObject.Find("Return");
            volumeSliderTool[0] = null;
            volumeSliderTool[1] = null;
            volumeSliderTool[2] = null;
   
            volumeSliderTool[0] = GameObject.Find("VolumeSlider0");    
            volumeSliderTool[1] = GameObject.Find("VolumeSliderNote");    
            volumeSliderTool[2] = GameObject.Find("VolumeSlider1");
            print(volumeSliderTool);
            calibratorTool[0] = GameObject.Find("Calibrator0");
            calibratorTool[1] = GameObject.Find("CalibratorNote");
            calibratorTool[2] = GameObject.Find("Calibrator1");
            eventCap = GameObject.Find("EventSystem");
        }

        // Update is called once per frame
        void Update()
        {
            if (MinigameInputHelper.IsButton1Down(1) ||
                MinigameInputHelper.IsButton2Down(1) ||
                MinigameInputHelper.IsButton1Down(2) ||
                MinigameInputHelper.IsButton2Down(2) )
            {
                if (selected == 1)
                {
                    cooldown2 = 0.7f;
                    if (side == "left")
                    {
                        calibration -= (calibration <= -1 ? 0.0f : 0.01f);
                    }
                    else if (side == "right")
                    {
                        calibration += (calibration < 1 ? 0.01f : 0.0f);
                    }
                
                }
                else if (selected == 0)
                {
                    cooldown2 = 0.7f;
                    if (side == "left")
                    {
                        masterVolume -= (masterVolume <= 0 ? 0 : 1);
                    }
                    else if (side == "right")
                    {
                        masterVolume += (masterVolume <= 100 ? 1 : 0);
                    }
                }
                else if (selected == 2)
                {
                    calibra = calibration;
                    volume = masterVolume;
                }
                else if (selected == 3)
                {
                    returnToMainMenu();
                }
            }

            float joystickx1, joystickx2;
            float joysticky1, joysticky2;

            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                joystickx1 = MinigameInputHelper.GetHorizontalAxis(1);
                joystickx2 = MinigameInputHelper.GetHorizontalAxis(2);
                joysticky1 = MinigameInputHelper.GetVerticalAxis(1);
                joysticky2 = MinigameInputHelper.GetVerticalAxis(2);

                if (joysticky1 < 0 || joysticky2 < 0) //scroll up
                {
                    selected = (selected + 1) % 4;
                    cooldown = 0.28f;
                }
                else if (joysticky1 > 0 || joysticky2 > 0) //scroll down
                {
                    selected = (selected - 1 + 4) % 4;
                    cooldown = 0.28f;
                }

                if (selected < 2) {
                    if (joystickx1 < 0 || joystickx2 < 0) //left nudge
                    {
                        side = "left";
                        cooldown2 = 1.3f;

                        if (selected == 0) {
                            masterVolume -= (masterVolume <= 0 ? 0 : 1);
                            cooldown = 0.1f;
                        }
                        else {
                            calibration -= (calibration <= -1 ? 0.0f : 0.01f);
                            cooldown = 0.1f;
                        }
                    }
                    else if (joystickx1 > 0 || joystickx2 > 0) //right nudge
                    {
                        side = "right";
                        cooldown2 = 1.3f;

                        if (selected == 0)
                        {
                            masterVolume += (masterVolume < 100 ? 1 : 0);
                            cooldown = 0.1f;
                        }
                        else 
                        { 
                            calibration += (calibration <= 1 ? 0.01f : 0.0f);
                            cooldown = 0.1f;
                        }

                    }
                }
            }

            //eventCap.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            if (true)
            {
                if (selected == 0)
                {
                    volumeSliderTool[1].GetComponent<Button>().Select();
                }
                else if (selected == 1)
                {
                    calibratorTool[1].GetComponent<Button>().Select();
                }
                else if (selected == 2)
                {
                    applyButtn.GetComponent<Button>().Select();
                }
                else
                {
                    returnButtn.GetComponent<Button>().Select();
                }
            }

            if (cooldown2 > 0)
            {
                if (side == "left")
                {
                    if (selected == 0)
                    {
                        volumeSliderTool[0].GetComponent<Button>().Select();
                    }
                    else
                    {
                        calibratorTool[0].GetComponent<Button>().Select();
                    }
                }
                else
                {
                    if (selected == 0)
                    {
                        volumeSliderTool[2].GetComponent<Button>().Select();
                    }
                    else
                    {
                        calibratorTool[2].GetComponent<Button>().Select();
                    }
                }

                cooldown2 -= Time.deltaTime;
            }

            t_calibration.text = "Delay: " + (Mathf.Round(calibration*100)/100).ToString();
            calibration_bar.fillAmount = (calibration + 1) / 2;
            t_m_volume.text = "Master Volume: " + masterVolume.ToString();
            volume_bar.fillAmount = ((float)masterVolume)/100;
        

        }


        void returnToMainMenu()
        {
            SceneManager.LoadScene("Menu_Scene");
        }
    }
}
