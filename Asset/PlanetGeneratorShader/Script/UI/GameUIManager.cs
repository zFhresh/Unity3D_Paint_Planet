using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
public class GameUIManager : MonoBehaviour
{   
    [System.Serializable]
    public class MovingCanvaObject {
        public GameObject CanvaGM;
        public bool IsOn = false;
        public Vector3 BasePosition;
    }
    [SerializeField]Transform Target;
    [SerializeField]List<MovingCanvaObject> MovingCanvaObjects = new List<MovingCanvaObject>();

    bool AnyCanvaIsMoving = false;

    [Space(20)]
    [Header("Easer ref")]
    [SerializeField]PaintWithMouseCompute PaintWithMouseCompute;
    [SerializeField] Button EaserButton;

    void Start()
    {
        foreach(MovingCanvaObject CanvaObject in MovingCanvaObjects) {
            CanvaObject.BasePosition = CanvaObject.CanvaGM.transform.position;
        }

        EaserButton.onClick.AddListener(OnClickEaser);

    }


    public void OnClickCanvaButton(int SenderCanvaIndex) {
        MovingCanvaObject CanvaObject = MovingCanvaObjects[SenderCanvaIndex];

        if(AnyCanvaIsMoving) {
            return;
        }

        if(CanvaObject.IsOn) {

            CanvaObject.IsOn = false;
            AnyCanvaIsMoving = true;

            CanvaObject.CanvaGM.transform.DOMove(CanvaObject.BasePosition, 0.5f).OnComplete(() => {
                AnyCanvaIsMoving = false;
            });
            return;
        }

        AnyCanvaIsMoving = true;

        CanvaObject.IsOn = true;

        Vector3 TargetPosition = new Vector3(Target.position.x , CanvaObject.BasePosition.y, CanvaObject.BasePosition.z);
        CanvaObject.CanvaGM.transform.DOMove(TargetPosition, 0.5f).OnComplete(() => {
            AnyCanvaIsMoving = false;
        });

    }

    public void OnClickEaser() {
        bool EaserValue = PaintWithMouseCompute.ChangeEraseMode();
        EaserButton.GetComponent<Image>().color = EaserValue ? Color.green : Color.white;

        
    }
    
}

