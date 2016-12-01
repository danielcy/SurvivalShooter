using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Transform player;
	public float smoothing = 5f;

	private Vector3 offset;

	void Start()
	{
		offset = transform.position - player.position;
	}

	void FixedUpdate()
	{
		Vector3 MoveToPosition = player.position + offset;
		transform.position = Vector3.Lerp (transform.position, MoveToPosition, smoothing * Time.deltaTime);
	}
}
