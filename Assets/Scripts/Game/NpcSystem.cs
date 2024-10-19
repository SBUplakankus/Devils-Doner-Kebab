using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class NpcSystem : MonoBehaviour
    {
        public Transform[] npcSpawns;
        public Transform[] npcPoints;

        public NpcController weirdo, magic, chef, demon;

        private void Start()
        {
            demon.GetComponent<NavMeshAgent>().enabled = false;
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
            }
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
            }
        }

    }
}
