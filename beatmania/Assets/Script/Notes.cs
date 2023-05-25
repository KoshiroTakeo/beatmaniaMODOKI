using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public struct NotesParameter
    {
        public NotesSponer.NotesType type;
        public NotesSponer.Lane lane;
        public int notesID;
        public float startposY;
        public float fbpm;
        public float fposX;
    }

    [SerializeField] private Vector3 birthpos;
    [SerializeField] private Transform hitline;
    [SerializeField] string tagname_hitline;

    //
    [SerializeField] private NotesParameter parametar;

    [SerializeField] float speed = 0.01f; // �~�����x
    [SerializeField] float cross_a_hitline = 1.0f; // �~�����x

    [SerializeField] public int n_ID;
    [SerializeField] private bool b = false;

    [SerializeField] private BPMManager nowbpm;
    [SerializeField] string tagname_manager;

    private void Start()
    {
        b = false;
    }

    private void FixedUpdate()
    {
        speed = nowbpm.NowBPM();
        //this.transform.position.y - hitline.position.y;


        this.transform.position = new Vector3(this.transform.position.x,
                                              this.transform.position.y - speed, 
                                              this.transform.position.z);



        
    }

    // �m�[�c�����Z�b�g����
    public void SetNotes(NotesParameter _parameter)
    {
        birthpos = this.transform.position;
        hitline = GameObject.FindWithTag(tagname_hitline).transform;
        n_ID = _parameter.notesID;

        nowbpm = GameObject.FindWithTag(tagname_manager).GetComponent<BPMManager>();
    }

    // ���胉�C���܂ł̋���
    public float DifferencefromLine()
    {
        float value = 0;
        b = true;
        

        value = this.transform.position.y - hitline.position.y;
        return value;
    }

    

    // ���S�ɏ���������
    public void Kill()
    {
        Debug.Log("��");
        Destroy(this.gameObject);
    }

    
}
