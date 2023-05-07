using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using General;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class BossAi : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        private List<GameObject> _createdTowers = new();
        [SerializeField] private List<int> phaseHealthThresholds;
        [SerializeField] private int shootCyclesBeforeDamagePhase;
        [SerializeField] private int shootCyclesExecuted = 0;
        private GameObject _player;
        private EnemyHealthController _enemyHealthController;
        private float _startingHealth;
        [SerializeField] private bool isShooting;
        [SerializeField] private GameObject leftHand;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private AudioSource shootAudioSource;

        private enum BossPhases
        {
            Phase1,
            Phase2,
            Phase3,
            DamagePhase
        }
        
        [SerializeField] private BossPhases currentPhase = BossPhases.Phase1;
        [SerializeField] private BossPhases phaseBeforeDamagePhase = BossPhases.Phase1;

        private void Start()
        {
            GameObject.FindWithTag("Player");
            _enemyHealthController = GetComponent<EnemyHealthController>();
            _startingHealth = _enemyHealthController.GetMaxHealth();
            leftHand.SetActive(false);
            rightHand.SetActive(false);
        }

        private void OnEnable()
        {
            BuildPhaseManager.TowerPlaced += AddTowerToList;
        }
        
        private void OnDisable()
        {
            BuildPhaseManager.TowerPlaced -= AddTowerToList;
        }

        private void AddTowerToList(GameObject tower)
        {
            _createdTowers.Add(tower);
        }

        private void DestroyTowers()
        {
            foreach (GameObject tower in _createdTowers)
            {
                Destroy(tower);
            }
            
            _createdTowers.Clear();
        }
        
        private void FixedUpdate()
        {
            switch(currentPhase)
            {
                case BossPhases.Phase1:
                    if (_enemyHealthController.GetCurrentHealth() <= phaseHealthThresholds[0])
                    {
                        DestroyTowers();
                        currentPhase = BossPhases.Phase2;
                        break;
                    }
                    
                    if (isShooting) break;

                    if (shootCyclesExecuted >= shootCyclesBeforeDamagePhase)
                    {
                        shootCyclesExecuted = 0;
                        phaseBeforeDamagePhase = BossPhases.Phase1;
                        currentPhase = BossPhases.DamagePhase;
                    }
                    else
                    {
                        StartCoroutine(PhaseOne());    
                    }
                    
                    break;
                case BossPhases.Phase2:
                    if (_enemyHealthController.GetCurrentHealth() <= phaseHealthThresholds[1])
                    {
                        DestroyTowers();
                        currentPhase = BossPhases.Phase3;
                        break;
                    }
                    
                    if (isShooting) break;
                    
                    if (shootCyclesExecuted >= shootCyclesBeforeDamagePhase)
                    {
                        shootCyclesExecuted = 0;
                        phaseBeforeDamagePhase = BossPhases.Phase2;
                        currentPhase = BossPhases.DamagePhase;
                    }
                    else
                    {
                        StartCoroutine(PhaseTwo());    
                    }
                    
                    break;
                case BossPhases.Phase3:
                    //Health controller will take care of death
                    
                    if (isShooting) break;
                    
                    if (shootCyclesExecuted >= shootCyclesBeforeDamagePhase)
                    {
                        shootCyclesExecuted = 0;
                        phaseBeforeDamagePhase = BossPhases.Phase3;
                        currentPhase = BossPhases.DamagePhase;
                    }
                    else
                    {
                        StartCoroutine(PhaseThree());    
                    }
                    
                    break;
                case BossPhases.DamagePhase:
                    StartCoroutine(DamagePhase());
                    break;
            }
        }

        private IEnumerator PhaseOne()
        {
            isShooting = true;
            shootAudioSource.Play();

            const int bulletsToShoot = 6;

            int yOffsetLeft = shootCyclesExecuted % 2 == 0 ? 0 : 1;
            int yOffsetRight = shootCyclesExecuted % 2 == 0 ? 1 : 0;
            
            for (int i = 0; i < bulletsToShoot; i++)
            {
                GameObject leftBullet = Instantiate(bulletPrefab, new Vector3(15, i * 2.5f + yOffsetLeft), Quaternion.identity);
                leftBullet.GetComponent<DirectionalEnemyBullet>().direction = Vector3.left;
            }
            
            for (int i = 0; i < bulletsToShoot; i++)
            {
                GameObject rightBullet = Instantiate(bulletPrefab, new Vector3(0, i * 2.5f + yOffsetRight), Quaternion.identity);
                rightBullet.GetComponent<DirectionalEnemyBullet>().direction = Vector3.right;
            }

            yield return new WaitForSeconds(1.5f);
            isShooting = false;
            shootCyclesExecuted++;
        }

        private IEnumerator PhaseTwo()
        {
            isShooting = true;
            shootAudioSource.Play();

            const int bulletsToShoot = 7;

            int yOffsetLeft = shootCyclesExecuted % 2 == 0 ? 0 : 1;
            int yOffsetRight = shootCyclesExecuted % 2 == 0 ? 1 : 0;
            
            for (int i = 0; i < bulletsToShoot; i++)
            {
                GameObject leftBullet = Instantiate(bulletPrefab, new Vector3(15, i * 2.5f + yOffsetLeft), Quaternion.identity);
                leftBullet.GetComponent<DirectionalEnemyBullet>().direction = new Vector3(-1, -1);
            }
            
            for (int i = 0; i < bulletsToShoot; i++)
            {
                GameObject leftBullet = Instantiate(bulletPrefab, new Vector3(0, i * 2.5f + yOffsetRight), Quaternion.identity);
                leftBullet.GetComponent<DirectionalEnemyBullet>().direction = new Vector3(1, 1);
            }
            
            for (int i = 0; i < 4; i++)
            {
                GameObject leftBullet = Instantiate(bulletPrefab, new Vector3(15, i * 2.5f + yOffsetLeft), Quaternion.identity);
                leftBullet.GetComponent<DirectionalEnemyBullet>().direction = Vector3.left;
            }
            
            for (int i = 0; i < 4; i++)
            {
                GameObject rightBullet = Instantiate(bulletPrefab, new Vector3(0, i * 2.5f + yOffsetRight), Quaternion.identity);
                rightBullet.GetComponent<DirectionalEnemyBullet>().direction = Vector3.right;
            }

            yield return new WaitForSeconds(1.5f);
            isShooting = false;
            shootCyclesExecuted++;
        }

        private IEnumerator PhaseThree()
        {
            isShooting = true;
            shootAudioSource.Play();

            const int bulletsToShoot = 7;

            int yOffsetLeft = shootCyclesExecuted % 2 == 0 ? 0 : 1;
            int yOffsetRight = shootCyclesExecuted % 2 == 0 ? 1 : 0;
            
            // for (int i = 0; i < bulletsToShoot; i++)
            // {
            //     GameObject leftBullet = Instantiate(bulletPrefab, new Vector3(15, i * 2.5f + yOffsetLeft), Quaternion.identity);
            //     leftBullet.GetComponent<DirectionalEnemyBullet>().direction = new Vector3(-1, -1);
            // }
            //
            // for (int i = 0; i < bulletsToShoot; i++)
            // {
            //     GameObject leftBullet = Instantiate(bulletPrefab, new Vector3(0, i * 2.5f + yOffsetRight), Quaternion.identity);
            //     leftBullet.GetComponent<DirectionalEnemyBullet>().direction = new Vector3(1, 1);
            // }
            //
            // for (int i = 0; i < 4; i++)
            // {
            //     GameObject leftBullet = Instantiate(bulletPrefab, new Vector3(15, i * 2.5f + yOffsetLeft), Quaternion.identity);
            //     leftBullet.GetComponent<DirectionalEnemyBullet>().direction = Vector3.left;
            // }
            //
            // for (int i = 0; i < 4; i++)
            // {
            //     GameObject rightBullet = Instantiate(bulletPrefab, new Vector3(0, i * 2.5f + yOffsetRight), Quaternion.identity);
            //     rightBullet.GetComponent<DirectionalEnemyBullet>().direction = Vector3.right;
            // }
            
            for (int i = 0; i < bulletsToShoot; i++)
            {
                GameObject topBullet = Instantiate(bulletPrefab, new Vector3(i * 2.5f + yOffsetLeft, 0), Quaternion.identity);
                topBullet.GetComponent<DirectionalEnemyBullet>().direction = new Vector3(0, 1);
            }
            
            for (int i = 0; i < bulletsToShoot; i++)
            {
                GameObject bottomBullet = Instantiate(bulletPrefab, new Vector3(i * 2.5f + yOffsetRight, 12), Quaternion.identity);
                bottomBullet.GetComponent<DirectionalEnemyBullet>().direction = new Vector3(0, -1);
            }

            yield return new WaitForSeconds(2);
            isShooting = false;
            shootCyclesExecuted++;
        }

        private IEnumerator DamagePhase()
        {
            shootCyclesExecuted = 0;
            isShooting = false;
            leftHand.SetActive(true);
            rightHand.SetActive(true);
            
            yield return new WaitForSeconds(5);
            rightHand.SetActive(false);
            leftHand.SetActive(false);
            currentPhase = phaseBeforeDamagePhase;
        }
    }
}