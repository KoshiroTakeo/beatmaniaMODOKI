using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    [SerializeField] private List<Notes.NotesParameter> l_notes = new List<Notes.NotesParameter>();

    public void SetNotesList(List<Notes.NotesParameter> _notes)
    {
        l_notes = _notes;
        Debug.Log(_notes.Count + " / " + l_notes[_notes.Count - 1].type);
    }
}
