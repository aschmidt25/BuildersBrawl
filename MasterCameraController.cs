using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[CreateAssetMenu]
public class MasterCameraController : ScriptableObject
{
    public Dictionary<string, CinemachineVirtualCameraBase> _masterList;
    public CinemachineVirtualCameraBase _activeCam;

    [SerializeField]
    List<string> _masterCamListNames;

    public void AddToMasterList(CinemachineVirtualCameraBase camToAdd)
    {
        _masterList.Add(camToAdd.name, camToAdd);
        Debug.Log("Add " + camToAdd + " To master list");
        _masterCamListNames.Add(camToAdd.name);

        if (!camToAdd.GetComponent<VirtualCameraControllerBase>().isStartingCam)
        {
            camToAdd.gameObject.SetActive(false);
        }
        else
        {
            _activeCam = camToAdd;
        }
    }

    public void SwitchCamera(string camName)
    {
        _activeCam.gameObject.SetActive(false);
        _masterList[camName].gameObject.SetActive(true);
        _activeCam = _masterList[camName];
    }

    public void Initialize()
    {
        _masterList = new Dictionary<string, CinemachineVirtualCameraBase>();

        _masterCamListNames = new List<string>();

        Debug.Log("Initialize MCC");
    }
}
