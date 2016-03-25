using UnityEngine;
using System.Collections;

public class Apple : MonoBehaviour {

	public static float bottomY = -10f;

	// Update is called once per frame
	void Update ()
	{
		if (transform.position.y < bottomY )
		{
			Destroy( gameObject );
		}
	}
}
