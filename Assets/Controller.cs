using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    [SerializeField]
    private GameObject leftLaser;

    [SerializeField]
    private GameObject rightLaser;

    [SerializeField]
    private GameObject beam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, 1);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            Vector3 point = mouseRay.origin + (mouseRay.direction * 100);

            GameObject left = GameObject.Instantiate(beam);
            left.transform.position = leftLaser.transform.position;

            GameObject right = GameObject.Instantiate(beam);
            right.transform.position = rightLaser.transform.position;

            if(Physics.Raycast(mouseRay,out hit, 500))
            {
                if (hit.collider.tag == "Asteroid")
                {
                    Asteroid a = hit.collider.GetComponent<Asteroid>();

                    Vector3 between = (leftLaser.transform.position - rightLaser.transform.position)/2 + rightLaser.transform.position;
                    float distance = (between - hit.point).magnitude;
                    float beamTime = (distance/left.GetComponent<LaserBeam>().Speed)/2;
                    Vector3 future = hit.point - (Vector3.forward * a.Speed * beamTime);

                    point = future;

                    Debug.DrawLine(between, future,Color.red);
                    Debug.DrawLine(between, hit.point, Color.blue);
                    //Debug.Break();
                }
            }


            left.transform.up = point - leftLaser.transform.position;
            right.transform.up = point - rightLaser.transform.position;
        }
    }
}
