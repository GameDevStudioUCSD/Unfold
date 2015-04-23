using UnityEngine;
using System.Collections;

public class MenuWalk : MonoBehaviour {
	public Transform startMarker;
	public Transform endMarker;
    public Transform portal;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
    private Vector3 to;
    private Vector3 from;
    private PlayerCharacter playerCharacter;
	public void DefineLerp( Transform start, Transform end) {
		startTime = Time.time;
        to = start.position;
        from = end.position;
		journeyLength = Vector3.Distance(to, from);
        foreach(Transform player in this.GetComponent<Transform>())
        {
            playerCharacter = player.GetComponent<PlayerCharacter>();
            playerCharacter.animator.SetBool("Walking", true);
        }
	}

    void Start()
    {
        DefineLerp(startMarker, endMarker);
    }
	// Update is called once per frame
	void Update () {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		   transform.position = Vector3.Lerp(to, from, fracJourney);
	}

}
