using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private SpriteRenderer sr;
    private float cloneTimer;
    [SerializeField] private float colorLosingSpeed;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));
        }
    }
    public void SetupClone(Transform _newTransform,float _cloneDuration)
    {
        transform.position = _newTransform.position;
        cloneTimer = _cloneDuration;
    }
}
