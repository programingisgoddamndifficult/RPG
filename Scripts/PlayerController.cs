using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;

    public LayerMask movementMask;//限定角色移动位置（仅包括Ground图层）
    
    Camera cam;
    PlayerMotor motor;//控制角色移动
    
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(Input.GetMouseButtonDown(0))//0为左键，人物移动
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,100,movementMask))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);

                //停止聚焦于某物品
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))//1为右键，物品交互
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        
        void SetFocus(Interactable newFocus)
        {
            if (newFocus !=focus)
            {
                if(focus!= null)
                    focus.OnDefocused();

                focus = newFocus;
                motor.FollowTarget(newFocus);
            }
            
            newFocus.OnFocused(transform);      
        }

        void RemoveFocus()
        {
            if (focus != null)
                focus.OnDefocused();
            focus=null;
            motor.StopFollowingTarget();
        }
        
    }
}
