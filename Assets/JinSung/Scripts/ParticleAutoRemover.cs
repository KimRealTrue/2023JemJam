using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(AutoRemove());
    }

	IEnumerator AutoRemove()
	{
		yield return new WaitForSeconds(1.5f);

		Destroy(gameObject);
	}

}
