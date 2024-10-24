using UnityEngine;
using Unity.Cinemachine;
public class CamDisablerScript : MonoBehaviour
{
    CinemachineInputAxisController cinemachineInputAxisController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cinemachineInputAxisController = GetComponent<CinemachineInputAxisController>();
            cinemachineInputAxisController.enabled = !cinemachineInputAxisController.enabled;
        }        
    }
}
