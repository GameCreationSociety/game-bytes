using UnityEngine;

namespace Shotpot
{
	public class TimedDestroy : MonoBehaviour
	{
		public float timeLeft = 1;
	
		// Update is called once per frame
		void Update ()
		{
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
