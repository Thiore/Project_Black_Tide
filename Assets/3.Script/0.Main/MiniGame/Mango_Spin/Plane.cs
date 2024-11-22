 using System.Collections.Generic;
 using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<GameObject> tileList;
    public List<GameObject> rayStartTile;
    public Material lampMaterial;

    private void Start()
    {
        // ��� Ÿ�Ͽ� �ִ� GridSpin�� ȸ�� �Ϸ� �̺�Ʈ ����
        foreach (var tile in tileList)
        {
            GridSpin gridSpin = tile.GetComponent<GridSpin>();
            if (gridSpin != null)
            {
                gridSpin.OnRotationComplete += CheckRay; // ȸ�� �Ϸ� �� CheckRay ȣ��
            }
        }
        lampMaterial.DisableKeyword("_EMISSION");
    }



    // CheckRay �޼���: ��� rayStartTile�� RayCheck�� Ȯ��
    private void CheckRay()
    {
        foreach (var ray in rayStartTile)
        {
            RayCheck rayCheck = ray.GetComponent<RayCheck>();
            rayCheck.CheckConnections();

            if (rayCheck.isComplete)
            {
                lampMaterial.EnableKeyword("_EMISSION");
                //gamecomplete
            }
        }
    }
}
