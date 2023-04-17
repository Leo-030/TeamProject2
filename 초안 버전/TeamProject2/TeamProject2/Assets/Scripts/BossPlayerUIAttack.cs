using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerUIAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        // 일단 1번만 때리기
        BossManager.instance.Attack(0);
    }
}
