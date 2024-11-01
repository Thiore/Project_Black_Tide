using UnityEngine;

public class GridInitializer : MonoBehaviour
{
    public GameObject tilePrefab;       // Ÿ�Ϸ� ����� ������
    public int gridSize = 6;            // �׸��� ũ�� (6x6)
    public Transform gridParent;        // Grid ������Ʈ�� ���� �θ� ������Ʈ
    public float tileSize = 1f;         // �� Ÿ���� ũ��
    public float tileSpacing = 0.1f;    // Ÿ�� ������ ����

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {

        // �׸����� ���� ��ġ ���� (���� �Ʒ��� �̵�)
        Vector3 startPosition = gridParent.position - new Vector3(
            (gridSize - 1) * (tileSize + tileSpacing) / 2,
            0,
            (gridSize - 1) * (tileSize + tileSpacing) / 2
        );

        // 6x6 Ÿ�� ���� �� ��ġ
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                // Ÿ�� ���� �� ��ġ
                GameObject tile = Instantiate(tilePrefab, startPosition + new Vector3(
                    x * (tileSize + tileSpacing),
                    0,
                    z * (tileSize + tileSpacing)
                ), Quaternion.identity, gridParent);

                tile.name = $"Tile_{x}_{z}"; // Ÿ�� �̸� ����
            }
        }
    }
}
