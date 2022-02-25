using UnityEngine;

namespace Sporshmallow
{
	public class HealthScript : MonoBehaviour
	{

		private float timer = 0.0f;
		private float waitTime = 0.3f;
		public int[] scores = new int[2];

		//private int startingHealth = 100;
		//public int playerHealth;
		//public Slider healthSlider; //reference to UI health bar

		int howMany;
		int circleCount;

		int player;
		GameObject other;
		Color orange;
		// Use this for initialization
		void Start()
		{
			scores[0] = 0;
			scores[1] = 0;
			orange = new Color(1f, 0.67f, 0f, 1f);
			if (gameObject.name == "Player1")
			{
				player = 1;
				other = GameObject.Find("Player2");
				gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
				other.GetComponent<Renderer>().material.SetColor("_Color", orange);
			}
			else
			{
				player = 2;
				other = GameObject.Find("Player1");
				gameObject.GetComponent<Renderer>().material.SetColor("_Color", orange);
				other.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
			}
			howMany = 0;
			GameObject.Find("p" + player + "pointcircle").transform.localScale = new Vector3(0, 0, 0);
		}

		// Update is called once per frame
		void Update()
		{
			timer += Time.deltaTime;

			// Check if we have reached beyond 1 seconds.
			// Subtracting one is more accurate over time than resetting to zero.
			if (timer > waitTime)
			{
				//MinigameController.Instance.AddScore(2, 2);
				// Remove the recorded 2 seconds.
				timer = timer - waitTime;
				var colorer = other.GetComponent<Renderer>();
				if(player == 1) colorer.material.SetColor("_Color", new Color(1f, 0.67f, 0f, 1f));
				else colorer.material.SetColor("_Color", Color.green);
				//TakeDamage(2);
				howMany++;
				circleCount++;
				if(circleCount == 2)
				{
					GameObject.Find("p" + player + "pointcircle").transform.localScale = new Vector3(0, 0, 0);
				}
			}
			if (howMany > 3 && gameObject.transform.position.y < -10)
			{
				other.GetComponent<HealthScript>().TakeDamage(1);
				howMany = 0;
			}
			double xdist = gameObject.transform.position.x - other.transform.position.x;
			double ydist = gameObject.transform.position.y - other.transform.position.y;
			//if (xdist < 1 && xdist > -1 && ydist > 0.5 && ydist < 1.1) TakeDamage(1);
		}

		public void TakeDamage(int amount)
		{
			MinigameController.Instance.AddScore(player, amount);
			scores[player - 1] += amount;
			other.GetComponent<HealthScript>().updateScore(player, amount);
			if (scores[0] > scores[1])
				GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(0.152f, 0.535f, 0.0625f, 0.1f);
			if (scores[1] > scores[0])
				GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(0.9f, 0.7f, 0.0625f, 0.1f);
			var colorer = other.GetComponent<Renderer>();
			colorer.material.SetColor("_Color", Color.red);
			GameObject.Find("p" + player + "pointcircle").transform.localScale = new Vector3(1, 1, 1);
			circleCount = 0;
			//playerHealth -= amount;
			//healthSlider.value = playerHealth;
		}
		public void updateScore(int player, int amount)
		{
			scores[player - 1] += amount;
		}
	}
}
