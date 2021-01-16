using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableZone : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if (controller.health > 0)
            {
                controller.ChangeHealth(-1);
            }
        }
    }
}
