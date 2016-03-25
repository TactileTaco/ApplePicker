using UnityEngine;
using System.Collections;

public class AppleTree : MonoBehaviour 
{
	public GameObject applePrefab;

	//movement speed
	public float speed = 1f;

	//movement range
	public float leftAndRightEdge = 10f;

	//direction change probability
	public float chanceToChangeDirections = 0.1f;

	//apple drop rate
	public float secondsBetweenAppleDrops = 1f;

	private float lastDirectionChange = 0f;

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating( "DropApple", 2f, secondsBetweenAppleDrops );
	}

	void DropApple()
	{
		GameObject apple = Instantiate( applePrefab ) as GameObject;
		apple.transform.position = transform.position;
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		lastDirectionChange += Time.deltaTime;
		transform.position = pos;

		if ( pos.x < -leftAndRightEdge )
		{
			speed = Mathf.Abs(speed);
		}
		else if ( pos.x > leftAndRightEdge )
		{
			speed = -Mathf.Abs(speed);
		}
	}

	void FixedUpdate()
	{
		if ( Random.value < chanceToChangeDirections * Mathf.Clamp(lastDirectionChange, 0f, 2f) / 2 )
		{
			lastDirectionChange = 0f;
			speed *= -1;
		}
	}
}

