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
            int congratulation = 0;

            while (play)
            {
                Console.Write("please enter character : ");
                try
                {
                    char d = Char.Parse(Console.ReadLine());
                    int resultInput = (int)hangmanService.Input(d);

                    if (resultInput == 0)
                    {//ตรง
                        String resultDisplay = hangmanService.GetDisplay();
                        Console.WriteLine(resultDisplay);
                        
                        congratulation++;
                        //Console.WriteLine("congratulation : " + congratulation);
                        //Console.WriteLine("resultDisplay.Length/2 : " + resultDisplay.Length / 2);
                        
                        if (congratulation == (resultDisplay.Length/2)) //length มันมีเว้นวรรคติดมาด้วยเลยต้องหาร 2 *มีปัญหา n ไป2ครั้ง
                        {//ชนะs
                            Console.WriteLine("\nCongratulation, you’re win.");
                            Console.WriteLine("please enter any key...");
                            Console.ReadLine();
                            play = false;
                        }

                        /*
                        Console.WriteLine("resultDisplay.Length : " + resultDisplay.Length);
                        int j = 0;
                        for (int i = 0; i < resultDisplay.Length; i++)
                        {
                            //Console.WriteLine("i : " + i);
                            if (resultDisplay[i].Equals("_ "))
                            {
                                Console.WriteLine(resultDisplay[i]);


                                //Console.WriteLine("!resultDisplay[i].Equals() : " + !resultDisplay[i].Equals("_"));
                                j++;
                            }

                            if (j == resultDisplay.Length)
                            {
                                Console.WriteLine("Congratulation, you’re win.");
                                //play = false;
                            }

                        }
                        */



                    }
                    else if (resultInput == 1)
                    {//ไม่ตรง
                        Console.WriteLine("Sorry, you ’re wrong. Remaining ... " + hangmanService.GetRemainingTry() + " time");

                        if (hangmanService.GetRemainingTry() == 0)
                        {//ชีวิตหมด แพ้
                            play = false;
                            Console.WriteLine("GAME OVER");
                        }
                    }
                    else
                    {//ซ้ำ
                        Console.WriteLine("you have already tried this character.");
                    }

                    //Console.ReadLine();}
                }
                catch (ArgumentNullException ane)
                {
                    Console.Write("Null");
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("\nRestart");
                    hangmanService.Restart();
                }
            }
        }

    }
    public class HangmanService
    {
        private static String[] word;
        private char[] selectWord;
        private Random random;
        List<Char> showUnder = new List<Char>();


        int amountleft;

        public enum Status { Correct = 0, Incorrect = 1, again = 2 };

        public HangmanService()
        {
            random = new Random();
            word = new[] { "ironman", "hulk", "batman" };
        }

        public void Restart()
        {
            //clear ค่าเก่า
            showUnder.Clear();
            amountleft = 10;


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