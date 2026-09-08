using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private float columns = 9f;
    [SerializeField] private float rows = 9f;
    [SerializeField] private float size = 2f;
    [SerializeField] private float spacing = 2f;

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        for(int i = 0; i < columns; i++){
            for(int j = 0; j < rows; j++){
                Gizmos.DrawWireCube(new Vector2(-i*(size + spacing) + ((columns - 1)*(size + spacing)) / 2f , -j*(size + spacing) + ((rows - 1)*(size + spacing)) / 2f), new Vector2(size, size));
            }
        }
    }
}
