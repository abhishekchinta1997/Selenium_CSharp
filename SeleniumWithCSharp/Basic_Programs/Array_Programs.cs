using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Programs
{
    public class Array_Programs
    {
        // Element Occurances in an Array
        [Test]
        public void Array_Occurances()
        {
            int[] arr = {1,2,2,3,6,9,4,3,8 };
            for (int i = 0; i < arr.Length; i++)
            {
                int c = arr[i];
                if (c != int.MinValue)
                {
                    int count = 1;
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if (c == arr[j])
                        {
                            count++;
                            arr[j] = int.MinValue;
                        }
                    }
                    TestContext.WriteLine(c + " appeared " + count + " times");
                }
            }
        }
    }
}
