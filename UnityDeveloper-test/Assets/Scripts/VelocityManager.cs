using UnityEngine;
public class VelocityManager : MonoBehaviour, IMove
{
    public void ChangeSpeed(Vector2 speed, Rigidbody2D rigidBody)
    {
        if (rigidBody != null & speed!=null)
        {
            if (float.IsNaN(speed.x) || float.IsNaN(speed.y))
            {
                Debug.LogWarning("DeadZone");
                rigidBody.velocity = new Vector2(0, 0);
            }
            else
            {
                rigidBody.velocity = speed;
            }
        }
    }
}