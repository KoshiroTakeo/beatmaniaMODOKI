using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesSponer : MonoBehaviour
{
    public enum Lane
    {
        Disc,
        Key1,
        Key2,
        Key3,
        Key4,
        Key5,
        Key6,
        Key7,

        MAX
    }

    public enum NotesType
    {
        Disc,
        White,
        Black,

        MAX
    }

    [SerializeField] private Player player; // ˆá–@“o˜^’†
    [SerializeField] private GameObject[] notesobj = new GameObject[(int)NotesType.MAX];
    [SerializeField] private Transform[] sponepos = new Transform[(int)Lane.MAX];



    public void SetNotesList(List<Notes.NotesParameter> _notes)
    {
        List<Notes.NotesParameter>[] L_all_the_notes = new List<Notes.NotesParameter>[(int)Lane.MAX]; 
        List<GameObject>[] L_notes_per_lane = new List<GameObject>[(int)Lane.MAX];
        int n = 0;


        for (int i = 0; i < (int)Lane.MAX; i++)
        {
            L_all_the_notes[i] = new List<Notes.NotesParameter>();
            L_notes_per_lane[i] = new List<GameObject>();
        }

        for(int i = 0; i < _notes.Count; i++)
        {
            n = (int)_notes[i].lane;

            switch (_notes[i].lane)
            {
                case Lane.Disc:
                    L_all_the_notes[n].Add(_notes[i]);
                    break;

                case Lane.Key1:
                    L_all_the_notes[n].Add(_notes[i]);
                    break;

                case Lane.Key2:
                    L_all_the_notes[n].Add(_notes[i]);
                    break;

                case Lane.Key3:
                    L_all_the_notes[n].Add(_notes[i]);
                    break;

                case Lane.Key4:
                    L_all_the_notes[n].Add(_notes[i]);
                    break;
                    
                case Lane.Key5:
                    L_all_the_notes[n].Add(_notes[i]);
                    break;

                case Lane.Key6:
                    L_all_the_notes[n].Add(_notes[i]);
                    break;

                case Lane.Key7:
                    L_all_the_notes[n].Add(_notes[i]);
                    break;
            }

            L_notes_per_lane[n].Add(SponNotes(_notes[i]));
            //Debug.Log(i);

           
        }

        player.SetNotes(L_notes_per_lane);
    }

    public GameObject SponNotes(Notes.NotesParameter _notes)
    {
        GameObject obj = notesobj[(int)_notes.type];
        
        obj.GetComponent<Notes>().SetNotes(_notes);

        Vector3 pos = new Vector3(sponepos[(int)_notes.lane].position.x,
                                  sponepos[(int)_notes.lane].position.y + _notes.startposY,
                                  sponepos[(int)_notes.lane].position.z);

        obj = Instantiate(obj, pos, Quaternion.identity);



        return obj;
    }
}
