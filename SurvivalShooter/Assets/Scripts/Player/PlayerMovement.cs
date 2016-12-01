using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	private Animator anim;
	private Rigidbody playerRigidbody;
	private Vector3 movement;
	private float camRayLength = 100f;
	private LayerMask floorMask;

	void Start()
	{
		playerRigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		floorMask = LayerMask.GetMask ("Floor");
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		Move (moveHorizontal, moveVertical);
		Animating (moveHorizontal, moveVertical);
		Turning ();
	}

	void Move(float moveHoritontal, float moveVertical)
	{
		movement.Set (moveHoritontal, 0f, moveVertical);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Animating(float moveHorizontal, float moveVertical)
	{
		bool walking = moveHorizontal != 0 || moveVertical != 0;
		if (walking) {
			anim.SetBool ("IsWalking", true);
		} else {
			anim.SetBool ("IsWalking", false);
		}
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit = new RaycastHit ();

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 direction = floorHit.point - transform.position;
			direction.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (direction);
			playerRigidbody.MoveRotation (newRotation);
		}
	}
}
