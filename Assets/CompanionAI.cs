using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAI : MonoBehaviour
{

    public Transform Idle;
    public Transform Stealth;
    float angle = 0;
    float rotationA = 0;

    enum Mode
    {
        IDLE,
        STEALTH,
        ATTACK
    };

    [SerializeField]
    Mode currentMode;
    Mode oldMode;

    // Start is called before the first frame update
    void Start()
    {
        currentMode = Mode.IDLE;
        oldMode = Mode.STEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMode == Mode.IDLE)
        {
            rotationA = 0;
            if (Vector3.Distance(transform.position, Idle.position) > 0.1 && (oldMode == Mode.STEALTH || oldMode == Mode.ATTACK))
            {
                Vector3 move = (Idle.position - transform.position).normalized;
                transform.position = new Vector3(transform.position.x + 0.01f * move.x, transform.position.y + 0.01f * move.y, transform.position.z + 0.01f * move.z);
            }
            else
            {
                oldMode = Mode.IDLE;
                angle += Time.deltaTime;
                transform.localPosition = new Vector3(transform.localPosition.x + 0.0001f * Mathf.Sin(angle), transform.localPosition.y + 0.0005f * Mathf.Sin(angle), transform.localPosition.z + 0.0001f * Mathf.Sin(angle));
            }
        }
        else if (currentMode == Mode.STEALTH)
        {
            rotationA = 0;
            if (Vector3.Distance(transform.position, Stealth.position) > 0.1)
            {
                Vector3 move = (Stealth.position - transform.position).normalized;
                transform.position = new Vector3(transform.position.x + 0.01f * move.x, transform.position.y + 0.01f * move.y, transform.position.z + 0.01f * move.z);
            }
            else
            {
                oldMode = Mode.STEALTH;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, Idle.position) > 0.1 && oldMode == Mode.STEALTH)
            {
                Vector3 move = (Idle.position - transform.position).normalized;
                transform.position = new Vector3(transform.position.x + 0.01f * move.x, transform.position.y + 0.01f * move.y, transform.position.z + 0.01f * move.z);
            }
            else
            {
                oldMode = Mode.ATTACK;
                transform.localPosition = new Vector3(0.5f * Mathf.Cos(rotationA), transform.localPosition.y, 0.5f * Mathf.Sin(rotationA));
                rotationA += 2 * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.J))
        {
            currentMode = Mode.IDLE;
        }
        if (Input.GetKey(KeyCode.K))
        {
            currentMode = Mode.STEALTH;
        }
        if (Input.GetKey(KeyCode.L))
        {
            currentMode = Mode.ATTACK;
        }
    }
}
