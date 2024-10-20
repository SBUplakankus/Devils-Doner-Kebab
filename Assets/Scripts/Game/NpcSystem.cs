using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class NpcSystem : MonoBehaviour
    {
        public Transform[] npcSpawns;
        public Transform[] npcPoints;

        public NpcController weirdo, magic, chef, demon, heretic;

        private void Start()
        {
            
        }

        /// <summary>
        /// Spawn in a NPC
        /// </summary>
        /// <param name="npc">NPC ID</param>
        /// <param name="point">Spawn Point ID</param>
        public void SpawnNpc(int npc, int point)
        {
            switch (npc)
            {
                case 0:
                    weirdo.SpawnNpc(npcSpawns[point]);
                    break;
                case 1:
                        magic.SpawnNpc(npcSpawns[point]);
                    break;
                case 2:
                    chef.SpawnNpc(npcSpawns[point]);
                    break;
                case 3:
                    demon.SpawnNpc(npcSpawns[point]);
                    break;
                case 4:
                    heretic.SpawnNpc(npcSpawns[point]);
                    break;
            }
        }
        public void MoveNpc(int npc, int point)
        {
            switch (npc)
            {
                case 0:
                    weirdo.UpdateMovePosition(npcPoints[point]);
                    break;
                case 1:
                    magic.UpdateMovePosition(npcPoints[point]);
                    break;
                case 2:
                    chef.UpdateMovePosition(npcPoints[point]);
                    break;
                case 3:
                    demon.UpdateMovePosition(npcPoints[point]);
                    break;
                case 4:
                    heretic.UpdateMovePosition(npcPoints[point]);
                    break;
            }
        }

        public void NpcAttacking(int npc)
        {
            switch (npc)
            {
                case 0:
                    weirdo.isAttacking = true;
                    break;
                case 1:
                    magic.isAttacking = true;
                    break;
                case 2:
                    chef.isAttacking = true;
                    break;
                case 3:
                    demon.isAttacking = true;
                    break;
                case 4:
                    heretic.isAttacking = true;
                    break;
            }
        }
        
        public void PlayNpcScream(int npc)
        {
            switch (npc)
            {
                case 0:
                    weirdo.PlayNpcScream();
                    break;
                case 1:
                    magic.PlayNpcScream();
                    break;
                case 2:
                    chef.PlayNpcScream();
                    break;
                case 3:
                    demon.PlayNpcScream();
                    break;
                case 4:
                    heretic.PlayNpcScream();
                    break;
            }
        }

        public void DisableDemonChefNav()
        {
            demon.GetComponent<NavMeshAgent>().enabled = false;
        }
        public void SetNpcToLeaving(int npc)
        {
            switch (npc)
            {
                case 0:
                    weirdo.isLeaving = true;
                    break;
                case 1:
                    magic.isLeaving = true;
                    break;
                case 2:
                    chef.isLeaving = true;
                    break;
                case 3:
                    demon.isLeaving = true;
                    break;
            }
        }

        public void LookAtPlayer(int npc)
        {
            switch (npc)
            {
                case 0:
                    weirdo.LookAtPlayer();
                    break;
                case 1:
                    magic.LookAtPlayer();
                    break;
                case 2:
                    chef.LookAtPlayer();
                    break;
                case 3:
                    demon.LookAtPlayer();
                    break;
                case 4:
                    heretic.LookAtPlayer();
                    break;
            }
        }

        public void DeleteNpc(int npc)
        {
            switch (npc)
            {
                case 0:
                    weirdo.gameObject.SetActive(false);
                    break;
                case 1:
                    magic.gameObject.SetActive(false);
                    break;
                case 2:
                    chef.gameObject.SetActive(false);
                    break;
                case 3:
                    demon.gameObject.SetActive(false);
                    break;
                case 4:
                    heretic.gameObject.SetActive(false);
                    break;
            }
        }

    }
}
