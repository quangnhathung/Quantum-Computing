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
            // format chieu dai 2 so
            int maxLen = Math.Max(num1.Length, num2.Length);
            num1 = num1.PadLeft(maxLen, '0');
            num2 = num2.PadLeft(maxLen, '0');

            int carry = 0;
            string result = "";


            for (int i = maxLen - 1; i >= 0; i--)
            {
                int sum = (num1[i] - '0') + (num2[i] - '0') + carry;
                result = (sum % 10).ToString() + result;
                carry = sum / 10;
            }

            if (carry > 0)
                result = carry.ToString() + result;

            // xoa so 0 thua
            return result.TrimStart('0').Length == 0 ? "0" : result.TrimStart('0');
        }

        // Hàm trừ hai số lớn (num1 >= num2)
        public static string SubStrings(string num1, string num2)
        {
            int maxLen = Math.Max(num1.Length, num2.Length);
            num1 = num1.PadLeft(maxLen, '0');
            num2 = num2.PadLeft(maxLen, '0');

            int borrow = 0;
            string result = "";

            for (int i = maxLen - 1; i >= 0; i--)
            {
                int diff = (num1[i] - '0') - (num2[i] - '0') - borrow;
                if (diff < 0)
                {
                    diff += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }
                result = diff.ToString() + result;
            }

            return result.TrimStart('0').Length == 0 ? "0" : result.TrimStart('0');
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
            if (x.Length <= 3 && y.Length <= 3)
            {
                return ((int.Parse(x) * int.Parse(y)).ToString());
            }

            int n = Math.Max(x.Length, y.Length);
            if (n % 2 != 0) n++;
            x = x.PadLeft(n, '0');
            y = y.PadLeft(n, '0');

            int m = n / 2;

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

            // rs = ac * 10^(2m) + adbc * 10^m + bd
            string part1 = ac + new string('0', 2 * m);
            string part2 = adbc + new string('0', m);
            string result = AddStrings(AddStrings(part1, part2), bd);

            return result.TrimStart('0').Length == 0 ? "0" : result.TrimStart('0');
        }
    }
}
