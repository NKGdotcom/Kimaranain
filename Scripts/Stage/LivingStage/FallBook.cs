using UnityEngine;
using System.Collections;

public class FallBook : MonoBehaviour
{
    [SerializeField] private GameObject bookText;
    private bool isPlayerInRange = false;
    private bool hasFallen = false;
    private Animator bookAnimator;

    public GameObject BookBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BookBox.SetActive(false);
        bookAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange&&Input.GetKeyDown(KeyCode.E))
        {
            isPlayerInRange = false;
            hasFallen = true;
            this.GetComponent<BoxCollider>().enabled = false;
            bookText.SetActive(false);
            bookAnimator.SetBool("act1", true);
            bookAnimator.SetBool("act3", false);
            BookBox.SetActive(true);
            StartCoroutine(BoxSpawn());
        }
        if ((hasFallen)&& TimeStateManager.Instance.IsPastState() /*&& Input.GetMouseButton(0)*/)
        {
            hasFallen = false;
            this.GetComponent<BoxCollider>().enabled = true;
            bookAnimator.SetBool("act1", false);
            bookAnimator.SetBool("act3", true);
        }
    }

    private IEnumerator BoxSpawn()
    {
        yield return new WaitForSeconds(2f);
        BookBox.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TimeStateManager.Instance.IsNormalState())
            {
                bookText.SetActive(true);
                isPlayerInRange = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bookText.SetActive(false);
            isPlayerInRange = false;
        }
    }
}

