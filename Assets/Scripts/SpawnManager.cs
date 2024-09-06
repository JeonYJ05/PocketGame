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
        [SerializeField] GameObject _specialMonster;
        [SerializeField] GameObject _bossMonster;
        [SerializeField] int _spawnTimer;

        private bool isTenSpawend = true;
        private bool isTwentySpawend = true;
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
            if(GameManager.Instance.MyScore == 10 && isTenSpawend)
            {
                Instantiate(_specialMonster, _spawnSpot[1].transform.position, _spawnSpot[3].transform.rotation);
                Instantiate(_specialMonster, _spawnSpot[3].transform.position, _spawnSpot[3].transform.rotation);
                Instantiate(_specialMonster, _spawnSpot[5].transform.position, _spawnSpot[3].transform.rotation);
                isTenSpawend = false;
            }
            if(GameManager.Instance.MyScore == 20 && isTwentySpawend)
            {
                Instantiate(_specialMonster, _spawnSpot[1].transform.position, _spawnSpot[3].transform.rotation);
                Instantiate(_bossMonster, _spawnSpot[3].transform.position, _spawnSpot[3].transform.rotation);
                Instantiate(_specialMonster, _spawnSpot[5].transform.position, _spawnSpot[3].transform.rotation);
                isTwentySpawend = false;
            }
            
        }
        private IEnumerator SpawnTime()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(_spawnTimer);
            }
        }

        //private void ScoreTen()
        //{
        //    Instantiate(_specialMonster, _spawnSpot[1].transform.position, _spawnSpot[3].transform.rotation);
        //    Instantiate(_specialMonster, _spawnSpot[3].transform.position, _spawnSpot[3].transform.rotation);
        //    Instantiate(_specialMonster, _spawnSpot[5].transform.position, _spawnSpot[3].transform.rotation);
        //    TenSpawend = false;
        //}
        //
        //private void ScoreTwenty()
        //{
        //    Instantiate(_specialMonster, _spawnSpot[1].transform.position, _spawnSpot[3].transform.rotation);
        //    Instantiate(_bossMonster, _spawnSpot[3].transform.position, _spawnSpot[3].transform.rotation);
        //    Instantiate(_specialMonster, _spawnSpot[5].transform.position, _spawnSpot[3].transform.rotation);
        //    TwentySpawend = false;
        //}



    }


 }
