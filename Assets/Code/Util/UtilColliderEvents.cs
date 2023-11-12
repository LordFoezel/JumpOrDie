using UnityEngine;
using System;

public class UtilColliderEvents : MonoBehaviour
{
    public event Action<GameObject, Collision2D> OnCollisionStayEvent;
    public event Action<GameObject, Collision2D> OnCollisionEnterEvent;
    public event Action<GameObject, Collision2D> OnCollisionExitEvent;
    public event Action<GameObject, Collider2D> OnColliderStayEvent;
    public event Action<GameObject, Collider2D> OnColliderEnterEvent;
    public event Action<GameObject, Collider2D> OnColliderExitEvent;

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionStayEvent?.Invoke(gameObject, collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnterEvent?.Invoke(gameObject, collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExitEvent?.Invoke(gameObject, collision);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        OnColliderStayEvent?.Invoke(gameObject, collider);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnColliderEnterEvent?.Invoke(gameObject, collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        OnColliderExitEvent?.Invoke(gameObject, collider);
    }
}
