using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum_Computation
{
    class karatsuba
    {
        public static string AddStrings(string num1, string num2)
        {
            if (string.IsNullOrEmpty(num1)) return num2 ?? "0";
            if (string.IsNullOrEmpty(num2)) return num1 ?? "0";

            int i = num1.Length - 1;
            int j = num2.Length - 1;
            int carry = 0;

            int maxLen = Math.Max(num1.Length, num2.Length);
            StringBuilder sb = new StringBuilder(maxLen + 1);

            while (i >= 0 || j >= 0 || carry > 0)
            {
                int d1 = (i >= 0) ? num1[i] - '0' : 0;
                int d2 = (j >= 0) ? num2[j] - '0' : 0;

                int sum = d1 + d2 + carry;

                sb.Append(sum % 10);

                carry = sum / 10;

                i--;
                j--;
            }

            char[] resultChars = new char[sb.Length];
            for (int k = 0; k < sb.Length; k++)
            {
                resultChars[k] = sb[sb.Length - 1 - k];
            }

            return new string(resultChars);
        }

        // Hàm trừ hai số lớn (num1 >= num2)
        public static string SubStrings(string num1, string num2)
        {
            num1 = num1.TrimStart('0');
            num2 = num2.TrimStart('0');
            if (num2 == "") return num1 == "" ? "0" : num1;

            int i = num1.Length - 1;
            int j = num2.Length - 1;
            int borrow = 0;

            StringBuilder sb = new StringBuilder(num1.Length);

            while (i >= 0)
            {
                int d1 = num1[i--] - '0';
                int d2 = (j >= 0) ? num2[j--] - '0' : 0;

                int diff = d1 - d2 - borrow;

                if (diff < 0)
                {
                    diff += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }
                sb.Append(diff);
            }

            while (sb.Length > 1 && sb[sb.Length - 1] == '0')
            {
                sb.Length--;
            }

            char[] resultChars = new char[sb.Length];
            for (int k = 0; k < sb.Length; k++)
            {
                resultChars[k] = sb[sb.Length - 1 - k];
            }

            return new string(resultChars);
        }

        public static int CompareBigIntegers(string a, string b)
        {
            a = a.TrimStart('0');
            b = b.TrimStart('0');

            if (a.Length > b.Length)
                return 1;
            if (a.Length < b.Length)
                return -1;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > b[i])
                    return 1;
                if (a[i] < b[i])
                    return -1;
            }

            return 0;
        }

        // Nhân một chữ số với số lớn
        private static string MultiplySingleDigit(string num, int digit)
        {
            int carry = 0;
            string result = "";

            for (int i = num.Length - 1; i >= 0; i--)
            {
                int product = (num[i] - '0') * digit + carry;
                result = (product % 10).ToString() + result;
                carry = product / 10;
            }

            if (carry > 0)
                result = carry.ToString() + result;

            return result.TrimStart('0').Length == 0 ? "0" : result.TrimStart('0');
        }

        public static string Karatsuba(string x, string y)
        {
            //xoa so 0 dau
            x = x.TrimStart('0');
            y = y.TrimStart('0');

            if (x == "" || y == "")
                return "0";

            // Neu nho, nhan truc tiep
            if (x.Length < 800 || y.Length < 800)
            {
                return MultiplyBigInt(x, y);
            }

            int n = Math.Max(x.Length, y.Length);
            if (n % 2 != 0) n++;
            x = x.PadLeft(n, '0');
            y = y.PadLeft(n, '0');

            int m = n / 2;

            //chia làm 2 nửa
            string a = x.Substring(0, n - m);
            string b = x.Substring(n - m);
            string c = y.Substring(0, n - m);
            string d = y.Substring(n - m);

            string ac = Karatsuba(a, c);
            string bd = Karatsuba(b, d);
            string aPlusb = AddStrings(a, b);
            string cPlusd = AddStrings(c, d);
            string abcd = Karatsuba(aPlusb, cPlusd);

            string adbc = SubStrings(SubStrings(abcd, ac), bd);

            // rs = (ac * 10^(2m)) + ((a+b)(c+d) - ac – bd )* 10^m + bd
            string part1 = ac + new string('0', 2 * m);
            string part2 = adbc + new string('0', m);
            string result = AddStrings(AddStrings(part1, part2), bd);

            return result.TrimStart('0').Length == 0 ? "0" : result.TrimStart('0');
        }

        public static string MultiplyBigInt(string num1, string num2)
        {
            if (num1 == "0" || num2 == "0") return "0";

            int m = num1.Length;
            int n = num2.Length;


            int[] pos = new int[m + n];

            for (int i = m - 1; i >= 0; i--)
            {
                for (int j = n - 1; j >= 0; j--)
                {
                    int digit1 = num1[i] - '0';
                    int digit2 = num2[j] - '0';

                    int mul = digit1 * digit2;

                    int p1 = i + j;
                    int p2 = i + j + 1;

                    int sum = mul + pos[p2];

                    pos[p1] += sum / 10;
                    pos[p2] = sum % 10; 
                }
            }

            StringBuilder sb = new StringBuilder();

            foreach (int num in pos)
            {
                if (!(sb.Length == 0 && num == 0))
                {
                    sb.Append(num);
                }
            }
            return sb.Length == 0 ? "0" : sb.ToString();
        }

        private static readonly Random _random = new Random();


        public static string RandomBigInt(int n)
        {
            if (n <= 0) return "0";
            if (n == 1) return _random.Next(0, 10).ToString();

            StringBuilder sb = new StringBuilder();

            sb.Append(_random.Next(1, 10));

            for (int i = 1; i < n; i++)
            {
                sb.Append(_random.Next(0, 10));
            }

            return sb.ToString();
        }

    }
}
