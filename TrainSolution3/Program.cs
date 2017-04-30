using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSolution3
{
    class Program
    {
        private static HangmanService hangmanService;

        static void Main(string[] args)
        {
            hangmanService = new HangmanService();
            hangmanService.Restart();

            Boolean play = true;

            while (play)
            {
                Console.Write("please enter character : ");
                char d = Char.Parse(Console.ReadLine());

                Console.WriteLine("Enum : " + hangmanService.Input(d));

                int something = (int)hangmanService.Input(d);

                if ((int)hangmanService.Input(d) == 0)
                {
                    hangmanService.GetDisplay();
                }
                else if ((int)hangmanService.Input(d) == 1)
                {
                    Console.WriteLine("Sorry, you ’re wrong. Remaining ... " + hangmanService.GetRemainingTry() + " time");

                    if(hangmanService.GetRemainingTry() == 0)
                    {
                        play = false;
                        Console.WriteLine("GAME OVER");
                    }
                   

                }
                else
                {
                    Console.WriteLine("you have already tried this character.");
                }

                //Console.ReadLine();
            }
        }

    }
    public class HangmanService
    {
        private static String[] word;
        private char[] selectWord;
        private Random random;
        List<Char> showUnder = new List<Char>();
        enum Status { Correct = 0, Incorrect = 1, again = 2 };
        int amountleft = 10;

        public HangmanService()
        {
            random = new Random();
            word = new[] { "ironman", "hulk", "batman" };
        }

        public void Restart()
        {
            int valueRandom = random.Next(0, word.Length);
            selectWord = word[valueRandom].ToCharArray();

            //ยัดตำแหน่ง _ ครั้งแรกให้ showUnder
            for (int i = 0; i < selectWord.Length; i++)
            {
                showUnder.Add('_');
            }

            Console.WriteLine(GetDisplay());
        }

        public String GetDisplay()
        {
            //selectWord.ToList().ForEach(Console.Write);

            String show = "";
            for (int i = 0; i < selectWord.Length; i++)
            {
                //Console.WriteLine("selectWord : " + selectWord[i]);
                show += showUnder[i] + " ";
            }

            //showUnder.ForEach(Console.Write);
            return show;
        }

        public Status Input(char s)
        {
            int switchS = 1;

            Status statusEnum;

            //Status status = new Status();
            //Console.WriteLine("status: "+ Status.Correct);
            for (int i = 0; i < selectWord.Length; i++)
            {
                //check ว่าใส่ค่าเดิมไหม
                if (showUnder[i] == s)
                {
                    switchS = 2;
                    break;
                }

                //check ว่าตรงกับ ในโจทไหม
                if (selectWord[i] == s)
                {
                    showUnder[i] = selectWord[i];

                    switchS = 0;
                }
            }
            //statusEnum = Status.Incorrect;

            switch (switchS)
            {
                case 0: //ตรง
                    statusEnum = Status.Correct;
                    break;
                case 2: //ใส่ค่าเดิม
                    statusEnum = Status.again;
                    break;
                default: //ไม่ตรง
                    statusEnum = Status.Incorrect;
                    break;
            }

            return statusEnum;
        }



        public int GetRemainingTry()
        {
            amountleft--;

            return amountleft;
        }


    }



}