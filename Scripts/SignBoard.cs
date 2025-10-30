using System.Collections;
using UnityEngine;

public class SignBoard : MonoBehaviour
{
    [SerializeField] private GameObject signBoardUI;
    private Animator signBoardDisplayAnimator;
    [SerializeField] private GameObject signBoardCanvas;
    [SerializeField] private GameObject signBoardText;
    private float waitCloseTime = 0.5f;

    private bool isPlayerInRange = false;
    private bool isOpenSignBoard = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        signBoardDisplayAnimator = signBoardUI.GetComponent<Animator>();

        signBoardText.SetActive(false);
        signBoardDisplayAnimator.enabled = false;
        signBoardCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            if (!isOpenSignBoard)
            {
                isOpenSignBoard = true;
                signBoardDisplayAnimator.enabled = true;
                signBoardDisplayAnimator.Play("SignBoardOpen");
                return;
            }
            else
            {
                signBoardDisplayAnimator.SetTrigger("Close");
                StartCoroutine(CloseSignBoardAfterDelay(waitCloseTime));
                return;
            }
        }
    }
    private IEnumerator CloseSignBoardAfterDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        isOpenSignBoard = false;
        signBoardDisplayAnimator.enabled = false;
        yield break;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            signBoardText.SetActive(true);
            isPlayerInRange = true;
            signBoardCanvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            signBoardText.SetActive(false);
            isPlayerInRange = false;
            signBoardCanvas.SetActive(false);
        }
    }
}
