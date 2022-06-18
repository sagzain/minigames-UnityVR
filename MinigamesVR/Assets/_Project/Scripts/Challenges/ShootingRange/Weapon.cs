using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public struct WeaponAudios
{
    public AudioClip shootSound;
    public AudioClip emptySound;
    public AudioClip reloadSound;
}

[RequireComponent(typeof(AudioSource), typeof(XRBaseInteractable))]
public class Weapon : MonoBehaviour
{
    #region Vars
    
    [SerializeField] private Transform output;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private XRSocketInteractor ammoSocket;
    
    [Header("Sounds")] 
    [SerializeField] private WeaponAudios audios;

    private bool _canShoot;
    private AudioSource _audioSource;
    private Magazine _currentMagazine;
    private XRBaseInteractable _interactable;
    
    #endregion 
    
    #region Unity

    void Awake()
    {
        _canShoot = true;
        _audioSource = GetComponent<AudioSource>();

        try { _currentMagazine = ammoSocket.firstInteractableSelected.transform.GetComponent<Magazine>(); }
        catch { Debug.Log($"[{gameObject.name}] Socket has no magazine."); }
        
        ammoSocket.selectEntered.AddListener(OnSelectEnter_SocketInteractor);
        ammoSocket.selectExited.AddListener(OnSelectExit_SocketInteractor);

        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.activated.AddListener(OnActivate_Interactable);
    }
    
    #endregion
    
    #region Methods

    public void ShootWeapon()
    {
        _canShoot = _currentMagazine != null && !_currentMagazine.IsEmpty();
        
        if (!_canShoot)
        {
            PlaySound(audios.emptySound);
            return;
        }

        PlaySound(audios.shootSound);
        _currentMagazine.ReduceBullet();
        Instantiate(shotPrefab, output.position, output.rotation);
    }


    private void PlaySound(AudioClip clip)
    {
        if(clip)
            _audioSource.PlayOneShot(clip);
    }

    private void AddMagazine(Magazine magazine)
    {
        _currentMagazine = magazine;
        _currentMagazine.AttachMagazine(transform);
        
        PlaySound(audios.reloadSound);
    }

    private void RemoveMagazine()
    {
        _currentMagazine.DropMagazine();
        _currentMagazine = null;
        
        PlaySound(audios.reloadSound);
    }

    #endregion

    #region Callbacks

    private void OnSelectEnter_SocketInteractor(SelectEnterEventArgs selectEnterEventArgs)
    {
        AddMagazine(selectEnterEventArgs.interactableObject.transform.GetComponent<Magazine>());
    }

    private void OnSelectExit_SocketInteractor(SelectExitEventArgs selectExitEventArgs)
    {
        RemoveMagazine();
    }

    private void OnActivate_Interactable(ActivateEventArgs activateEventArgs)
    {
        ShootWeapon();
    }

    #endregion
}
