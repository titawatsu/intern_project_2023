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

        public Animator doorAnim;

        private void Start()
        {
            bossCode = new bool[code.Length];
        }

        void SendAns()
        {
            doorAnim.SetBool("open", true);
        }

        public void RecieveSignal(GameObject obj, bool state)
        {
            Debug.Log("Check Signal");
            for (int i = 0; i < levers.Length; i++)
            {
                if (obj == levers[i])
                {
                    bossCode[i] = state;
                    break;
                }
            }
            Debug.Log("....");
            Verificate();
            Debug.Log("Verifing....");
        }

        public void Verificate()
        {
            bool isRight = true;

            for (int i = 0; i < code.Length; i++)
            {
                if (bossCode[i] != code[i])
                {
                    isRight = false;
                    Debug.Log("Failed");
                    break;
                }

            }

            if (isRight)
            {
                Debug.Log("Right!");
                foreach (GameObject obj in levers)
                {
                    obj.GetComponent<Lever>().canTurn = false;
                }
                Debug.Log("Yes");
                SendAns();
            }
        }
    }
}
