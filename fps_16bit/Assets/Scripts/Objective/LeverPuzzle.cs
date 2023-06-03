using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;

namespace fps_16bit
{
    public class LeverPuzzle : MonoBehaviour
    {
        public GameObject[] levers;
        public bool[] code;

        bool[] bossCode;

        private void Start()
        {
            bossCode = new bool[code.Length];
        }

        void SendAns()
        {
            /*
            GameObject door = GameObject.Find("Door");

            if (door != null)
            {
                
                door.GetComponent<DoorOpenAnimation>().OpenDoor();
            }*/
        }

        public void RecieveSignal(GameObject obj, bool state)
        {
            for (int i = 0; i < levers.Length; i++)
            {
                if (obj == levers[i])
                {
                    bossCode[i] = state;
                    break;
                }
            }
            Verificate();
        }

        public void Verificate()
        {
            bool isRight = true;

            for (int i = 0; i < code.Length; i++)
            {
                if (bossCode[i] != code[i])
                {
                    isRight = false;
                    break;
                }

            }

            if (isRight)
            {
                foreach (GameObject obj in levers)
                {
                    obj.GetComponent<Lever>().canTurn = false;
                }
                SendAns();
            }
        }
    }
}
