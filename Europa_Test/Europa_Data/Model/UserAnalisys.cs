using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Europa_Data.Model
{
    public class UserAnalisys
    {
        public UserAnalisys(string nameAnalize, long valueReached)
        {
            this.NameAnalize= nameAnalize;
            this.ValueReached = valueReached;
        }
        public string NameAnalize
        {
            get;
            set;
        }
        public long ValueReached
        {
            get;
            set;
        }
    }

    public class UserAnalisysCollection : Collection<UserAnalisys>
    {
        public UserAnalisysCollection()
        {
            
            Add(new UserAnalisys("Performance", Recalc()));
            Add(new UserAnalisys("Communication", Recalc()));
            Add(new UserAnalisys("Pro-Activity", Recalc()));
            Add(new UserAnalisys("Organization", Recalc()));
            Add(new UserAnalisys("Evolution", Recalc()));
        }

        public int Recalc()
        {
            Random rd = new Random();
            IList<int> primes = new List<int>(PrimeNumbers(1,50));
            int sum = 0;
            foreach (var item in primes)
                sum += item;
            return sum;
       
        }
        
        public static List<int> PrimeNumbers(int min, int max)
        {
            List<int> numbers = new List<int>();
            Random r = new Random();
            int num = r.Next(1, 25);

            for (int loop = 0; loop < 15; loop++)
            {
                int n = r.Next(1, 18);
                num = n >= num ? n + 1 : n;
                numbers.Add(num);
                //Console.WriteLine(num);
            }

            //List<int> numbers = Enumerable.Range(min, max).ToList();
            numbers = ShuffleList(numbers); 
            var primes = numbers.Where(x => x % 2 == 1).Where(x => x % 2 != 0).Take(25);
            return primes.ToList();
        }

        private static List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();
            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count);
                randomList.Add(inputList[randomIndex]); 
                inputList.RemoveAt(randomIndex); 
            }
            return randomList; 
        }
    }

}
