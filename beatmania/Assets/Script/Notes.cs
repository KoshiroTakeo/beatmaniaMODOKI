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

    [SerializeField] float speed = 0.01f; // 降下速度
    [SerializeField] float cross_a_hitline = 1.0f; // 降下速度

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

    // ノーツ情報をセットする
    public void SetNotes(NotesParameter _parameter)
    {
        birthpos = this.transform.position;
        hitline = GameObject.FindWithTag(tagname_hitline).transform;
        n_ID = _parameter.notesID;

        nowbpm = GameObject.FindWithTag(tagname_manager).GetComponent<BPMManager>();
    }

    // 判定ラインまでの距離
    public float DifferencefromLine()
    {
        float value = 0;
        b = true;
        

        value = this.transform.position.y - hitline.position.y;
        return value;
    }

    

    // 完全に消去させる
    public void Kill()
    {
        Debug.Log("死");
        Destroy(this.gameObject);
    }

    
}
