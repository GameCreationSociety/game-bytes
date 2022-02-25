using UnityEngine;
using UnityEngine.UI;

namespace Sporshmallow
{
	public class ButtonScript : MonoBehaviour
	{
		// Start is called before the first frame update
		bool hover = false;
		bool selected = false;
		const float hoverScale = (float)1.2;
		int current_place = 1;
		GameObject p1Select, p2Select;

		void Start(){
			p1Select = GameObject.Find("Player1Select");
			p2Select = GameObject.Find("Player2Select ");
			if(this.name == "TennisButton"){
				hover = true;
				current_place = 0;
			}
			else if(this.name == "KarateButton"){
				hover = false;
				current_place = 1;
			}
			else if(this.name == "BaseballButton"){
				hover = false;
				current_place = 2;
			}
			if(p1Select == null) Debug.LogError("could not find Player1Select");
			if(p2Select == null) Debug.LogError("could not find Player2Select");
		}
		// Update is called once per frame
		void Update(){
			int cursor_p1 = p1Select.GetComponent<MenuScript>().place;
			int cursor_p2 = p2Select.GetComponent<MenuScript>().place;
			bool p1_select = p1Select.GetComponent<MenuScript>().select;
			bool p2_select = p2Select.GetComponent<MenuScript>().select;
			bool p1_hover = cursor_p1 == current_place;
			bool p2_hover = cursor_p2 == current_place;
			if(p1_hover || p2_hover) {
				hover = true;
			}
			else hover = false;
			if(hover) this.transform.localScale = new Vector3(hoverScale, hoverScale,1);//scale
			else this.transform.localScale = new Vector3(1,1,1);//scale

			if((p1_hover && p1_select) || (p2_hover && p2_select)){
				selected = true;
			}
			else selected = false;
			/*if(p1_hover && p2_hover && selected){
			
		}*/
			if(p1_hover && selected) this.GetComponent<Image>().color = Color.green;
			else if (p2_hover && selected) this.GetComponent<Image>().color = Color.yellow;
			else this.GetComponent<Image>().color = Color.white;
		
		}
	}
}
