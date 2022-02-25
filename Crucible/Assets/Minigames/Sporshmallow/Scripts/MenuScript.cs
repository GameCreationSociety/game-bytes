using UnityEngine;

namespace Sporshmallow
{
	public class MenuScript : MonoBehaviour
	{
		GameObject player1;//, player2;
		const float move_amnt = (float)1.6;
		// Start is called before the first frame update
		public int place = 0;
		public bool select = false;
		public int TYPE;
		int player = 0;
		bool up = false;
		bool down = false;
		const int min = 0;
		const int max = 2;
		void Start()
		{
			player1 = GameObject.Find("Player1Select");
			if(this.name == "Player1Select") player = 0;
			else player = 1;
			place = 0;
			TYPE = -1;//nothing yet
			//player2 = GameObject.Find("Player2Select");
		}

		// Update is called once per frame
		void Update()
		{
			//MinigameInputHelper.IsButton1Down(player)
			float v_axis = MinigameInputHelper.GetVerticalAxis(player+1);
			if(v_axis > 0 && !up && place > min && !select){
				place--;
				up = true;
				transform.Translate(
					Vector3.up * move_amnt
				);		
				down = false;		
			}
			if (v_axis < 0 && !down && place < max && !select){
				place++;
				down = true;
				transform.Translate(
					Vector3.down * move_amnt
				);	
				up = false;	
			}
			if(v_axis == 0 ){
				up = false;
				down = false;		
			}
			if(MinigameInputHelper.IsButton1Down(player+1)){
				select = !select;
			}
			if(place == 0){
				TYPE = 0;
			}else if(place == 1){
				TYPE = 4;
			}else if(place == 2){
				TYPE = ((int)(Random.Range(0, 2)) * 4);//random between the two
			}
		}
	}
}
