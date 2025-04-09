using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public static class Util
    {
        public static Random rand = new Random();
        public static int CalculateDamage(Stat _attackerStat, Stat _defenderStat, int RepeatNum)
        {
            int damage = (int)(_attackerStat.Atk - _defenderStat.Def * 0.5) * 3;
            double criticalRate;
            double luckCrit = (_attackerStat.Luc - (_defenderStat.Luc * 0.4)) * 0.7;
            double AgiCrit = (_attackerStat.Agi - (_defenderStat.Agi*0.4)) * 0.3;
            
            if (luckCrit <= 0 || AgiCrit <= 0) { 
                criticalRate = 0;
            }
            else criticalRate = 0.5 * (luckCrit + AgiCrit);
            
            int critJudge = rand.Next(0, 100);
            if(criticalRate > critJudge)
            {
                damage *= 2;
            }
            damage *= RepeatNum;
            if (damage <= 1) damage = 1;

            return damage;
        }
        
        public static int CalculateRepeat(Stat _attackerStat, Stat _defenderStat)
        {
            double comboRate = (_attackerStat.Agi - (_defenderStat.Agi * 0.4)) * 0.3;

            int comboJudge = rand.Next(0, 100);
            int repeat = 1;
            while(comboRate > comboJudge)
            {
                comboJudge = rand .Next(0, 100);
                comboRate *= 0.75;
                repeat++;
            }
            return repeat;

        }

        public static void ReadyPlayer()
        {
            Console.WriteLine("아무키나 눌러 다음으로 진행합니다.");
            Console.ReadKey(true);
        }
    }
}
