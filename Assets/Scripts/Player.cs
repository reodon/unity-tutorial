using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_speed;
    private void Update()
    {
        var h = Input.GetAxis( "Horizontal" );
	var v = Input.GetAxis( "Vertical" );
	var velocity = new Vector3( h, v ) * m_speed;
	transform.localPosition += velocity;
    }
}
