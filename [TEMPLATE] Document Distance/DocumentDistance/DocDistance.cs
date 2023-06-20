using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DocumentDistance
{
    internal class DocDistance
    {
        private static Dictionary<string, double> file1 = new Dictionary<string, double>();
        private static Dictionary<string, double> file2 = new Dictionary<string, double>();
        private static StringBuilder state = new StringBuilder();
        private static String singleString;
        private static long count1=0, count2=0;
        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO
        // *****************************************
        /// <summary>
        /// Write an efficient algorithm to calculate the distance between two documents
        /// </summary>
        /// <param name="doc1FilePath">File path of 1st document</param>
        /// <param name="doc2FilePath">File path of 2nd document</param>
        /// <returns>The angle (in degree) between the 2 documents</returns>
        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {

            string D1 = File.ReadAllText(doc1FilePath);
            string D2 = File.ReadAllText(doc2FilePath);
            D1=D1.ToLower();
            D2=D2.ToLower();
            count1=splitt(D1, file1);
            count2=splitt(D2, file2);
            double s=0;
            if (count1 <= count2)
                s = calc1(file2,file1);
            else
                s = calc1(file1, file2);


            double angle = Math.Acos(s);

            file1.Clear();
            file2.Clear();
           
            angle = angle * (180.0 / Math.PI);


            return angle;
        }

        public static long splitt(string D1, Dictionary<string, double> file)
        {
            int count = 0, count_loop;
            long count_size=0;
            long size = D1.Length;
            char ch_i,ch_count;
            while (count < size)
            {
                state.Clear();
                ch_count = D1[count];
                if (Char.IsLetterOrDigit(ch_count))
                {
                    state.Append(ch_count);
                    count++;
                    count_loop = count;
                    for (int i = count_loop; i < size; i++)
                    {
                        ch_i = D1[i];
                        if (Char.IsLetterOrDigit(ch_i))
                        {
                            state.Append(ch_i);
                            count++;
                        }
                        else
                        {
                            count++;
                            break;
                        }
                    }
                }
                else
                {
                    count++;
                }
                singleString = state.ToString();

                if (singleString == "")
                {
                    continue;
                }
                if (file.ContainsKey(singleString))
                {
                    file[singleString]++;
                }
                else
                {
                    file.Add(singleString, 1);
                    count_size++;
                }
            }
             return count_size;
        }
        public static double calc1(Dictionary<string, double> file1, Dictionary<string, double> file2)
        {
            double sum_up = 0, a = 0, sum_down = 0, sum1 = 0, sum2 = 0;
            foreach (var key in file1)
            {
                if (file2.ContainsKey(key.Key))
                {
                    sum_up += (file2[key.Key] * key.Value);
                }
                a = key.Value;
                sum2 += a*a ;
               
            }
            if(sum_up==0.0)
                return 0.0;
            sum1 = file2.Sum(x => (Math.Pow(x.Value,2)));
            sum_down = Math.Sqrt(sum1 * sum2);
            return sum_up / sum_down;
        }
    }
}