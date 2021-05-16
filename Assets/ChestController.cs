using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestController : MonoBehaviour
{

    private Animator animator;

    [SerializeField] private UnityEvent _startMinigame;

    private bool _triggered = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnChestFound()
    {
        StartCoroutine(OnChestFoundAsync());
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("AAAAAAA")
        if (!_triggered)
        {
            OnChestFound();
            _triggered = true;
        }
    }

    public IEnumerator OnChestFoundAsync()
    {
        animator.SetBool("found", true);
        yield return new WaitForSeconds(1f);
        _startMinigame.Invoke();
        this.enabled = false;
    }


}
