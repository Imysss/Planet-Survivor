using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Stage, Exp, Level, Kill, Time, Health, Stardust, EnemyGoal, BossGoal, Exit }
    public InfoType type;

    TextMeshProUGUI myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Stage:
                myText.text = string.Format("Stage {0:D2}", GameManager.instance.stage + 1);
                break;
            case InfoType.Stardust:
                int curStardust = GameManager.instance.stardust;
                int maxStardust = GameManager.instance.maxStardust;
                mySlider.value = (float)curStardust / (float)maxStardust;
                break;
            case InfoType.EnemyGoal:
                int curKill = GameManager.instance.kill;
                int goalKill = GameManager.instance.stageGoal[GameManager.instance.stage];
                myText.text = string.Format("몬스터 처치 ({0}/{1})", curKill, goalKill);
                if(curKill >= goalKill)
                {
                    gameObject.GetComponentInParent<DOTweenAnimation>().DOPlayById("1");
                }
                break;
            case InfoType.BossGoal:
                myText.text = "보스 처치";
                if (GameManager.instance.isBossKilled)
                {
                    gameObject.GetComponentInParent<DOTweenAnimation>().DOPlayById("1");
                }
                break;
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                //Format: 각 숫자 인자값을 지정된 형태의 문자열로 만들어주는 함수
                //{0: 인자값의 문자열이 들어갈 자리, F?: 소수점 자리 지정}
                myText.text = string.Format("Lv. {0:F0}", GameManager.instance.level + 1);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Time:
                float gameTime = GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(gameTime / 60);
                int sec = Mathf.FloorToInt(gameTime % 60);
                //D?: 자리수 지정
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.player.health;
                float maxHealth = GameManager.instance.player.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;

            case InfoType.Exit:
                if (!GameManager.instance.isClear)
                    return;
                gameObject.GetComponentInParent<DOTweenAnimation>().DOPlayById("0");
                break;
        }
    }
}
