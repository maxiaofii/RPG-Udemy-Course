using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    public void CreateClone(Transform _clonePosition)
    {
        //不能这样写,因为第二个参数是父对象 会一直跟随
        //GameObject newclone = Instantiate(clonePrefab, _clonePosition.transform);
        GameObject newclone = Instantiate(clonePrefab);
        newclone.GetComponent<CloneSkillController>().SetupClone(_clonePosition, cloneDuration);
    }
}
