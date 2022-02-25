using UnityEngine;

namespace Sporshmallow
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Just for debugging, adds some velocity during OnEnable")]
		private Vector2 initialVelocity;

		[SerializeField]
		private float minVelocity = 10f;

		private Vector2 lastFrameVelocity;
		private Rigidbody2D rb;

		GameObject p1;
		GameObject p2;

		private void OnEnable()
		{
			rb = GetComponent<Rigidbody2D>();
			rb.velocity = initialVelocity;
		}

		private void Update()
		{
			lastFrameVelocity = rb.velocity;

			double xdist = gameObject.transform.position.x - p1.transform.position.x;
			double ydist = gameObject.transform.position.y - p1.transform.position.y;
			double p1_height = p1.GetComponent<MoveScript>().playerHeight;
			double p1_width = p1.GetComponent<MoveScript>().playerWidth;
		
			if (xdist > -p1_width/2 && xdist < p1_width/2 && ydist < p1_height/2 && ydist > -p1_height/2)
			{
				if (!p1.GetComponent<ActionScript>().defenseOn)
				{
					p2.GetComponent<HealthScript>().TakeDamage(1);
					p1.GetComponent<MoveScript>().vel.x = -(float)(p1.GetComponent<MoveScript>().vel.x - rb.velocity.x);
				}
				else
				{
					p1.GetComponent<ActionScript>().shieldHits++;
					p1.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
					if (p1.GetComponent<ActionScript>().shieldHits >= 3)
					{
						p1.GetComponent<ActionScript>().shield.transform.localScale = new Vector3(0, 0, 0);
						p1.GetComponent<ActionScript>().defenseOn = false;
						p1.GetComponent<ActionScript>().shieldHits = 0;
						p1.GetComponent<ActionScript>().defCooldown = 0;
					}
				}
				Destroy(gameObject);

			}
			xdist = gameObject.transform.position.x - p2.transform.position.x;
			ydist = gameObject.transform.position.y - p2.transform.position.y;
			double p2_width = p2.GetComponent<MoveScript>().playerHeight;
			double p2_height = p2.GetComponent<MoveScript>().playerWidth;
		
			if (xdist > -p2_width/2 && xdist < p2_width/2 && ydist < p2_height/2 && ydist > -p2_height/2)
			{
				if (!p2.GetComponent<ActionScript>().defenseOn)
				{
					p1.GetComponent<HealthScript>().TakeDamage(1);
					p2.GetComponent<MoveScript>().vel.x = -(float)(p2.GetComponent<MoveScript>().vel.x - rb.velocity.x);
				}
				else
				{
					p2.GetComponent<ActionScript>().shieldHits++;
					p2.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
					if (p2.GetComponent<ActionScript>().shieldHits >= 3)
					{
						p2.GetComponent<ActionScript>().shield.transform.localScale = new Vector3(0, 0, 0);
						p2.GetComponent<ActionScript>().defenseOn = false;
						p2.GetComponent<ActionScript>().shieldHits = 0;
						p2.GetComponent<ActionScript>().defCooldown = 0;
					}
				}
				Destroy(gameObject);
			}
		}
		private void Start()
		{
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
			gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
			p1 = GameObject.Find("Player1");
			p2 = GameObject.Find("Player2");
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			Bounce(collision.contacts[0].normal);
		}

		private void Bounce(Vector2 collisionNormal)
		{
			var speed = lastFrameVelocity.magnitude;
			var direction = Vector2.Reflect(lastFrameVelocity.normalized, collisionNormal);

			Debug.Log("Out Direction: " + direction);
			rb.velocity = direction * Mathf.Max(speed, minVelocity);
		}

		//End of Collision Stuff
	
		void OnBecameInvisible()
		{
			Destroy(gameObject);
		}

	}
}
