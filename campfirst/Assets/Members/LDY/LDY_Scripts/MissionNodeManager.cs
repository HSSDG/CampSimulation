using UnityEngine;

public class MissionNodeManager : MonoBehaviour
{
    public DialogNode_HW dialogNode;
    public void Success()
    {
        if (dialogNode != null)
        {
            Debug.Log("[미션 성공]");
            dialogNode.requiresMissionSuccess = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
