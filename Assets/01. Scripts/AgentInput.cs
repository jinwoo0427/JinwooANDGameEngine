using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;
public class AgentInput : MonoBehaviour, IAgentInput
{
    [field: SerializeField] public UnityEvent<Vector3> OnMovementKeyPress { get; set; }
    [field: SerializeField] public UnityEvent OnFireButtonPress { get; set; }
    [field: SerializeField] public UnityEvent OnFireButtonRelease { get; set; }

    [field: SerializeField] public UnityEvent OnRollButtonPress { get; set; }

    public UnityEvent OnReloadButtonPress = null;

    public UnityEvent<int> OnNextWeaponPress;

    private bool _fireButtonDown = false;
    //public UnityEvent<bool> OnNextWeaponPress;

    public string fireButtonName = "Fire1"; // �߻縦 ���� �Է� ��ư �̸�
    public string rollButtonName = "Jump";
    public string moveHorizontalAxisName = "Horizontal"; // �¿� ȸ���� ���� �Է��� �̸�
    public string moveVerticalAxisName = "Vertical"; // �յ� �������� ���� �Է��� �̸�
    public string reloadButtonName = "Reload"; // �������� ���� �Է� ��ư �̸�
    public string oneButtonName = "Swap1"; // �������� ���� �Է� ��ư �̸�
    public string twoButtonName = "Swap2"; // �������� ���� �Է� ��ư �̸�
    public string threeButtonName = "Swap3"; // �������� ���� �Է� ��ư �̸�

    public Vector3 moveInput { get; private set; }
    public bool fire { get; private set; } // ������ �߻� �Է°�
    public bool reload { get; private set; } // ������ ������ �Է°�
    public bool jump { get; private set; }

    private void Update()
    {
        GetFireInput();
        GetReloadInput();
        GetSpaceInput();
        GetMovementInput();
        GetChangeWeapon();
    }
    private void FixedUpdate()
    {
        
        
    }
    private void GetChangeWeapon()
    {
        if(Input.GetButtonDown(oneButtonName))
        {
            OnNextWeaponPress?.Invoke(1);
        }
        if (Input.GetButtonDown(twoButtonName))
        {
            OnNextWeaponPress?.Invoke(2); 
        }
        if (Input.GetButtonDown(threeButtonName))
        {
            OnNextWeaponPress?.Invoke(3); 
        }
    }
    private void GetReloadInput()
    {
        if (Input.GetButtonDown(reloadButtonName))
        {
            OnReloadButtonPress?.Invoke();
        }
    }
    private void GetSpaceInput()
    {
        if (Input.GetButtonDown(rollButtonName) == true)
        {
            OnRollButtonPress?.Invoke();
        }
    }
    
    private void GetFireInput()
    {
        if (Input.GetAxisRaw(fireButtonName) > 0)
        {
            if (!_fireButtonDown)
            {
                fire = true;
                _fireButtonDown = true;
                OnFireButtonPress?.Invoke();
            }
        }
        else
        {
            if (_fireButtonDown)
            {
                fire = false;
                _fireButtonDown = false;
                OnFireButtonRelease?.Invoke();
            }
        }
    }

    private void GetMovementInput()
    {
        moveInput = new Vector3(Input.GetAxisRaw(moveHorizontalAxisName), 0f, Input.GetAxisRaw(moveVerticalAxisName));
        OnMovementKeyPress?.Invoke(moveInput);
    }

}
