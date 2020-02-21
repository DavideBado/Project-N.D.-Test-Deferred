using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesDirection : MonoBehaviour
{
    public List<Image> Icons = new List<Image>();
    PlayerMovController _player;
    bool[] areaChecked;

    bool setupDone = false;
    public Color ResearchColor, PursueColor;

    float constArea;


    private void OnEnable()
    {
        GameManager.instance.CheckEnemiesStateNPosition += UpdateUI;
        GameManager.instance.OnExePhaseAction += Setup;
    }

    private void OnDisable()
    {
        GameManager.instance.CheckEnemiesStateNPosition -= UpdateUI;
        GameManager.instance.OnExePhaseAction -= Setup;
    }

    private void Setup()
    {
        _player = GameManager.instance.Player;
        if (Icons.Count > 0)
        {
            areaChecked = new bool[Icons.Count];
            constArea = 360 / Icons.Count;
            ResetIcons();
        }

        setupDone = true;
    }
    private void Update()
    {
        if (setupDone) UpdateUI();
    }

    private void UpdateUI()
    {
        ResetIcons();
        CheckEnemiesInPursue();
        CheckEnemiesInResearch();
    }

    private void CheckEnemiesInPursue()
    {
        List<EnemyNavController> _enemies = GameManager.instance.EnemiesInPursue;
        for (int i = 0; i < _enemies.Count; i++)
        {
            Vector3 dir = _enemies[i].transform.position - _player.movementTransform.position;
            float _angle = Mathf.Abs(Vector3.Angle(dir, _player.movementTransform.forward) + AngleDir(_player.movementTransform.forward, dir, _player.movementTransform.up));
            int index = (int) (_angle / constArea);
            Icons[index].enabled = true;
            Icons[index].color = PursueColor;
            areaChecked[index] = true;
           // Debug.Log("Angle: " + _angle + ", Index: " + index);
        }
    }

    private void CheckEnemiesInResearch()
    {
        List<EnemyNavController> _enemies = GameManager.instance.EnemiesInResearch;      
        for (int i = 0; i < _enemies.Count; i++)
        {
            Vector3 dir = _enemies[i].transform.position - _player.movementTransform.position;
            float _angle = Mathf.Abs(Vector3.Angle(dir, _player.movementTransform.forward) + AngleDir(_player.movementTransform.forward, dir, _player.movementTransform.up));
            int index = (int)(_angle / constArea);
            if (!areaChecked[index])
            {
                Icons[index].enabled = true;
                Icons[index].color = ResearchColor;
               // Debug.Log("Angle: " + _angle + ", Index: " + index);
            }
        }
    }

    private void ResetIcons()
    {
        for (int i = 0; i < areaChecked.Length; i++)
        {
            areaChecked[i] = false;
        }

        for (int i = 0; i < Icons.Count; i++)
        {
            Icons[i].enabled = false;
        }
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 right = Vector3.Cross(up, fwd);        // right vector
        float dir = Vector3.Dot(right, targetDir);

        if (dir < 0f)
        {
            return -360;
        }
        else
        {
            return 0f;
        }
    }
}

