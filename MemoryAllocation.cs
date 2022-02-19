using System;

namespace MemoryAllocation
{
    public class Program
    {
        static void Main(String[] args)
        {

            Console.Write("Enter number of memory holes: ");
            var nu_mh = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter number of memory requests: ");
            var nu_mre = Convert.ToInt32(Console.ReadLine());

            int[,] FirstFitMatrix = new int[nu_mh + 1 , nu_mre + 1];
            int[,] BestFitMatrix = new int[nu_mh + 1, nu_mre + 1];
            int[,] WorstFitMatrix = new int[nu_mh + 1, nu_mre + 1];

            Console.WriteLine("Memory holes: ");
            int j = 0;
            FirstFitMatrix[0, j] = 0;
            BestFitMatrix[0, j] = 0;
            WorstFitMatrix[0, j] = 0;
            for (int i = 1; i < nu_mh + 1; i++)
            {
                var hole_element = Convert.ToInt32(Console.ReadLine());
                FirstFitMatrix[i, j] = hole_element;
                BestFitMatrix[i, j] = hole_element;
                WorstFitMatrix[i, j] = hole_element;
            }

            Console.WriteLine("Memory Requests: ");
            int k = 0;
            for (int i = 1; i < nu_mre + 1; i++)
            {
                var request_element = Convert.ToInt32(Console.ReadLine());
                FirstFitMatrix[k, i] = request_element;
                BestFitMatrix[k, i] = request_element;
                WorstFitMatrix[k, i] = request_element;
            }

            ShowMatrix("Initial state",FirstFitMatrix, nu_mh, nu_mre);

            FirstFit(FirstFitMatrix, nu_mh, nu_mre);

            BestFit(BestFitMatrix, nu_mh, nu_mre);

            WorstFit(WorstFitMatrix, nu_mh, nu_mre);

            void FirstFit(int[,] Mem_hole, int nu_mh, int nu_mre)
            {
                int flag = 0;
                int ex_frag_checker = 0;
                int ex_frag_flag = 0;
                int temp = 0;
                int main_value = 0;
                int ex_frag = 0;

                for (int l = 1; l < nu_mre + 1; l++)
                {
                    for (int i = 1; i < nu_mh + 1; i++)
                    {
                        if (Mem_hole[i, l - 1] >= Mem_hole[main_value, l] && flag == 0)
                        {
                            Mem_hole[i, l] = Mem_hole[i, l - 1] - Mem_hole[main_value, l];
                            temp = i;
                            flag++;
                        }
                        else
                        {
                            ex_frag_checker++;
                        }
                    }
                    if(ex_frag_checker == nu_mh && ex_frag_flag == 0)
                    {
                        for(int m = 1; m < nu_mh + 1; m++)
                        {
                            ex_frag = ex_frag + Mem_hole[m, l - 1];
                            Mem_hole[m, l] = 0;
                        }
                        ex_frag_flag++;
                    }
                    else
                    {
                        for (int m = 1; m < nu_mh + 1; m++)
                        {
                            if(m == temp)
                            {
                                Mem_hole[m, l] = Mem_hole[m, l];
                            }
                            else
                            {
                                Mem_hole[m, l] = Mem_hole[m, l - 1];
                            }
                        }
                    }
                    ex_frag_checker = 0;
                    flag = 0;
                }
                ShowMatrix("First Fit", Mem_hole, nu_mh, nu_mre);
                ShowExFrag(ex_frag);
            }



            void BestFit(int[,] Mem_hole, int nu_mh, int nu_mre)
            {
                int flag = 0;
                int ex_frag_checker = 0;
                int ex_frag_flag = 0;
                int temp = 0;
                int main_value = 0;
                int ex_frag = 0;
                var smallest = new List<int>();

                for (int l = 1; l < nu_mre + 1; l++)
                {
                    for (int i = 1; i < nu_mh + 1; i++)
                    {
                        if (Mem_hole[i, l - 1] >= Mem_hole[main_value, l])
                        {
                            smallest.Add(Mem_hole[i,l-1]);
                        }
                        else
                        {
                            ex_frag_checker++;
                        }
                    }
                    smallest.Sort();
                    for (int i = 1; i < nu_mh + 1; i++)
                    {
                        if(smallest.Count!= 0)
                        {
                            if(Mem_hole[i, l -1] == smallest[0] && flag == 0)
                            {
                                Mem_hole[i, l] = Mem_hole[i, l - 1] - Mem_hole[main_value, l];
                                temp = i;
                                flag++;
                            }
                        }
                    }

                    if (ex_frag_checker == nu_mh && ex_frag_flag == 0)
                    {
                        for (int m = 1; m < nu_mh + 1; m++)
                        {
                            ex_frag = ex_frag + Mem_hole[m, l - 1];
                            Mem_hole[m, l] = 0;
                        }
                        ex_frag_flag++;
                    }
                    else
                    {
                        for (int m = 1; m < nu_mh + 1; m++)
                        {
                            if (m == temp)
                            {
                                Mem_hole[m, l] = Mem_hole[m, l];
                            }
                            else
                            {
                                Mem_hole[m, l] = Mem_hole[m, l - 1];
                            }
                        }
                    }
                    ex_frag_checker = 0;
                    flag = 0;
                    smallest.Clear();
                }

                ShowMatrix("Best Fit", Mem_hole, nu_mh, nu_mre);
                ShowExFrag(ex_frag);
            }


            void WorstFit(int[,] Mem_hole, int nu_mh, int nu_mre)
            {
                int flag = 0;
                int ex_frag_checker = 0;
                int ex_frag_flag = 0;
                int temp = 0;
                int main_value = 0;
                int ex_frag = 0;
                var largest = new List<int>();

                for (int l = 1; l < nu_mre + 1; l++)
                {
                    for (int i = 1; i < nu_mh + 1; i++)
                    {
                        if (Mem_hole[i, l - 1] >= Mem_hole[main_value, l])
                        {
                            largest.Add(Mem_hole[i, l - 1]);
                        }
                        else
                        {
                            ex_frag_checker++;
                        }
                    }
                    largest.Sort();
                    largest.Reverse();
                    for (int i = 1; i < nu_mh + 1 ; i++)
                    {
                        if (largest.Count != 0)
                        {
                            if (Mem_hole[i, l - 1] == largest[0] && flag == 0)
                            {
                                Mem_hole[i, l] = Mem_hole[i, l - 1] - Mem_hole[main_value, l];
                                temp = i;
                                flag++;
                            }
                        }
                    }

                    if (ex_frag_checker == nu_mh && ex_frag_flag == 0)
                    {
                        for (int m = 1; m < nu_mh + 1; m++)
                        {
                            ex_frag = ex_frag + Mem_hole[m, l - 1];
                            Mem_hole[m, l] = 0;
                        }
                        ex_frag_flag++;
                    }
                    else
                    {
                        for (int m = 1; m < nu_mh + 1; m++)
                        {
                            if (m == temp)
                            {
                                Mem_hole[m, l] = Mem_hole[m, l];
                            }
                            else
                            {
                                Mem_hole[m, l] = Mem_hole[m, l - 1];
                            }
                        }
                    }
                    ex_frag_checker = 0;
                    flag = 0;
                    largest.Clear();
                }

                ShowMatrix("Worst Fit", Mem_hole, nu_mh, nu_mre);
                ShowExFrag(ex_frag);
            }

            // Function to show matrix................
            void ShowMatrix(String s, int [,] Mem_hole, int nu_mh, int nu_mre)
            {
                Console.WriteLine();
                Console.WriteLine($"{s} ");
                Console.WriteLine($"Step by step memory allocation situation in {s} ");
                int digit = 0;
                for (int i = 0; i < nu_mh + 1; i++)
                {
                    for (int n = 0; n < nu_mre + 1; n++)
                    {
                        var num = Mem_hole[i, n];
                        do
                        {
                            digit++;
                            num = num / 10;
                        }while(num != 0);

                        var space = 4 - digit;
                        // Considering 3 is the highest number of digits........... 
                        for(int z = 0; z < space; z++)
                        {
                            Console.Write(" ");
                        }
                        Console.Write($"{Mem_hole[i, n]}    ");
                        digit = 0;
                    }
                    Console.WriteLine();
                }
            }
            // Function to show external fragmentation................
            void ShowExFrag(int ex_frag)
            {
                if (ex_frag == 0)
                {
                    Console.WriteLine("No external fragementation.");
                }
                else
                {
                    Console.WriteLine($"External fragementation: {ex_frag}");
                }
            }
        }
    }
}