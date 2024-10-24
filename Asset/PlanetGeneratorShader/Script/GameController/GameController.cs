using TMPro;
using UnityEngine;
using Unity.Cinemachine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject GameUI;

    [SerializeField]CinemachineInputAxisController c;
    [SerializeField]CinemachineCamera MenuCam, GameCam;
    public void StartGame()
    {
        MainMenu.SetActive(false);
        GameUI.SetActive(true);

        MenuCam.Priority = 0;
        GameCam.Priority = 1;
        c.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
