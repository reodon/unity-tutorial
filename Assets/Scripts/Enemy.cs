using UnityEngine;

public enum RESPAWN_TYPE
{
	UP,
	RIGHT,
	DOWN,
	LEFT,
	SIZEOF,
}

public class Enemy : MonoBehaviour
{
	public Vector2 m_respawnPosInside;
	public Vector2 m_respawnPosOutside;
	public float m_speed;
	public int m_hpMax;
	public int m_exp;
	public int m_damage;

	private int m_hp;
	private Vector3 m_direction;

	private void Start()
	{
		m_hp = m_hpMax;
	}

	private void Update()
	{
		transform.localPosition += m_direction * m_speed;
	}

	public void Init( RESPAWN_TYPE respawnType )
	{
		var pos = Vector3.zero;

		switch( respawnType )
		{
			case RESPAWN_TYPE.UP:
				pos.x = Random.Range( -m_respawnPosInside.x, m_respawnPosInside.x );
				pos.y = m_respawnPosOutside.y;
				m_direction = Vector2.down;
				break;

			case RESPAWN_TYPE.RIGHT:
				pos.x = m_respawnPosOutside.x;
				pos.y = Random.Range( -m_respawnPosInside.y, m_respawnPosInside.y );
				m_direction = Vector2.left;
				break;

			case RESPAWN_TYPE.DOWN:
				pos.x = Random.Range( -m_respawnPosInside.x, m_respawnPosInside.x );
				pos.y = m_respawnPosOutside.y;
				m_direction = Vector2.up;
				break;

			case RESPAWN_TYPE.LEFT:
				pos.x = m_respawnPosOutside.x;
				pos.y = Random.Range( -m_respawnPosInside.y, m_respawnPosInside.y );
				m_direction = Vector2.right;
				break;
		}

		transform.localPosition = pos;
	}
}



	
