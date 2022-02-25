using UnityEngine;

namespace Sporshmallow
{
	public class SFX : MonoBehaviour
	{
		// Start is called before the first frame update
		public AudioSource boing;
		public AudioSource mario;
		public AudioSource stomp;
		public AudioSource die;
		public AudioSource run;
		public AudioSource bump;

		public void PlayBoing(){
			boing.Play();
		}
		public void PlayMario(){
			mario.Play();
		}
		public void PlayStomp(){
			stomp.Play();
		}
		public void PlayDie(){
			die.Play();
		}
		public void PlayRun(){
			run.Play();
		}public void StopRun(){
			run.Stop();
		}
		public void PlayBump(){
			bump.Play();
		}
	}
}
