using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tempo
{
    public class LevelSelect : MonoBehaviour
    {
        public int selected = 0; // index of selected button (0 start, 1 settings)
        public float coolDown = 0.3f; // separate the movements of the joystick
        public GameObject play;
        public GameObject settings;


        GameObject myEventSystem;

        private GameObject[] buttons = new GameObject[5];
        void Start(){
            buttons[0] = GameObject.Find("Beginner");
            buttons[1] = GameObject.Find("Intermediate");
            buttons[2] = GameObject.Find("Expert");
            buttons[3] = GameObject.Find("Back");
            buttons[4] = GameObject.Find("Click Track");
            myEventSystem = GameObject.Find("EventSystem");

        }
        void Update(){
            // load the level select menu if any button is pressed
            if(MinigameInputHelper.IsButton1Up(1) || 
               MinigameInputHelper.IsButton1Up(2) || 
               MinigameInputHelper.IsButton2Up(1) || 
               MinigameInputHelper.IsButton2Up(2)){
                if(selected == 3){
                    LoadMainMenu();
                    return;
                } 

                if(selected == 0) {
                    LevelState.beatMapFilename = "Tempo/Beatmaps/Easy";
                    LevelState.songFilename = "Tempo/Songs/Easy";
                } else if (selected == 1) {
                    LevelState.beatMapFilename = "Tempo/Beatmaps/Intermediate";
                    LevelState.songFilename = "Tempo/Songs/Intermediate";
                } else if (selected == 2) {
                    LevelState.beatMapFilename = "Tempo/Beatmaps/Expert";
                    LevelState.songFilename = "Tempo/Songs/Expert";
                } else if (selected == 4)
                {
                    LevelState.beatMapFilename = "Tempo/Beatmaps/Click Track";
                    LevelState.songFilename = "Tempo/Songs/Click Track";
                }
                PlayGame();
            }


            if(coolDown <= 0){

                // are one or more of the joysticks being pushed
                float joystick1 = MinigameInputHelper.GetVerticalAxis(1);
                float joystick2 = MinigameInputHelper.GetVerticalAxis(2);

                bool joystick_up = joystick1 > 0 || joystick2 > 0;
                bool joystick_down = joystick1 < 0 || joystick2 < 0;

                if (joystick_up)
                {
                    selected = (selected + 4) % 5;
                    coolDown = 0.3f;
                } else if (joystick_down)
                {
                    selected = (selected + 1) % 5;
                    coolDown = 0.3f;
                }

                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                buttons[selected].GetComponent<Button>().Select();

            
         
            } else if (coolDown > 0)
            {
                coolDown -= Time.deltaTime;
            }


        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Menu_Scene");
        
        }

        public void PlayGame()
        {
            SceneManager.LoadScene("Tempo_Game");
        }
    }
}
