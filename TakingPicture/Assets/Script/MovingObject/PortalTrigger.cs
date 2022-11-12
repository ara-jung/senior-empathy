using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PortalTrigger : MonoBehaviour
{

    public DirectionEventChannelSO objectDeirectionToMove;

    public enum Direction {Front, Left, Right, Back};

    public Direction portalDirection;


    public void MoveDirection()
    {
        if (objectDeirectionToMove)
        {
            switch(portalDirection)
            {
                case Direction.Front:
                objectDeirectionToMove.RaiseEvent(Vector3.forward);
                break;
                case Direction.Left:
                objectDeirectionToMove.RaiseEvent(Vector3.left);
                break;
                case Direction.Right:
                objectDeirectionToMove.RaiseEvent(Vector3.right);
                break;
                case Direction.Back:
                objectDeirectionToMove.RaiseEvent(Vector3.back);
                break;
                default:
                break;
            }
        }
    }


}
