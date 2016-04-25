using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotnBot.Classes
{
    public class SlotMachineFactory
    {
        private static SlotMachineFactory _slotMachine = new SlotMachineFactory();

        public static SlotMachineFactory Instance
        {
            get { return _slotMachine; }
        }

        private static SlotMachine slotMachine;

        public static int CheckIfWin()
        {
            if (SlotTypesAreEqual("GRAPE"))
                return 2;
            else if (SlotTypesAreEqual("APPLE"))
                return 4;
            else if (SlotTypesAreEqual("WILD CHERRY"))
                return 5;
            else if (SlotTypesAreEqual("BELL"))
                return 10;
            else if (SlotTypesAreEqual("BAR"))
                return 15;
            else if (SlotTypesAreEqual("LUCKY 7"))
                return 25;
            else
                return 0;
        }
         
        public static string Play(Discord.User _user, int betAmount )
        {
            //_slotMachine.slotMachine.ResetGame();
            slotMachine = new SlotMachine();
            User user = EconomyFactory.FindUser(_user);
            if (user == null)
                return "You need to register with the bank before you can play.";
            if (user.cashAmount < betAmount)
                return "You dont have enough cash.";

            int multiplier = CheckIfWin();
            if(multiplier != 0)
            {
                int sum = (betAmount * multiplier);
                EconomyFactory.Instance.bank.cashAmount -= sum;
                user.cashAmount += sum;
                EconomyFactory.Save();
                return ShowSlotValues() + "You won $" + sum + "!";
            }else
            {
                user.cashAmount -= betAmount;
                EconomyFactory.Instance.bank.cashAmount += betAmount;
                EconomyFactory.Save();
                return ShowSlotValues() +
                "You didn't win anything, too bad.";
            }
        }

        private static string ShowSlotValues()
        {
            return slotMachine.Slot1 + " | " + slotMachine.Slot2 + " | " + slotMachine.Slot3 + "\n";
        }

        private static bool SlotTypesAreEqual(string _slotType)
        {
            if (slotMachine.Slot1.ToString() == _slotType && slotMachine.Slot2.ToString() == _slotType && slotMachine.Slot3.ToString() == _slotType)
                return true;
            else
                return false;
        }
    }
}
