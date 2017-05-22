using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour {

    [SerializeField]
    private Rigidbody rigidBody;
    public float maxForce;

    public static int diceSurfaceInfo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (rigidBody.velocity.magnitude == 0)
        {
            DecideDiceNum();
        }
    }

    /// <summary>
    /// サイコロを転がす.
    /// </summary>
    public void RollDice()
    {
        Vector3 randomDirection = new Vector3(1f, Random.Range(0f, 1f), 1f);
        rigidBody.AddForce(randomDirection * Random.Range(maxForce/2,maxForce),ForceMode.Impulse);
        rigidBody.AddTorque(randomDirection * maxForce*1.15f,ForceMode.Impulse);
    }
    /// <summary>
    /// 値が0のままならもう一度転がす.
    /// </summary>
    void DecideDiceNum()
    {
        if (diceSurfaceInfo == 0)
        {
            Debug.Log("もう一度飛ばすよ");
            RollDice();
        }else
        {
            Debug.Log("とまったよ");
        }
    }

}
