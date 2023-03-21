using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraFollowsPlayer : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera cam;
    void Start()
    {
        cam = this.GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(delayedCameraAssignment());
    }
    private IEnumerator delayedCameraAssignment(){
        yield return new WaitForSeconds(0.1f);
        cam.m_Follow = PlayerPersistence.Instance.transform;    
    }
}
