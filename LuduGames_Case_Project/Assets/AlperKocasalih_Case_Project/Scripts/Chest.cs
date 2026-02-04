using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableBase
{
    [Header("Chest Settings")]
    [SerializeField] private GameObject m_ChestLid;
    [SerializeField] private bool m_IsOpen = false;
    [SerializeField] private bool m_IsLocked = true;
    [SerializeField] private string m_LockedText = "Chest is Locked (Key Required)";
    [SerializeField] private string m_UnlockedText = "Press and hold [E] to Open Chest";
    [SerializeField] private float m_UnlockDuration = 2f;
    [SerializeField] private float m_OpenDuration = 0f;
    [SerializeField] private float m_Speed = 2f;
    [SerializeField] private float m_OpenAngle = 180f;

    private Quaternion m_ClosedRotation;
    private Quaternion m_OpenRotation;

    public override float InteractionDuration 
    {
        get 
        {
            if (m_IsLocked)
            {
                return GameState.ChestKey ? m_UnlockDuration : m_OpenDuration;
            }
            return m_OpenDuration;
        }
    } 
    public override string GetInteractionText() 
    {
        if (m_IsLocked)
        {
            return GameState.ChestKey ? m_UnlockedText: m_LockedText;
        }
        return m_IsOpen ?  "Press [E] to Close Chest" :  "Press [E] to Open Chest";
    }
    public override void Interact()
    {
        if (m_IsLocked)
        {
            if (GameState.ChestKey)
            {
                Debug.Log("Unlocking the chest...");
                m_IsLocked = false; // Kilidi aç
                OpenChest(); // Ve sandığı hareket ettir
            }
            else
            {
                Debug.Log("Locked! Cannot open.");
                // Burada bir 'tık tık' sesi çalabilirsin
            }
            return; // Kilitliyse aşağıya geçme
        }
        Debug.Log("Chest opened!");
        OpenChest();
    }

    private void Start()
    {
        if (m_ChestLid != null)
        {
            m_ClosedRotation = m_ChestLid.transform.localRotation;
            m_OpenRotation = Quaternion.Euler(m_OpenAngle, 0f, 0f);
        }
    }
    private void OpenChest()
    {
        StopAllCoroutines();
        m_IsOpen = !m_IsOpen;
        Quaternion target = m_IsOpen ? m_OpenRotation : m_ClosedRotation;
        StartCoroutine(RotateChest(target));
    }
    public void LockChest()
    {
        StopAllCoroutines();
        m_IsLocked = true;
        m_IsOpen = false;
        Quaternion target = m_IsOpen ? m_OpenRotation : m_ClosedRotation;
        GameState.ChestKey = false;
        StartCoroutine(RotateChest(target));
    }
    private IEnumerator RotateChest(Quaternion targetRotation)
    {
        while (Quaternion.Angle(m_ChestLid.transform.rotation, targetRotation) > 0.1f)
        {
            m_ChestLid.transform.rotation = Quaternion.Slerp(m_ChestLid.transform.rotation, targetRotation, Time.deltaTime * m_Speed);
            yield return null;
        }
        m_ChestLid.transform.rotation = targetRotation;
    }
}
