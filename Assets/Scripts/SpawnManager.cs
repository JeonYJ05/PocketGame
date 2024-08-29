using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YJ.PocketGame.Monsters;

namespace YJ.PocketGame
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] GameObject[] _spawnSpot;
        [SerializeField] GameObject _normalMonster;

        [SerializeField] float _minY;
        [SerializeField] float _maxY;
        private void Start()
        {
            StartCoroutine(SpawnTime());
        }
        public void Spawn()
        {
            int RanY = UnityEngine.Random.Range(0, 7);
            
           
            GameObject SpawnMon = Instantiate(_normalMonster, _spawnSpot[RanY].transform.position , _spawnSpot[RanY].transform.rotation);
            //SpawnMon.GetComponent<MonsterBase>().Initialize(RanY);

            
        }
        private IEnumerator SpawnTime()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(3);
            }
        }



    }


 }
