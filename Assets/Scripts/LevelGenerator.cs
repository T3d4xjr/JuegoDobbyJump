using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform platforms;
    public GameObject platform;
    private Vector3 spawnPos;
    public int numberOfPlatform = 10;

    public float levelWidth =2.2f;
    public float minY =0.2f;
    public float maxY=1.5f;

    private void Awake(){
    spawnPos.y = 0; // Iniciar la generación más arriba
    for(int i = 0; i < numberOfPlatform; i++){
        spawnPos.x = Random.Range(-levelWidth, levelWidth);
        spawnPos.y += Random.Range(minY, maxY);
        Instantiate(platform, spawnPos, Quaternion.identity);
    }
}


}
