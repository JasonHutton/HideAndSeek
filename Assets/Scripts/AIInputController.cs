using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputController : InputControllerI
{
    private float directionChangeInterval;
    private float timer;
    public float minDirectionChangeInterval;
    public float maxDirectionChangeInterval;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > directionChangeInterval)
        {
            timer = 0;
            directionChangeInterval = Random.Range(minDirectionChangeInterval, maxDirectionChangeInterval);
            ResetMovement();

            int decision = Mathf.FloorToInt(Random.Range(0.0f, 8.0f));
            switch (decision)
            {
                case 0:
                    MoveUp = true;
                    break;
                case 1:
                    MoveDown = true;
                    break;
                case 2:
                    MoveLeft = true;
                    break;
                case 3:
                    MoveRight = true;
                    break;
                case 4:
                    MoveUp = true;
                    MoveLeft = true;
                    break;
                case 5:
                    MoveUp = true;
                    MoveRight = true;
                    break;
                case 6:
                    MoveDown = true;
                    MoveLeft = true;
                    break;
                case 7:
                    MoveDown = true;
                    MoveRight = true;
                    break;
            }
        }

    }
}
