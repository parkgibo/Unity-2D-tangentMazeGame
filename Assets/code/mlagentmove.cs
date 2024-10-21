using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class MoveToTargetAgent : Agent
{

    [SerializeField] private Transform target;
    [SerializeField] private SpriteRenderer backgroundSpriteRandere;
    

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-3.5f, -1.5f), Random.Range(-3.5f, 3.5f));
        target.localPosition = new Vector3(Random.Range(-1.5f, -3.5f), Random.Range(-3.5f, 3.5f));
    }
    

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector2)transform.localPosition);
        sensor.AddObservation((Vector2)target.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        float movementSpeed = 5f;

        transform.localPosition += new Vector3(moveX, moveY) * Time.deltaTime * movementSpeed;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> ContinuousActions = actionsOut.ContinuousActions;
        ContinuousActions[0] = Input.GetAxisRaw("Horizontal");
        ContinuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.TryGetComponent(out Target target))
        {
            AddReward(10f);
            backgroundSpriteRandere.color = Color.green;
            EndEpisode();
            GameManager.Instance.aiScore += 1;
            GameManager.Instance.AIScoreTxt.text = "Score : " + GameManager.Instance.aiScore;
        }
        else if(collision.TryGetComponent(out Wall wall))
        {
            AddReward(-2f);
            backgroundSpriteRandere.color = Color.red;
            EndEpisode();

        }
    }
}