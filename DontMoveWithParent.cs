using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteAlways]
public class DontMoveWithParent : MonoBehaviour
{
    Vector3 savedPosition;
    [Tooltip("When DontMoveWithParent is on, Ctrl+Z doesn't work for movement changes on t$$anonymous$$s GameObject.")]
    public bool dontMoveWithParent = false;
    bool lastDontMoveWithParent = true;
    GameplayManager GMP;
    Vector3 parentLastPos;

    private void OnEnable()
    {
        dontMoveWithParent = false;
    }
    private void OnDisable()
    {
        dontMoveWithParent = false;
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }
    private void Start()
    {
        GMP = FindObjectOfType<GameplayManager>();
    }
    private void Update()
    {
        if (GMP.StartBattle)
            dontMoveWithParent = true;

        if (transform.hasChanged && !transform.parent.hasChanged && savedPosition != transform.position)
        {
            savedPosition = transform.position;
            transform.hasChanged = false;
        }

        if (!lastDontMoveWithParent && dontMoveWithParent)
            savedPosition = transform.position;

        lastDontMoveWithParent = dontMoveWithParent;
    }

    private void LateUpdate()
    {
        if (dontMoveWithParent)
        {
            if (savedPosition == Vector3.zero)
            {
                savedPosition = transform.position;
            }

            if (transform.parent.hasChanged)
            {
                transform.position = savedPosition;
                transform.parent.hasChanged = false;
            }
        }
    }
}