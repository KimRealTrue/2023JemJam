using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private GameObject[] trash;
    public enum StateType { Idle, Walk, Drop }
    public StateType stateType;
    public float spawnTime;

    bool act;

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
        if(!act)
        StartCoroutine(State());
    }

    IEnumerator State()
    {
        act = true;
        int statenum = Random.Range(1, 4);

        switch (statenum)
        {
            case 1:
                stateType = StateType.Idle;
                act = false;
                yield return null;
                break;
            case 2:
                stateType = StateType.Walk;
                yield return StartCoroutine(PenguinPosition());
                break;
            case 3:
                stateType = StateType.Drop;
                yield return StartCoroutine(TrashDrop());
                break;
        }
    }
    IEnumerator TrashDrop()
    {
        int trashnum = Random.Range(0, trash.Length);
        Instantiate(trash[trashnum], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnTime);

        act = false;
    }
    IEnumerator PenguinPosition()
    {
        anim.SetBool("Walk", true);
        rigid.velocity = MovePosition();
        yield return new WaitForSeconds( 0.5f+ spawnTime);

        anim.SetBool("Walk", false);
        act = false;
    }
    private Vector2 MovePosition()
    {
        Vector2 movepos = box.transform.position;

        float x = Random.Range(-movepos.x, movepos.x);
        float y = Random.Range(-movepos.y, movepos.y);

        Vector2 move = new Vector2(x, y);
        return move;
    }
}
