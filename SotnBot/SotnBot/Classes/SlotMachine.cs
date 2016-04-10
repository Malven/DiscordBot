using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotnBot.Classes
{
    public class SlotMachine
    {
        Random randNumGen = new Random();
        string slot1;
        string slot2;
        string slot3;

        //record the next 3 numbers in the RandNumGen 
        int randNum1;
        int randNum2;
        int randNum3;
        //divide the 3 numbers by 8 with remainders (%) 
        int modR1;
        int modR2;
        int modR3;

        public SlotMachine()
        {
            SetupSlotMachine();
        }


        private string[] slots = new string[32];

        public string Slot1
        {
            get
            {
                return slot1;
            }

            set
            {
                slot1 = value;
            }
        }

        public string Slot2
        {
            get
            {
                return slot2;
            }

            set
            {
                slot2 = value;
            }
        }

        public string Slot3
        {
            get
            {
                return slot3;
            }

            set
            {
                slot3 = value;
            }
        }

        private void SetupSlotMachine()
        {
            randNum1 = randNumGen.Next(10000, 20000);
            randNum2 = randNumGen.Next(30000, 40000);
            randNum3 = randNumGen.Next(50000, 60000);

            modR1 = randNum1 % 32;
            modR2 = randNum2 % 32;
            modR3 = randNum3 % 32;

            string[] slots = new string[32];

            for (int i = 0; i < 8; i++)
                slots[i] = "GRAPE";
            for (int i = 8; i < 15; i++)
                slots[i] = "APPLE";
            for (int i = 15; i < 21; i++)
                slots[i] = "WILD CHERRY";
            for (int i = 21; i < 26; i++)
                slots[i] = "BELL";
            for (int i = 26; i < 30; i++)
                slots[i] = "BAR";
            for (int i = 30; i < 32; i++)
                slots[i] = "LUCKY 7";

            Slot1 = slots[modR1];
            Slot2 = slots[modR2];
            Slot3 = slots[modR3];
        }

        public void ResetGame()
        {
            SetupSlotMachine();
        }
    }
}
