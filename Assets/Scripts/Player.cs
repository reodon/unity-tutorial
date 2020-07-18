using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player m_instance;
    
    public float m_speed;

    public Shot m_shotPrefab;
    public float m_shotSpeed;
    public float m_shotAngleRange;
    public float m_shotTimer;
    public int m_shotCount;
    public float m_shotInterval;

    public int m_hpMax;
    public int m_hp;

    public float m_magnetDistance;

    private void Awake()
    {
	    m_instance = this;
	    m_hp = m_hpMax;
    }

    private void Update()
    {
        var h = Input.GetAxis( "Horizontal" );
	var v = Input.GetAxis( "Vertical" );
	var velocity = new Vector3( h, v ) * m_speed;
	transform.localPosition += velocity;
	transform.localPosition = Utils.ClamPosition( transform.localPosition );

	var screenPos = Camera.main.WorldToScreenPoint( transform.position );
	var direction = Input.mousePosition - screenPos;
	var angle = Utils.GetAngle( Vector3.zero, direction );
	var angles = transform.localEulerAngles;
	angles.z = angle - 90;
	//angles.z = angle;
	//transform.localEulerAngles.z = Utils.GetAngle( screenPos, Input.mousePosition ) - 90;
	transform.localEulerAngles = angles;

	m_shotTimer += Time.deltaTime;
	if ( m_shotTimer < m_shotInterval ) return;
	m_shotTimer = 0;

	ShootNWay( angle, m_shotAngleRange, m_shotSpeed, m_shotCount );

    }

    private void ShootNWay( float angleBase, float angleRange, float speed, int count )
    {
	    var pos = transform.localPosition;
	    var rot = transform.localRotation;

	    if ( 1 < count )
	    {
		    for ( int i = 0; i < count; i++ )
		    {
			    var angle = angleBase + angleRange * ( (float)i / ( count - 1 ) - 0.5f );
			    var shot = Instantiate( m_shotPrefab, pos, rot );
			    shot.Init( angle, speed );
		    }
	    }
	    else if ( count == 1 )
	    {
		    var shot = Instantiate( m_shotPrefab, pos, rot );
		    shot.Init( angleBase, speed );
	    }
    }

    public void Damage( int damage )
    {
	    m_hp -= damage;
	    if ( 0 < m_hp ) return;
	    gameObject.SetActive( false );
    }

}
