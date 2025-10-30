using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] private DrinkingWater drinkingWater;
    public Animator LeafAnimator { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LeafAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drinkingWater.IsOffDrinkWater)
        {
            if (TimeStateManager.Instance.IsNormalState())
            {
                LeafAnimator.ResetTrigger("Rewinding");
                LeafAnimator.ResetTrigger("MoveTime");
            }
            else if (TimeStateManager.Instance.IsRewindingState())
            {
                LeafAnimator.SetTrigger("Rewinding");
            }
            else if (TimeStateManager.Instance.IsFastForwardState())
            {
                LeafAnimator.SetTrigger("MoveTime");
            }
        }
    }


}
