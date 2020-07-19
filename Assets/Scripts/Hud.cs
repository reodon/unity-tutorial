using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
	public Image m_hpGauge;
	public Image m_expGauge;

	public void Update()
	{
		var player = Player.m_instance;

		var hp = player.m_hp;
		var hpMax = player.m_hpMax;
		m_hpGauge.fillAmount = (float) hp / hpMax;

		var exp = player.m_exp;
		var prevNeedExp = player.m_prevNeedExp;
		var needExp = player.m_needExp;
		m_expGauge.fillAmount = (float) ( exp - prevNeedExp ) / ( needExp - prevNeedExp );
	}
}


