using BlockScripts;
using UnityEngine;

namespace Spawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnContainer;
        [SerializeField] private int _repeatCount;
        [SerializeField] private int _distanceBetweenFullLine;
        [SerializeField] private int _distanceBetweenRandomLine;

        [SerializeField] private Block _blockTemplate;
        [SerializeField] private int _blockSpawnChance;

        private SpawnPoint[] _spawnPoints;

        private void Start()
        {
            _spawnPoints = GetComponentsInChildren<SpawnPoint>();

            for (int i = 0; i < _repeatCount; i++)
            {
                MoveSpawner(_distanceBetweenFullLine);
                GenerateFullLine(_spawnPoints, _blockTemplate.gameObject);
                MoveSpawner(_distanceBetweenRandomLine);
                GenerateRandomLine(_spawnPoints, _blockTemplate.gameObject, _blockSpawnChance);
            }
        }

        private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject spawnObject)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                GenerateElement(spawnPoint.transform.position, spawnObject);
            }
        }

        private void GenerateRandomLine(SpawnPoint[] spawnPoints, GameObject spawnObject, int spawnChance)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                if (spawnChance > Random.Range(0, 100))
                    GenerateElement(spawnPoint.transform.position, spawnObject);
            }
        }

        private GameObject GenerateElement(Vector2 spawnPoint, GameObject spawnObject) =>
            Instantiate(spawnObject, spawnPoint, Quaternion.identity, _spawnContainer);

        private void MoveSpawner(int distanceY) =>
            transform.position = new Vector2(transform.position.x, transform.position.y + distanceY);
    }
}