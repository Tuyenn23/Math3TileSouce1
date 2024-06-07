using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField] private Transform Root;
    [SerializeField] private Animator anim;
    private float time;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (time > 0)
        {
            // select random point 
            time -= Time.deltaTime;
        }
        else
        {
            MovePet();
        }
    }

    [Button]
    public void MovePet()
    {
        Vector3 rdPoint = Root.transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        transform.LookAt(rdPoint);
        StartCoroutine(MoveSmoothly(rdPoint, 2f));
        time = Random.Range(15f, 25f);
    }

    private IEnumerator MoveSmoothly(Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        anim.SetBool("WALK", true);
        Vector3 start = transform.position;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            transform.position = Vector3.Lerp(start, targetPosition, progress);
            //Debug.DrawRay(start, targetPosition - start, Color.red, Vector3.Distance(start, targetPosition));
            yield return null;
        }
        anim.SetBool("WALK", false);
    }

}
