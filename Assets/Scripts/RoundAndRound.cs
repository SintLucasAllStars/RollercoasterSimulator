using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundAndRound : MonoBehaviour {

	public float radius;
	public float speed;
	public float wobbleAmm;
	public float petals;
	public float yp;
	float stepNum = 0;
	private float angle;
	public GameObject trail;
	Vector3 initCamPos;
	Quaternion initCamRotation;

	// Use this for initialization
	void Start () {
		wobbleAmm = radius/2;
		initCamPos = Camera.main.transform.position;
		initCamRotation = Camera.main.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.G))
		{
			generateRollerCoaster();
		}

		if(Input.GetKey(KeyCode.R))
		{
			Camera.main.transform.position = getPointStep(stepNum, petals, yp, wobbleAmm);
			stepNum++;
			Camera.main.transform.LookAt(getPointStep(stepNum, petals, yp, wobbleAmm));
		}
		else
		{
			Camera.main.transform.position = new Vector3(
				Mathf.Cos(Time.time/5)*initCamPos.magnitude,
				Camera.main.transform.position.y,
				Mathf.Sin(Time.time/5)*initCamPos.magnitude);
			Camera.main.transform.LookAt(Vector3.zero);
		}

	}

	public void generateRollerCoaster()
	{
		Camera.main.transform.position = initCamPos;
		Camera.main.transform.rotation = initCamRotation;
		GameObject [] parts = GameObject.FindGameObjectsWithTag("Part");
		for(int j=0; j<parts.Length; j++)
		{
			Destroy(parts[j]);
		}

		petals = Mathf.Round(Random.Range(1f, 10f));
		yp = Mathf.Round(Random.Range(4f, 8f));
		wobbleAmm = Random.Range(radius/2, radius/4);

		for(float i=0; i<2*System.Math.PI; i+=0.5f/25)
		{
			GameObject p = Instantiate(trail, getPointAngle(i, petals, yp, wobbleAmm), Quaternion.identity);
			p.transform.LookAt(getPointAngle(i+0.5f/25, petals, yp, wobbleAmm));

		}
	}

	public Vector3 getPointStep(float step, float phase, float yPhase, float wobbleAmount)
	{
		float alpha = step * (speed/100);
		float wobbleradius = Mathf.Cos(alpha*phase)*wobbleAmount;
		return new Vector3(
			Mathf.Cos(alpha)* (radius + wobbleradius),
			Mathf.Cos(alpha*yPhase)* (radius + wobbleradius),
			Mathf.Sin(alpha)*(radius + wobbleradius)
		);
	}

	public Vector3 getPointAngle(float alpha, float phase, float yPhase, float wobbleAmount)
	{
		float wobbleradius = Mathf.Cos(alpha*phase)*wobbleAmount;
		return new Vector3(
			Mathf.Cos(alpha)* (radius + wobbleradius),
			Mathf.Cos(alpha*yPhase)* (radius + wobbleradius),
			Mathf.Sin(alpha)*(radius + wobbleradius)
		);
	}
}
