using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        HP_Bar, Now_HP_Text, Defalut_HP_Text, Option_Button, DeathCount, totalPlayTime, Respawn_Name
    }

    public InfoType type;

    Text my_Text;
    Slider my_Slider;

    public bool Awake_HUD;

    private void Awake()
    {
        my_Text = GetComponent<Text>();
        my_Slider = GetComponent<Slider>();

        if (Awake_HUD)
            Update_HUD();
    }
    public void Update_HUD()
    {
        switch(type)
        {
            case InfoType.HP_Bar:
                float curHP = Player.instance.Health.NowHealth;
                float maxHP = Player.instance.Health.DefalutHealth;

                my_Slider.value = curHP / maxHP;
                break;
            case InfoType.Now_HP_Text:
                if (Player.instance.Health.NowHealth >= 0)
                    my_Text.text = string.Format("{0:F1}", Player.instance.Health.NowHealth);
                else
                    my_Text.text = "0.0";
                break;
            case InfoType.Defalut_HP_Text:
                my_Text.text = string.Format("{0:F1}", Player.instance.Health.DefalutHealth);
                break;
            case InfoType.Option_Button:
                my_Text.text = $"{MouseText_Change(Utils.Instance.binding.Bindings[Action.Option]).ToString()+" is Option"}";
                break;
            case InfoType.DeathCount:
                my_Text.text = string.Format("Total Death : {0:}", GameManager.instance.totalDeath);
                break;
            case InfoType.totalPlayTime:
                int Min = Mathf.FloorToInt(GameManager.instance.totalPlayTime / 60);
                int Sec = Mathf.FloorToInt(GameManager.instance.totalPlayTime % 60);

                my_Text.text = string.Format("Total PlayTime : {0:D2}:{1:D2}", Min, Sec);
                break;
            case InfoType.Respawn_Name:
                my_Text.text = string.Format(GameManager.instance.rm.nowPointName);
                break;
                    
        }
    }
    private string MouseText_Change(KeyCode code)
    {
        switch (code)
        {
            case KeyCode.Mouse0:
                return $"LeftButton";
            case KeyCode.Mouse1:
                return $"RightButton";
            case KeyCode.Mouse2:
                return $"MiddleButton";
            default:
                return $"{code}";
        }
    }
}
