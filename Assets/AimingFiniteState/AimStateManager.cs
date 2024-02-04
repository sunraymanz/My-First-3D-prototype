using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class AimStateManager : MonoBehaviour
{
    public AimBaseState currentState;
    public AimingState aimToken = new AimingState();
    public BowDrawState bowDrawToken = new BowDrawState();
    public Animator animToken; 

    float xAxis, yAxis;
    [SerializeField] float mouseSense;
    [SerializeField] Transform targetPos;

    public float targetFOV;
    public float normalFOV = 60f;
    public float drawFOV = 50f;
    public float aimFOV = 40f;
    public float zoomSmooth = 10f;
    CinemachineVirtualCamera camToken;
    public bool isEquip = false;

    public Transform aimPos;
    [SerializeField] LayerMask aimLayer;
    float aimSmooth = 20;

    public MultiAimConstraint bodyRotate;
    public TwoBoneIKConstraint handIK;
    public float bodyWieght = 0;
    public float handWieght = 0;
    public float equipWieght = 0;

    public float xAdaptPos = 0;
    public float yAdaptPos = 0;
    float camAdaptSpeed = 10;


    // Start is called before the first frame update
    void Start()
    {
        camToken = FindObjectOfType<CinemachineVirtualCamera>();
        animToken = GetComponent<Animator>();
        SwitchState(bowDrawToken);
        targetFOV = normalFOV;
        xAdaptPos = targetPos.localPosition.x;
        yAdaptPos = targetPos.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -30, 60);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isEquip)
            { equipWieght = 0; targetFOV = normalFOV; GetComponent<WeaponSystem>().WeaponUneqiup(); }
            else { equipWieght = 1; targetFOV = drawFOV; GetComponent<WeaponSystem>().WeaponEqiup(); }
            isEquip = !isEquip;
        }
        currentState.UpdateState(this);

        camToken.m_Lens.FieldOfView = Mathf.Lerp(camToken.m_Lens.FieldOfView, targetFOV, zoomSmooth * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width/2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,aimLayer))
        {
            //aimPos.position = Vector3.Lerp(aimPos.position,hit.point,Time.deltaTime * aimSmooth);
            aimPos.position =  hit.point;
        }
        SetAimRig();
        CameraAdapt();
    }
    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    private void LateUpdate()
    {
        targetPos.localEulerAngles = new Vector3(yAxis,targetPos.localEulerAngles.y, targetPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SetAimRig() 
    {
        bodyRotate.weight = Mathf.Lerp( bodyRotate.weight, bodyWieght, 10*Time.deltaTime);
        animToken.SetLayerWeight(1,Mathf.Lerp(animToken.GetLayerWeight(1), equipWieght, 10 * Time.deltaTime)) ;
    }

    void CameraAdapt() 
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) xAdaptPos = -xAdaptPos ;
        if (animToken.GetBool("Crouching")) yAdaptPos = 0.5f; else yAdaptPos = 0.8f;
        targetPos.localPosition = new Vector3(Mathf.Lerp(targetPos.localPosition.x,xAdaptPos,camAdaptSpeed*Time.deltaTime), Mathf.Lerp(targetPos.localPosition.y, yAdaptPos, camAdaptSpeed * Time.deltaTime), 0.5f);
    }
}
