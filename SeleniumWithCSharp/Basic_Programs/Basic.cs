using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Programs
{
    public class Basic
    {
        // print Prime Numbers till 10
        [Test]
        public void Prime_Number()
        {
            for (int num = 2; num <= 10; num++)   // Iterate through numbers from 2 to 10
            {
                bool isPrime = true;
                for (int i = 2; i < num; i++)  // Check divisibility by numbers from 2 to num-1
                {
                    if (num % i == 0)
                    {
                        isPrime = false;  // num is divisible by i, so it's not prime
                        break;
                    }
                }
                if (isPrime)
                {
                    TestContext.Out.WriteLine(num);
                }
            }
        }


        // Prime Number Check
        [Test]
        public void Prime_Number_Check()
        {
            int num = 8;
            bool isPrime = true;
            for (int i = 2; i < num; i++)  // Check divisibility by numbers from 2 to num-1
            {
                if (num % i == 0)
                {
                    isPrime = false;  // num is divisible by i, so it's not prime
                    break;
                }
            }
            if (isPrime)
            {
                TestContext.Out.WriteLine(num + " is a prime number");
            }
            else
            {
                TestContext.Out.WriteLine(num + " is not a prime number");
            }
        }


    }
}
