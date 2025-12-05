using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace Quantum_Computation
{
    public partial class Form1 : Form
    {
        private string num1;
        private string num2;
        private string op;

        public Form1()
        {
            InitializeComponent();
            ScreenResult.Text = "";
            num1 = "";
            num2 = "";
            op = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text == "+" ||
                ScreenResult.Text == "*" || ScreenResult.Text == "-" ||
                ScreenResult.Text == "")
            {
                ScreenResult.Text = "";
            }
            ScreenResult.Text += "7";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text == "+" ||
                ScreenResult.Text == "*" || ScreenResult.Text == "-" ||
                ScreenResult.Text == "")
            {
                ScreenResult.Text = "";
            }
            ScreenResult.Text += "8";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text == "+" ||
                ScreenResult.Text == "*" || ScreenResult.Text == "-" ||
                 ScreenResult.Text == "")
            {
                ScreenResult.Text = "";
            }
            ScreenResult.Text += "9";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text == "+" ||
                ScreenResult.Text == "*" || ScreenResult.Text == "-" ||
                ScreenResult.Text == "")
            {
                ScreenResult.Text = "";
            }
            ScreenResult.Text += "4";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text == "+" ||
                ScreenResult.Text == "*" || ScreenResult.Text == "-" ||
                ScreenResult.Text == "")
            {
                ScreenResult.Text = "";
            }
            ScreenResult.Text += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text == "+" ||
                ScreenResult.Text == "*" || ScreenResult.Text == "-" ||
                ScreenResult.Text == "")
            {
                ScreenResult.Text = "";
            }
            ScreenResult.Text += "6";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text == "+" ||
                ScreenResult.Text == "*" || ScreenResult.Text == "-" ||
                ScreenResult.Text == "")
            {
                ScreenResult.Text = "";
            }
            ScreenResult.Text += "1";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text == "+" ||
                ScreenResult.Text == "*" || ScreenResult.Text == "-" ||
                ScreenResult.Text == "")
            {
                ScreenResult.Text = "";
            }
            ScreenResult.Text += "2";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text == "+" ||
                ScreenResult.Text == "*" || ScreenResult.Text == "-" ||
                ScreenResult.Text == "")
            {
                ScreenResult.Text = "";
            }
            ScreenResult.Text += "3";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text == "+" ||
                 ScreenResult.Text == "*" || ScreenResult.Text == "-" ||
                 ScreenResult.Text == "")
            {
                ScreenResult.Text = "";
            }
            ScreenResult.Text += "0";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ScreenResult.Text += ".";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text.Length == 0) return;
            string temp = ScreenResult.Text.Substring(0, ScreenResult.Text.Length - 1);
            ScreenResult.Text = temp;
        }

        private void ScreenResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            ScreenResult.Text = "";
            num1 = "";
            num2 = "";
            op = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text != "+" &&
                ScreenResult.Text != "*" && ScreenResult.Text != "-" &&
                ScreenResult.Text != "")
            {
                num1 = ScreenResult.Text;
                ScreenResult.Clear();
                ScreenResult.Text = "+";
                op = "+";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            op = "";
            if (ScreenResult.Text != "+" &&
                ScreenResult.Text != "*" && ScreenResult.Text != "-" &&
                ScreenResult.Text != "")
            {
                num1 = ScreenResult.Text;
                ScreenResult.Clear();
                ScreenResult.Text = "*";
                op = "*";
            }
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            //bắt đầu do thời gian thực thi
            Stopwatch sw = new Stopwatch();
            num2 = ScreenResult.Text;
            if (num1 == "" || num2 == "" || op == "")
            {
                ScreenResult.Text = "Phép tính lỗi";
                await Task.Delay(1500);
                ScreenResult.Text = "";
                return;
            }
            string temp = "";
            switch (op)
            {
                case "*":
                    ScreenResult.Clear();
                    sw.Start();
                    temp = karatsuba.Karatsuba(num1, num2);
                    sw.Stop();
                    ScreenResult.Text = temp;
                    break;
                case "+":
                    ScreenResult.Clear();
                    sw.Start();
                    temp = karatsuba.AddStrings(num1, num2);
                    sw.Stop();
                    ScreenResult.Text = temp;
                    break;
                case "-":
                    ScreenResult.Clear();
                    if (karatsuba.CompareBigIntegers(num1, num2) == -1)
                    {
                        ScreenResult.Text = "ST phải bé hơn SBT";
                        await Task.Delay(3500);
                        ScreenResult.Text = "";
                        button17_Click(sender, e);
                        return;
                    }
                    sw.Start();
                    temp = karatsuba.SubStrings(num1, num2);
                    sw.Stop();
                    ScreenResult.Text = temp;
                    break;
            }
            op = "";
            label2.Text = $"Thời gian thực hiện: {sw.ElapsedTicks * (1_000_000.0 / Stopwatch.Frequency)} µs"; //microsecond_1/1000000 giây
            return;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ScreenResult.Text != "+" &&
                ScreenResult.Text != "*" && ScreenResult.Text != "-" &&
                ScreenResult.Text != "")
            {
                num1 = ScreenResult.Text;
                ScreenResult.Clear();
                ScreenResult.Text = "-";
                op = "-";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
