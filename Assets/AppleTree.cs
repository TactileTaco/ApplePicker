using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AppleTree : MonoBehaviour 
{
	public int maxDropCount;
	public GameObject applePrefab;
	public int movementWidth;
	public float avgDropTime;
	public float dropVariance;

	private GameObject[] _dropping;
	private GameObject[] _holding;
	private float _timeToDrop;
	private int _targetPosition;
	private int _maxMove;
	// Use this for initialization
	void Start ()
	{
		_maxMove = movementWidth / 2;
		_targetPosition = Random.Range(-_maxMove, _maxMove);
		
		_dropping = new GameObject[maxDropCount];
		_holding = new GameObject[maxDropCount];

		for (int i = 0; i < maxDropCount; i++)
		{
			_holding[i] = (GameObject)Instantiate(applePrefab);
		}
	}
	
	private void _TargetPositionReached()
	{
		_targetPosition = Random.Range(-_maxMove, _maxMove);
	}

	private int _NumDropping()
	{
		return maxDropCount - _holding.Length;
	}

	private int _GetNextApple(int i = 0)
	{
		GameObject apple = _dropping[i];
		while (apple == null && ++i < maxDropCount)
		{
			apple = _dropping[i];
		}
		return i;
	}

	private int _FindEmptySpace()
	{
		for (int i = 0; i < maxDropCount; ++i)
		{
			if (_dropping[i] == null)
			{
				return i;
			}
		}
		return -1;
	}

	private void _CheckDropping()
	{
		GameObject apple;
		int check = 0;
		for (int i = 0; check < _NumDropping(); ++i)
		{
			++check;
			i = _GetNextApple(i);
			apple = _dropping[i];

			if (!apple.activeSelf)
			{
				_holding[_holding.Length] = apple;
				_dropping[i] = null;
			}
		}
	}

	private void _DropApple()
	{
		int holding = _holding.Length;
		if (holding-- > 0)
		{
			Debug.Log(holding);
			Debug.Log(_dropping.Length);
			_holding[holding].transform.position = transform.position;
			_holding[holding].SetActive(true);
			_dropping[_FindEmptySpace()] = _holding[holding];
			_holding[holding] = null;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		_CheckDropping();
		if (Mathf.Abs(transform.position.x - _targetPosition) < 0.1)
		{
			_TargetPositionReached();
		}

		_timeToDrop -= Time.deltaTime;

		if (_timeToDrop <= 0)
		{
			_DropApple();
			_timeToDrop = avgDropTime + Random.Range(-dropVariance, dropVariance);
		}
	}
}
