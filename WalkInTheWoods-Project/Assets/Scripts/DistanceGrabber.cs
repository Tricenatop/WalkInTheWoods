using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DistanceGrabber : XRBaseControllerInteractor

{
    List<XRBaseInteractable> m_ValidTargets = new List<XRBaseInteractable>();
    XRBaseInteractable m_CurrentNearestObject;

    public float m_grabbingThreshold = .8f;
    public GameObject m_cursor;
    public Transform m_fwdVector;

    private List<XRBaseInteractable> m_grabbableItems;
    private SphereCollider m_coll;

    // Start is called before the first frame update
    void Start()
    {
        if (!m_coll)
        {
            m_coll = gameObject.AddComponent<SphereCollider>();

        }
        m_coll.radius = .1f;
        m_coll.isTrigger = true;

        m_cursor = Instantiate(m_cursor);
        m_cursor.SetActive(false);

        m_grabbableItems = FindObjectsOfType<XRBaseInteractable>().ToList();
    }
    
    protected override List<XRBaseInteractable> ValidTargets { get { return m_ValidTargets; } }

    public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractor(updatePhase);

        GetValidTargets(m_ValidTargets);
    }

    public override void GetValidTargets(List<XRBaseInteractable> validTargets)
    {
        validTargets.Clear();

        float bestGuess = 0;
        XRBaseInteractable selectable = null;
        foreach (XRBaseInteractable obj in m_grabbableItems)
        {
            Vector3 dir = (obj.transform.position - m_fwdVector.position).normalized;
            float currentGuess = Vector3.Dot(m_fwdVector.forward, dir);

            if (currentGuess > m_grabbingThreshold && currentGuess > bestGuess)
            {
                bestGuess = currentGuess;
                selectable = obj;
                m_CurrentNearestObject = selectable;
                //translate the center of the collider into world space (as it is in local space relative to the GameObject)
                m_coll.center = transform.InverseTransformPoint(selectable.transform.position);

                validTargets.Add(selectable);
            }
        }

        //if we found something, let's move our cursor to the selectable object
        if (selectable)
        {
            m_cursor.SetActive(true);
            m_cursor.transform.position = selectable.transform.position;
        }
        else //if not, let's disable the cursor
        {
            m_coll.center = Vector3.zero;
            m_cursor.SetActive(false);
        }
    }

    //tell the XRInteractionManager that we have an object that we can select for when the grab input is activated
    public override bool CanSelect(XRBaseInteractable interactable)
    {
        bool selectActivated = m_CurrentNearestObject == interactable || base.CanSelect(interactable);
        return selectActivated && (selectTarget == null || selectTarget == interactable);
    }
}


