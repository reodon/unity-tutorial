using UnityEngine;

public class Gem : MonoBehaviour
{
	public int m_exp;
	public float m_brake = 0.9f;
	
	private Vector3 m_direction;
	private float m_speed;

	private void Update()
	{
		var velocity = m_direction * m_speed;
		transform.localPosition += velocity;

		m_speed *= m_brake;

		transform.localPosition = Utils.ClamPosition( transform.localPosition );
	}

	public void Init( int score, float speedMin, float speedMax )
	{
		var angle = Random.Range( 0, 360 );
		var f = angle * Mathf.Deg2Rad;
		m_direction = new Vector3( Mathf.Cos( f ), Mathf.Sin( f ), 0 );
		m_speed = Mathf.Lerp( speedMin, speedMax, Random.value );

		Destroy( gameObject, 8 );
	}
}


