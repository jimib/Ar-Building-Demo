using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class ARPlaceItemOnPlane : MonoBehaviour
{
    public GameObject gaToPlace;

    private GameObject _gaPlaced;
    private ARRaycastManager _arRaycastManager;
    private Vector2 _vecTouchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition( out Vector2 touchPosition ){
        if( Input.touchCount > 0 ){
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!TryGetTouchPosition( out Vector2 _vecTouchPosition )){
            //don't continue
            return;
        }

        if( _arRaycastManager.Raycast( _vecTouchPosition, hits, TrackableType.PlaneWithinPolygon ) ){
            var hitPose = hits[0].pose;

            if( _gaPlaced == null ){
                _gaPlaced = Instantiate( gaToPlace, hitPose.position, hitPose.rotation );
            }else{
                _gaPlaced.transform.position = hitPose.position;
            }
        }
    }
}
