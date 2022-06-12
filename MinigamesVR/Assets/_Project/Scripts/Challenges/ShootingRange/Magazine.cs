using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractable), typeof(Rigidbody))]
public class Magazine : MonoBehaviour
{
    #region Vars
    
    [Range(0, 10)] 
    [SerializeField] private int bullets;
    [Range(2, 10)] 
    [SerializeField] private int timeToDespawn;

    private Rigidbody magazineRB;
    private XRBaseInteractable _interactable;
    
    #endregion 
    
    #region Unity

    void Awake()
    {
        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.selectExited.AddListener(OnSelectExit_Interactable);

        magazineRB = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.name == "Floor")
        {
            StartCoroutine(DespawnMagazine());
        }
    }
    
    #endregion
    
    #region Methods

    public bool IsEmpty() => bullets <= 0;
    
    public void ReduceBullet()
    {
        bullets--;
    }

    public void AttachMagazine(Transform parent)
    {
        transform.SetParent(parent);
        magazineRB.isKinematic = true;
    }
    
    public void DropMagazine()
    {
        transform.SetParent(null);
        magazineRB.isKinematic = false;
    }

    IEnumerator DespawnMagazine()
    {
        yield return transform.DOScale(Vector3.zero, timeToDespawn).WaitForCompletion();
        Destroy(gameObject);
    }
    
    #endregion
    
    #region Callbacks
    
    private void OnSelectExit_Interactable(SelectExitEventArgs selectExitEventArgs)
    {
        DropMagazine();
    }
    
    #endregion
}
