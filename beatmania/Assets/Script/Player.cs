using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    enum KeyList
    {
        Disc,
        Key1,
        Key2,
        Key3,
        Key4,
        Key5,
        Key6,
        Key7,
        Start,
        Select,

        MAX
    }

    [SerializeField] private InputAction[] inputlist = new InputAction[(int)KeyList.MAX];
    [SerializeField] private GameObject[] enablekey = new GameObject[(int)KeyList.MAX];

    [SerializeField] private List<GameObject>[] l_totalnotes = new List<GameObject>[(int)NotesSponer.Lane.MAX];
    [SerializeField] private int[] l_ncount = new int[(int)NotesSponer.Lane.MAX];

    [SerializeField] private bool[] b_onKey = new bool[(int)KeyList.MAX];

    [SerializeField] private float f_succeszone = 0.2f;
    [SerializeField] private float f_lostzone = 0.6f;


    // 有効化
    private void OnEnable()
    {
        for(int i = 0; i < (int)KeyList.MAX; i++)
        {
            inputlist[i]?.Enable();
        }

    }

    // 無効化
    private void OnDisable()
    {
        for (int i = 0; i < (int)KeyList.MAX; i++)
        {
            inputlist[i]?.Disable();
        }
        
    }

    private void Start()
    {
        f_succeszone = 0.2f;
        f_lostzone = -0.6f;

    }

    private void Update()
    {
        Input();

    }

    void Input()
    {
        
        for (int i = 0; i < (int)KeyList.MAX; i++)
        {
            // 入力値を見る++++++++++++++++++++++++++++++++++++++++++
            float value;

            if (i == (int)KeyList.Key7) //こいつだけ例外
            {
                value = -inputlist[i].ReadValue<Vector2>().x;
            }
            else
            {
                value = inputlist[i].ReadValue<float>();
            }
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // 押されたら++++++++++++++++++++++++++++++++++++++++++++
            if (value > 0.5f || value < -0.5f) // マイナスはディスク用
            {
                enablekey[i].SetActive(true); // デバック用

                // まだキーが押されてないなら
                if (b_onKey[i] == false)
                {
                    
                    Debug.Log(i + ":" + l_ncount[i]);
                    if (l_totalnotes[i][l_ncount[i]].GetComponent<Notes>().DifferencefromLine() <= f_succeszone)
                    {
                        l_totalnotes[i][l_ncount[i]].GetComponent<Notes>().Kill();
                        l_ncount[i]++;
                    }

                    b_onKey[i] = true;
                }

            }
            else
            {
                enablekey[i].SetActive(false);
                b_onKey[i] = false;
            }

            if (i > (int)KeyList.Key7) return;


            if (l_totalnotes[i][l_ncount[i]].GetComponent<Notes>().DifferencefromLine() < f_lostzone)
            {
                Debug.Log("君消す");
                l_totalnotes[i][l_ncount[i]].GetComponent<Notes>().Kill();
                l_ncount[i]++;
            }
        }
    }

    public void SetNotes(List<GameObject>[] _list)
    {
        int[] ns = new int[(int)NotesSponer.Lane.MAX];


        for (int i = 0; i < (int)NotesSponer.Lane.MAX; i++)
        {
            l_totalnotes[i] = new List<GameObject>();
            
            for(int m = 0; m < _list[i].Count; m++)
            {
                l_totalnotes[i].Add(_list[i][m]);
            }
            
        }

        
    }
}
