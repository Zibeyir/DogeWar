using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBot : MonoBehaviour
{
    public Transform UpPos;
    public Transform DownPos;
    public Transform RightPos;
    public Transform LeftPos;


    Transform Player;
    public float xPos;
    public float MaxxPos;
    public float MinxPos;
    public float MidxPos;
    public float yPos;
    public float MaxyPos;
    public float MinyPos;
    public float MidyPos;
    public float MaxxPosS;
    public float MinxPosS;
    public float MaxyPosS;
    public float MinyPosS;
    float zPos;
    public float TimeMove;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        MaxxPos = RightPos.localPosition.x;
        MaxxPosS = MaxxPos;
        MinxPos = LeftPos.localPosition.x;
        MinxPosS = MinxPos;
        MidxPos = (MaxxPos + MinxPos) / 2;
        MaxyPos = UpPos.localPosition.y;
        MaxyPosS = MaxyPos;
        MinyPos = DownPos.localPosition.y;
        MinyPosS = MinyPos;
        MidyPos = (MaxyPos + MinyPos) / 2;
        xPos = Random.Range(MinxPos, MidxPos);
        MinxPos = xPos;
        yPos = Random.Range(MinyPos, MidyPos);
        MinyPos = yPos;
        zPos = transform.position.z;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x>=MaxxPos)
        {
            xPos = Random.Range(MinxPosS, MidxPos);
            MinxPos = xPos;
        }if (transform.localPosition.x<=MinxPos)
        {
            xPos = Random.Range(MidxPos, MaxxPosS);
            MaxxPos = xPos;
        }
        if (transform.localPosition.y >= MaxyPos)
        {
            yPos = Random.Range(MinyPosS, MidyPos);
            MinyPos = yPos;
        }
        if (transform.localPosition.y <= MinyPos)
        {
            yPos = Random.Range(MidyPos, MaxyPosS);
            MaxyPos = yPos;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(xPos, yPos, 0), TimeMove*Time.deltaTime);

    }
}
