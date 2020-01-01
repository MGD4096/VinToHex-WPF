using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VinToHex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (vhex.Text.Length != 38 && vhex.Text.Length != 26)
            {
                vinhexerror.Content = "Incorect Format"+vhex.Text.Length;
                vinhexerror.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }
            vinhexerror.Content = "";
            System.Numerics.BigInteger bi = BigInteger.Parse(vhex.Text.Replace(" ",string.Empty), System.Globalization.NumberStyles.HexNumber);
            String va = "";
            for (int i=16;i>=0;i--)
            {
                var v1=BigInteger.Pow(new BigInteger(64), i);
                var v2=BigInteger.Divide(bi, v1);
                
                if((int)v2 > 9)
                {
                    va += Convert.ToChar((int)v2+55);
                }
                else
                {
                    va += (int)v2;
                }
                bi = BigInteger.Subtract(bi, BigInteger.Multiply(v2, v1));
            
            }
            vascii.Text = va;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (vascii.Text.Length != 17)
            {
                vinasciierror.Content = "Incorect Format";
                vinasciierror.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }
            vinasciierror.Content = "";
            string ascii = vascii.Text;
            BigInteger bi = new BigInteger(chartosint(ascii[16]));
            for(int i = 1; i <= 16; i++)
            {
                int v1 = chartosint((int)ascii[16-i]);
                var v2 = BigInteger.Pow(new BigInteger(64), i);
                bi = BigInteger.Add(BigInteger.Multiply(v1, v2),bi);
            }
            var va = bi.ToString("X");
            var vb = "";
            for(int i = 0; i < va.Length; i++)
            {
                if (i % 2 == 1) {
                    vb += (char)va[i]+" ";
                }
                else
                {
                    vb += (char)va[i];
                }
            }
            vhex.Text = vb.Trim();
            
        }
        private int chartosint(int j)
        {
            if (47 < j && j < 58)
            {
                return j-48;
            }
            else
            {
                return j - 55;
            }
        }
    }
}
