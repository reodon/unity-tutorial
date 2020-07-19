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

    public int m_nextExpBase;
    public int m_nextExpInterval;
    public int m_level;
    public int m_exp;
    public int m_prevNeedExp;
    public int m_needExp;

    public AudioClip m_levelUpClip;
    public AudioClip m_damageClip;

    private void Awake()
    {
	    m_instance = this;
	    m_hp = m_hpMax;

	    m_level = 1;
	    m_needExp = GetNeedExp( 1 );
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
	    var audioSource = FindObjectOfType<AudioSource>();
	    audioSource.PlayOneShot( m_damageClip );

	    m_hp -= damage;
	    if ( 0 < m_hp ) return;
	    gameObject.SetActive( false );
    }

    public void AddExp( int exp )
    {
	    m_exp += exp;

	    if ( m_exp < m_needExp ) return;

	    m_level++;
	    m_prevNeedExp = m_needExp;
	    m_needExp = GetNeedExp( m_level );

	    var angleBase = 0;
	    var angleRange = 360;
	    var count = 28;
	    ShootNWay( angleBase, angleRange, 0.15f, count );
	    ShootNWay( angleBase, angleRange, 0.2f, count );
	    ShootNWay( angleBase, angleRange, 0.25f, count );

	    var audioSource = FindObjectOfType<AudioSource>();
	    audioSource.PlayOneShot( m_levelUpClip );
    }

    public int GetNeedExp( int level )
    {
	    return m_nextExpBase + m_nextExpInterval * ( ( level - 1 ) * ( level - 1 ) );
    }

}
