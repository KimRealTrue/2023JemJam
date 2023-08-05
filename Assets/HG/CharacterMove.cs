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

    public int spawned;

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
		if (GameSystem_Controller.instance.gameStart) {
			//CameraView();
			if (!drop)
				StartCoroutine(TrashDrop());
			if (!act)
				StartCoroutine(State());
		}
    }


	private void FixedUpdate()
	{
		CameraView();
	}

	void CameraView()
    {
        Vector3 pos = Cam.WorldToViewportPoint(transform.position);
       // Debug.Log($"WTV: {pos}");
        if (pos.x > 0.88f) pos.x = 0.88f;
        if (pos.x < 0.12f) pos.x = 0.12f;
        if (pos.y > 0.8f) pos.y = 0.8f;
        if (pos.y < 0.2f) pos.y = 0.2f;
        if (pos.z < 0f) pos.z = 0f;
        transform.position = Cam.ViewportToWorldPoint(pos);
       // Debug.Log($"VTW: {transform.position}");
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
		var v2 = DataManager.Instance.TrashDropTime;
		yield return new WaitForSeconds(Random.Range(v2.x, v2.y));
        

        int trashnum = Random.Range(0, trash.Length);
        GameObject to= Instantiate(trash[trashnum], transform.position, Quaternion.identity);
		to.GetComponent<ItemObject>().autoRemove = true;
        GameSystem_Controller.instance.spawned++;
		Audio_Controller.instance.EffectPlay_TrashDrop();
		drop = false;
    }

    IEnumerator PenguinPosition()
    {
        anim.SetBool("Walk", true);

        var v2= MovePosition();
        rigid.velocity = v2 * 0.6f;
        yield return new WaitForSeconds(3f);

        anim.SetBool("Walk", false);
        act = false;
    }
    private Vector2 MovePosition()
    {
        Vector2 movepos = box.transform.position;
        Vector2 mapsize = box.size;
        float x =Random.Range(-mapsize.x / 2f, mapsize.x / 2f);
        float y =Random.Range(-mapsize.y / 2f, mapsize.y / 2f);

        Vector2 pos = new Vector2(x, y);
        //Debug.Log(this.transform.name + "    " + pos);
        return pos;
    }
}
