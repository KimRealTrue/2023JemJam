using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Animations;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private GameObject[] trash;
    [SerializeField] Camera Cam;
    public enum StateType { Idle, Walk }
    public StateType stateType;

    bool act;
    bool drop;

    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        CameraView();
        if (!drop)
            StartCoroutine(TrashDrop());
        if(!act)
            StartCoroutine(State());
    }

    void CameraView()
    {
        Vector3 pos = Cam.WorldToViewportPoint(transform.position);
        if (pos.x > 0.98f) pos.x = 0.98f;
        if (pos.x < 0.02f) pos.x = 0.02f;
        if (pos.y > 1f) pos.y = 1f;
        if (pos.y < 0.1f) pos.y = 0.1f;
        if (pos.z < 0f) pos.z = 0f;
        transform.position = Cam.ViewportToWorldPoint(pos);
    }
    IEnumerator State()
    {
        act = true;
        int statenum = Random.Range(0, 2);

        switch (statenum)
        {
            case 0:
                stateType = StateType.Idle;
                act = false;
                yield return new WaitForSeconds(0.4f);
                break;
            case 1:
                stateType = StateType.Walk;
                yield return StartCoroutine(PenguinPosition());
                break;
        }
    }
    IEnumerator TrashDrop()
    {
        drop = true;
        yield return new WaitForSeconds(Random.Range(2, 5));

        int trashnum = Random.Range(0, trash.Length);
        Instantiate(trash[trashnum], transform.position, Quaternion.identity);
        drop = false;
    }
    IEnumerator PenguinPosition()
    {
        anim.SetBool("Walk", true);
        rigid.velocity = Vector2.Lerp(box.transform.position, MovePosition(), 0.2f);
        yield return new WaitForSeconds(3f);

        anim.SetBool("Walk", false);
        act = false;
    }
    private Vector2 MovePosition()
    {
        Vector2 movepos = box.transform.position;
        Vector2 mapsize = box.size;
        float x = movepos.x + Random.Range(-mapsize.x / 2f, mapsize.x / 2f);
        float y = movepos.y + Random.Range(-mapsize.y / 2f, mapsize.y / 2f);

        Vector2 pos = new Vector2(x, y);
        return pos;
    }
}
