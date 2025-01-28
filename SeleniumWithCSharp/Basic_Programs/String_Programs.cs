namespace Basic_Programs
{
    public class String_Programs
    {
        // Character Occurances in a string
        [Test]
        public void Character_Occurances()
        {
            string str = "aaabbc";
            char[] strArray = str.ToCharArray();
            for(int i = 0; i<strArray.Length; i++)
            {
                char c = strArray[i];
                if (c != ' ' && c != '0')
                {
                    int count = 1;
                    for(int j = i+1; j<strArray.Length; j++)
                    {
                        if(c== strArray[j])
                        {
                            count++;
                            strArray[j] = '0';
                        }
                    }
                    TestContext.WriteLine(c + " appeared " + count + " times");
                }
            }
        }


        // Count duplicate chars in a string
        [Test]
        public void CountDuplicateCharsInString()
        {
            string str = "aaabbc";
            Dictionary<char, int> countChar = new Dictionary<char, int>();
            foreach (char c in str)
            {
                if (char.IsLetterOrDigit(c))
                {
                    if (countChar.ContainsKey(c))
                    {
                        countChar[c]++;
                    }
                    else
                    {
                        countChar.Add(c, 1);
                    }
                }
            }
            foreach(var entry in countChar)
            {
                if(entry.Value > 1)
                {
                    TestContext.WriteLine("Character: " + entry.Key + ", Count: " + entry.Value);
                }
            }
        }


        // Separate Digits and Letters
        [Test]
        public void SeparateDigitsAndLetters()
        {
            string str = "1a2b3c";
            string digits = "";
            string letters = "";
            foreach (char c in str)
            {
                if(char.IsDigit(c))
                {
                    digits = digits + c;
                }
                else if (char.IsLetter(c))
                {
                    letters = letters + c; 
                }
            }
            TestContext.WriteLine("Original String : " + str);
            TestContext.WriteLine("Digits : " + digits);
            TestContext.WriteLine("Letters : " + letters);
        }


        // Reverse String
        [Test]
        public void ReverseString()
        {
            string str = "1a2b3c";
            string revStr = "";
            for(int i = str.Length-1; i >= 0; i--)
            {
                revStr = revStr + str[i];
            }
            TestContext.WriteLine("Original String : " + str);
            TestContext.WriteLine("Reversed String : " + revStr);
        }


        // Reverse String
        [Test]
        public void CountChars()
        {
            string str = "Hello World!";
            int count = 0;
            for (int i=0; i<str.Length; i++)
            {
                count++;
            }
            TestContext.WriteLine("Original String : " + str);
            TestContext.WriteLine("Count Chars : " + count);
        }


    }
}