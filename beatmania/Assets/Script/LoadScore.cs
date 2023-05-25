using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour
{
    [SerializeField] private NotesSponer sponer;
    [SerializeField] private BPMManager bpmmanager;

    [SerializeField] private string s_loadfilepath;

    [SerializeField] private string s_title;
    [SerializeField] private float[] f_bpm = new float[1]; // bpm�ω��p
    [SerializeField] private float f_offset_notes;
    [SerializeField] private float f_offset_music;

    [SerializeField] public int totalnotes;
    [SerializeField] private int notescount = 0;

    int c;



    private void Start()
    {
        c = 0;

        sponer = this.gameObject.GetComponent<NotesSponer>();
        bpmmanager = this.gameObject.GetComponent<BPMManager>();


        LoadFile();
    }



    void LoadFile()
    {
        JsonNode json;
        string data = Resources.Load<TextAsset>("Charts\\" + s_loadfilepath).ToString();

        json = JsonNode.Parse(data);
        s_title = json["TITLE"].Get<string>();
        f_bpm[0] = float.Parse(json["BPM"].Get<string>());
        bpmmanager.SetBPM(f_bpm[0]);
        f_offset_notes = float.Parse(json["OFFSET"].Get<string>());
        f_offset_music = float.Parse(json["STARTMUSIC"].Get<string>());

        string bardata;
        char character;
        int bar_in_notes;
        int barcount;
        int linecount = 0;
        float notesdistance;

        int c= 0;

        List<Notes.NotesParameter> l_notes = new List<Notes.NotesParameter>();
        
        foreach (var note in json["SCORE"]) //"SCORE"(����)�ɑ����z����m�F����                           
        {

            // �P���߂ɗ������������߂Ă���
            barcount = int.Parse(note["BAR"].Get<string>()); //���ߔԍ�
            bardata = note["NOTE"].Get<string>();         //�����ԍ�                      
            bar_in_notes = bardata.Length;                //1���ߓ��̒l����

            // �u,�v�𐔂��ăm�[�c�Ԋu�����߂�++++++++++++++++++++++++++++
            for (int i = 0; i < bar_in_notes; i++)
            {
                character = bardata[i];

                if (character == ',')
                {
                    linecount++;
                }
            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // �m�[�c��o�^+++++++++++++++++++++++++++++++++++++++++++++++
            for (int i = 0, n = 1; i < bar_in_notes; i++)
            {
                notesdistance = (barcount + (n / (float)linecount));
                notesdistance = notesdistance * f_bpm[0];

                character = bardata[i];

                if (character == '0')
                {
                    l_notes.Add(NotesRegister(NotesSponer.NotesType.Disc,
                                              NotesSponer.Lane.Disc, 
                                              notesdistance, 
                                              notescount++));

                }
                else if (character == '1')
                {
                    l_notes.Add(NotesRegister(NotesSponer.NotesType.White, 
                                              NotesSponer.Lane.Key1, 
                                              notesdistance, 
                                              notescount++));

                }
                else if (character == '2')
                {
                    l_notes.Add(NotesRegister(NotesSponer.NotesType.Black,
                                              NotesSponer.Lane.Key2, 
                                              notesdistance, 
                                              notescount++));

                }
                else if (character == '3')
                {
                    l_notes.Add(NotesRegister(NotesSponer.NotesType.White, 
                                              NotesSponer.Lane.Key3, 
                                              notesdistance, 
                                              notescount++));

                }
                else if (character == '4')
                {
                    l_notes.Add(NotesRegister(NotesSponer.NotesType.Black, 
                                              NotesSponer.Lane.Key4, 
                                              notesdistance,
                                              notescount++));


                }
                else if (character == '5')
                {
                    l_notes.Add(NotesRegister(NotesSponer.NotesType.White, 
                                              NotesSponer.Lane.Key5, 
                                              notesdistance, 
                                              notescount++));

                }
                else if (character == '6')
                {
                    l_notes.Add(NotesRegister(NotesSponer.NotesType.Black, 
                                              NotesSponer.Lane.Key6, 
                                              notesdistance, 
                                              notescount++));

                }
                else if (character == '7')
                {
                    l_notes.Add(NotesRegister(NotesSponer.NotesType.White, 
                                              NotesSponer.Lane.Key7, 
                                              notesdistance, 
                                              notescount++));

                }
                else if (character == ',')
                {
                    n++;

                }
            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        }

        // �m�[�c�o��
        sponer.SetNotesList(l_notes);
    }

    // 
    Notes.NotesParameter NotesRegister(NotesSponer.NotesType _t, NotesSponer.Lane _l, float _dis, int _id)
    {
        Notes.NotesParameter parameter = new();

        parameter.type = _t;
        parameter.lane = _l;
        parameter.startposY = _dis;
        parameter.notesID = _id;

        return parameter;
    }
}

// ���ʃ��[��
/* �uBAR�v�łP����
 * �uNOTE�v�̌�ɓo�ꂳ����m�[�c
 * �m�[�c�́u12345678�v�Ɓuabcdefgh�v�A�����͒P���m�[�c�p���̓`���[�W�m�[�c
 * �u,�v��؂�Ŕ��q
 * 
 * 
 * 
 * 
 * 
 */
