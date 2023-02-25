using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Objective : MonoBehaviour
{
    [SerializeField]
    private Color _iconColor = new Color(0, .8f, 1);
    [SerializeField]
    private Sprite _objectiveIcon;
    [SerializeField]
    //private UnityEvent _onCompleteEvents;
    

    private void Start() => CompassManager.Instance.AddObjectiveForObject
    (gameObject, _iconColor, _objectiveIcon);

    private void OnTriggerEnter(Collider other)
    {
        //_onCompleteEvents.Invoke();
        Destroy(this.gameObject);
    }
}