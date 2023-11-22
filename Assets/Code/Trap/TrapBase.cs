using UnityEngine;

public class TrapBase
{
    public int id;
    public string name;
    public int damage;
    public string filename;    public GameObject trapPosition;
#nullable enable
    public GameObject? switchPosition;
#nullable disable
    GameObject trapObject;
    GameObject switchObject;
    bool isActive = false;
    public bool isReady = true;
    bool makeDamage = false;
    float lastDamage = 0f;
    LevelBaseLoader levelLoader;
    Animator animator;

    public void Tick()
    {
        if (makeDamage && isActive && (lastDamage + 1f) <= Time.time)
        {
            GameManager.Player.TakeDamage(damage);
            lastDamage = Time.time;
        }
    }

    #region <<< Loaders >>>

    public void InitTrap()
    {
        LoadTrap();
        LoadColliders();
    }

    public void SetIsReady(bool isReady)
    {
        this.isReady = isReady;
    }

    public void LoadTrap()
    {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelBaseLoader>();
        GameObject prefab = Resources.Load<GameObject>(filename);
        trapObject = levelLoader.PrefabInstantiate(prefab);
        trapObject.transform.position = trapPosition.transform.position;
        switchObject = trapObject.transform.Find("Switch").gameObject;
        if (switchPosition)
        {
            switchObject.transform.position = switchPosition.transform.position;
            TrapSwitch trapSwitch = switchObject.AddComponent<TrapSwitch>();
            trapSwitch.SetActive(isReady);
            trapSwitch.SetTrap(this);
        }
        else GameObject.Destroy(switchObject);
        animator = trapObject.GetComponent<Animator>();
        animator.SetBool("isActive", false);
    }

    private void LoadColliders()
    {
        GameObject triggerCollider = trapObject.transform.Find("Trigger").gameObject;
        UtilColliderEvents triggerTrapCollider = triggerCollider.AddComponent<UtilColliderEvents>();
        triggerTrapCollider.OnColliderEnterEvent += HandleColliderTriggerEnter;
        triggerTrapCollider.OnColliderExitEvent += HandleColliderTriggerExit;

        GameObject areaCollider = trapObject.transform.Find("Area").gameObject;
        UtilColliderEvents areaTrapCollider = areaCollider.AddComponent<UtilColliderEvents>();
        areaTrapCollider.OnColliderEnterEvent += HandleColliderAreaEnter;
        areaTrapCollider.OnColliderExitEvent += HandleColliderAreaExit;
        areaTrapCollider.OnColliderStayEvent += HandleColliderAreaStay;
    }



    #endregion

    #region  <<< Helpers >>>
    private void HandleColliderAreaEnter(GameObject gameObject, Collider2D collider)
    {
        if (collider.gameObject.name != "Hitbox") return;
    }

    private void HandleColliderAreaStay(GameObject gameObject, Collider2D collider)
    {
        if (collider.gameObject.name != "Hitbox") return;
        if (!isActive) return;
        makeDamage = true;
    }

    private void HandleColliderAreaExit(GameObject gameObject, Collider2D collider)
    {
        if (collider.gameObject.name != "Hitbox") return;
        makeDamage = false;
    }

    private void HandleColliderTriggerEnter(GameObject gameObject, Collider2D collider)
    {
        if (collider.gameObject.name != "Hitbox") return;
        if (!isReady) return;
        isActive = true;
        animator.SetBool("isActive", true);
    }

    private void HandleColliderTriggerExit(GameObject gameObject, Collider2D collider)
    {
        if (collider.gameObject.name != "Hitbox") return;
        isActive = false;
        animator.SetBool("isActive", false);
        lastDamage = 0;
    }

    #endregion
}
