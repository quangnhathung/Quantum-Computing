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

        private static string DivideBy2(string num)
        {
            StringBuilder sb = new StringBuilder();
            int carry = 0;

            foreach (char c in num)
            {
                int cur = carry * 10 + (c - '0');
                sb.Append(cur / 2);
                carry = cur % 2;
            }
            return sb.ToString().TrimStart('0').Length == 0 ? "0" : sb.ToString().TrimStart('0');
        }

        private static string DivideBy6(string num)
        {
            StringBuilder sb = new StringBuilder();
            int carry = 0;

            foreach (char c in num)
            {
                int cur = carry * 10 + (c - '0');
                sb.Append(cur / 6);
                carry = cur % 6;
            }
            return sb.ToString().TrimStart('0').Length == 0 ? "0" : sb.ToString().TrimStart('0');
        }

        public static string ToomCook3(string x, string y)
        {
            x = x.TrimStart('0');
            y = y.TrimStart('0');

            if (x == "" || y == "") return "0";

            // Nếu nhỏ thì dùng nhân chuẩn cho nhanh
            if (x.Length < 600 || y.Length < 600)
                return MultiplyBigInt(x, y);

            // Pad chiều dài
            int n = Math.Max(x.Length, y.Length);
            int m = (n + 2) / 3;
            x = x.PadLeft(3 * m, '0');
            y = y.PadLeft(3 * m, '0');

            // Tách 3 đoạn
            string x2 = x.Substring(0, m);
            string x1 = x.Substring(m, m);
            string x0 = x.Substring(2 * m, m);

            string y2 = y.Substring(0, m);
            string y1 = y.Substring(m, m);
            string y0 = y.Substring(2 * m, m);

            // Tính 5 điểm
            // 1. v0 = x(0)*y(0)
            string v0 = Karatsuba(x0, y0);

            // 2. v1 = x(1)*y(1)
            string X1 = AddStrings(AddStrings(x0, x1), x2);
            string Y1 = AddStrings(AddStrings(y0, y1), y2);
            string v1 = Karatsuba(X1, Y1);

            // 3. v_1 = x(-1)*y(-1)
            string X_1 = AddStrings(SubStrings(x0, x1), x2);
            string Y_1 = AddStrings(SubStrings(y0, y1), y2);
            string vneg1 = Karatsuba(X_1, Y_1);

            // 4. v2 = x(2)*y(2)
            string X2 = AddStrings(AddStrings(x0, MultiplySingleDigit(x1, 2)), MultiplySingleDigit(x2, 4));
            string Y2 = AddStrings(AddStrings(y0, MultiplySingleDigit(y1, 2)), MultiplySingleDigit(y2, 4));
            string v2 = Karatsuba(X2, Y2);

            // 5. v_inf = x2*y2
            string vInf = Karatsuba(x2, y2);

            // ──────────────────────────────
            // INTERPOLATION
            // ──────────────────────────────

            string t1 = SubStrings(v1, vneg1);                  // (v1 - v_-1)
            string t2 = SubStrings(v2, vneg1);                  // (v2 - v_-1)

            string m1 = SubStrings(t1, MultiplySingleDigit(v0, 2));
            m1 = DivideBy2(m1);

            string m2 = SubStrings(t2, MultiplySingleDigit(v1, 2));
            m2 = DivideBy6(m2);

            string m3 = SubStrings(vneg1, v0);

            string r0 = v0;
            string r4 = vInf;
            string r3 = SubStrings(SubStrings(m2, m1), m3);
            string r1 = SubStrings(m1, r3);
            string r2 = SubStrings(SubStrings(m3, r1), r4);

            // ──────────────────────────────
            // GHÉP KẾT QUẢ: shift theo B = 10^m
            // ──────────────────────────────
            string B = new string('0', m);

            string result =
                AddStrings(
                    AddStrings(
                        AddStrings(
                            AddStrings(
                                r0,
                                r1 + B
                            ),
                            r2 + B + B
                        ),
                        r3 + B + B + B
                    ),
                    r4 + B + B + B + B
                );

            return result.TrimStart('0').Length == 0 ? "0" : result.TrimStart('0');
        }

    }
}
