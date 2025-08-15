using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    [SerializeField] private Tower Tower;
    private List<GameObject> targets = new List<GameObject>();

    void Start()
    {
        UpdateRange();
    }

    void Update()
    {
        if (targets.Count > 0)
        {
            if (Tower.first)
            {
                float miniDistance = Mathf.Infinity;
                int maxIndex = 0;
                GameObject firstTarget = null;

                foreach (GameObject target in targets)
                {
                    int index = target.GetComponent<Enemy>().index;
                    float distance = target.GetComponent<Enemy>().distance;

                    if (index > maxIndex || (index == maxIndex && distance < miniDistance))
                    {
                        maxIndex = index;
                        miniDistance = distance;
                        firstTarget = target;
                    }
                }

                Tower.target = firstTarget;
            }
            else if (Tower.last)
            {

            }
            else if (Tower.strong)
            {

            }
            else
            {
                Tower.target = targets[0];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Remove(collision.gameObject);
        }
    }

    public void UpdateRange()
    {
        transform.localScale = new Vector3(Tower.range, Tower.range, Tower.range);
    }
}
