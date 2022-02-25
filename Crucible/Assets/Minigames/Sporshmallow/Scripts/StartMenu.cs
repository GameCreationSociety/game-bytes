using UnityEngine;

namespace Sporshmallow
{
	public class StartMenu : MonoBehaviour
	{
		Sprite tennis, karate, random;
		GameObject p1Select, p2Select;
		GameObject player1, player2;
		GameObject p1_img, p2_img;
		GameObject main_game;
		int image = 0;//0 for menu, 1/2 for player 1/2
		int count = 0;
		void Start()
		{
			//tennis = Resources.Load("IMG_1928", typeof(Sprite)) as Sprite;
			//karate = Resources.Load("IMG_1914", typeof(Sprite)) as Sprite;
			tennis =  Resources.Load<Sprite>("IMG_1914") as Sprite;//LoadNewSprite("Assets/Minigames/Sparty/Animations/crouch/IMG_1914.PNG");
			karate = Resources.Load<Sprite>("IMG_1918") as Sprite;//LoadNewSprite("Assets/Minigames/Sparty/Animations/crouch/IMG_1928.PNG");
			random = Resources.Load<Sprite>("random") as Sprite;
		
			p1_img = GameObject.Find("P1");
			p2_img = GameObject.Find("P2");
			p1Select = GameObject.Find("Player1Select");
			p2Select = GameObject.Find("Player2Select ");
			main_game = GameObject.Find("Game");
			player1 = GameObject.Find("Player1");
			player2 = GameObject.Find("Player2");
			if(main_game == null) Debug.LogError("could not find main game");
			if(p1Select == null) Debug.LogError("could not find p1");
			if(p2Select == null) Debug.LogError("could not find p2");
			if(player1 == null) Debug.LogError("could not find player1");
			if(player2 == null) Debug.LogError("could not find player2");
			if(p1_img == null) Debug.LogError("could not find p1_img");
			if(p2_img == null) Debug.LogError("could not find p2_img");
			count = 0;
			main_game.gameObject.SetActive(false);//turns off the game
		}

		// Update is called once per frame
		void Update()
		{
			bool p1_select = p1Select.GetComponent<MenuScript>().select;
			bool p2_select = p2Select.GetComponent<MenuScript>().select;
			int p1_string = p1Select.GetComponent<MenuScript>().TYPE;
			int p2_string = p2Select.GetComponent<MenuScript>().TYPE;
			if(p1_select && p2_select){//BEGIN GAME
				count++;
				player1.GetComponent<MoveScript>().TYPE = p1_string;
				player2.GetComponent<MoveScript>().TYPE = p2_string;
				if(count > 50){//timer
					main_game.gameObject.SetActive(true);
					this.gameObject.SetActive(false);
				}
			}
			int cursor_p1 = p1Select.GetComponent<MenuScript>().place;
			int cursor_p2 = p2Select.GetComponent<MenuScript>().place;
			if(cursor_p1 == 0){
				p1_img.GetComponent<SpriteRenderer>().sprite = tennis;
			}
			else if(cursor_p1 == 1){
				p1_img.GetComponent<SpriteRenderer>().sprite = karate;
			}
			else p1_img.GetComponent<SpriteRenderer>().sprite = random;
			if(cursor_p2 == 0){
				p2_img.GetComponent<SpriteRenderer>().sprite = tennis;
			}
			else if(cursor_p2 == 1){
				p2_img.GetComponent<SpriteRenderer>().sprite = karate;
			}
			else p2_img.GetComponent<SpriteRenderer>().sprite = random;
			p1_img.transform.localScale = new Vector3((float)30, (float)30, 1);
			p2_img.transform.localScale = new Vector3((float)30, (float)30, 1);

		}
	}
}
