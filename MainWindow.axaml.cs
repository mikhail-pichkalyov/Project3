using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Resources;
using System;
using System.IO;

namespace Project3
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        //����� ���������� ����� ����� ������ ���� ���
        bool shift;
        bool point;
        bool pointlip;
        /*bool pointcheck;
        bool minuslip;
        int index;*/
        private void ClrButtonClick(object sender, RoutedEventArgs e)
        {
            if (Result.Text != "") Result.Text = "";//������ ���� ������
        }
        private void txtACC_KeyPressUp(object sender, KeyEventArgs e)
        {
            if ((int)e.Key == 116 || (int)e.Key == 117)
                shift = false;
            if((Accuracy.Text!=null)&&(Accuracy.Text.Length>3))//���� ������������ ������������ ������ ����� ������ ������ ����� �������. �����, ���� � ������, �� �����?
            {
                Accuracy.Text = "1000";
            }
        }
        private void txtACC_KeyPressDown(object sender, KeyEventArgs e)//��� �� �� ��, ��� � ��� ����� � ��������, ������ ��������
        {
            if ((int)e.Key == 116 || (int)e.Key == 117)
                shift = true;

            if (((int)e.Key < 34 || (int)e.Key > 43) && ((int)e.Key < 74 || (int)e.Key > 83) || shift)
                e.Handled = true;
        }
        private void txtPassword_KeyPressUp(object sender, KeyEventArgs e)
        {
            if ((int)e.Key == 116 || (int)e.Key == 117)//���� ������ ����
                shift = false;
            if ((int)e.Key == 144||(int)e.Key == 145 || (int)e.Key == 142)//���� ������� ���������������� �����, ���������, ��� �� ���
            { 
                pointlip = false;//����� ��������
                
                if(Argument.Text.IndexOf('.')==-1)//���� ����� ��� ���
                {
                        point = false;
                    if (Argument.Text[Argument.Text.Length - 1]<'0'|| Argument.Text[Argument.Text.Length - 1] > '9')
                        Argument.Text = Argument.Text.Substring(0, Argument.Text.Length - 1);//������� ��������� ������ ������, ��� ������ ���� ���� �����, �� ��� �� ���
                }
                    else
                    {
                    point = true;
                    if (Argument.Text[0]=='.')
                        Argument.Text = "0" + Argument.Text;
                    
                }
            }
            if (((int)e.Key == 144 || (int)e.Key == 145 || (int)e.Key == 142|| (int)e.Key == 143)&& Argument.Text.Length>1)
                if (Argument.Text[0] == '-' && Argument.Text[1]=='.')
                        Argument.Text = "-0" + Argument.Text.Substring(1);
        }
        private void txtPassword_KeyPressDown(object sender, KeyEventArgs e)//������ �� ������ ��� ����� �����
        {
            if ((int)e.Key == 116 || (int)e.Key == 117)//����� ��� ������� ���� ������ ���� ������ �� �����
                shift = true;
            //���� �������, ���� ��� �� �����, �� ������ ��� ������������� �����, ����� ���� ��� ����� �� ������ ��������
            if (((((int)e.Key < 34 || (int)e.Key > 43)&&((int)e.Key < 74 || (int)e.Key > 83) && ((int)e.Key != 144 && (int)e.Key != 145 && (int)e.Key != 142 || (Argument.Text!=null&&Argument.Text.IndexOf('.') != -1)) || shift || pointlip) && (((int)e.Key != 143 || Argument.SelectionStart!=0)/*||minuslip*/))||(Argument.Text!=null&&Argument.SelectionStart<= Argument.Text.IndexOf('-')))
                e.Handled = true;
            if ((int)e.Key == 144||(int)e.Key == 145 || (int)e.Key == 142)//���� ��� ��������� �������, ������������ �����
            { pointlip = true;  }//��������������, ��� ����� ��� ��������������, � ���� ����� ������������, �� ��������������, ����� ������ �� ���������, ���� ����� ��������
        }
        private void CalcButtonClick(object sender, RoutedEventArgs e)
        {
            string arg = Argument.Text;//�������� �����
            string acr = Accuracy.Text;//�������� �����
            string com = "";
            int acur;//�������� � �����
            if(arg!=null)
            {
                if((arg.Length>2)||((arg!="-.")&&(arg!=".")&&(arg!="-")))
                {
                if (acr == null)
                    acur = 3;
                else
                    acur = int.Parse(acr);
                if (arg[0]=='-')//���� ������ �� ������������ �����
                {
                    com = "i";
                    arg= arg.Substring(1);
                    if(arg=="0")
                    {
                        com = "";
                    }
                }
                Result.Text=LongDigits.sqrt(arg, acur)+com;
                }
                
            }
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LangSelector!=null)
            switch (LangSelector.SelectedIndex)
            {
                case 1: //�������
                        Accuracy.Watermark = Russian.ResourceManager.GetString("accuracy");
                    Argument.Watermark = Russian.ResourceManager.GetString("argument");
                        CalcBTN.Content = Russian.ResourceManager.GetString("calculate");
                        Result.Watermark = Russian.ResourceManager.GetString("result");
                        ClrBtn.Content = Russian.ResourceManager.GetString("clear");
                        Help.Text= Russian.ResourceManager.GetString("help")+"\nadveneksanov@gmail.com";
                        ClrBtn.Width = 180;
                        break;
                case 0: //����������
                        Accuracy.Watermark = English.ResourceManager.GetString("accuracy");
                        Argument.Watermark = English.ResourceManager.GetString("argument");
                        CalcBTN.Content = English.ResourceManager.GetString("calculate");
                        Result.Watermark = English.ResourceManager.GetString("result");
                        ClrBtn.Content = English.ResourceManager.GetString("clear");
                        Help.Text = English.ResourceManager.GetString("help") + "\nadveneksanov@gmail.com";
                        ClrBtn.Width = 160;
                        break; 
                case 2:  //��������
                        Accuracy.Watermark = German.ResourceManager.GetString("accuracy");
                        Argument.Watermark = German.ResourceManager.GetString("argument");
                        CalcBTN.Content = German.ResourceManager.GetString("calculate");
                        Result.Watermark = German.ResourceManager.GetString("result");
                        ClrBtn.Content = German.ResourceManager.GetString("clear");
                        Help.Text = German.ResourceManager.GetString("help") + "\nadveneksanov@gmail.com";
                        ClrBtn.Width = 160;
                        break;
            }
        }



    }
    class LongDigits
    {
        string digits;
        public LongDigits(string digits)//����� ��� ������� ����������
        {
            this.digits = digits;
        }
        static int[] ToArray(LongDigits input)//������������ � ������
        {
            int[] result = new int[input.digits.Length];
            for (int i = 0; i < input.digits.Length; i++)
            {
                string tmp = input.digits.Substring(i, 1);
                result[i] = int.Parse(tmp);
            }
            return result;//���������� ������� ������ �����
        }
        public LongDigits DeleteNulls()//������� ������ ���� � ������
        {
            while ((digits[0] == '0')&&(digits.Length>1))
            {
                digits = digits.Substring(1);
            }
            return new LongDigits(digits);
        }
        public static bool operator >(LongDigits inFirst, LongDigits inSecond)//��� � ����� ���� ��� ��������������� ����������. ���� ����� �����-�� �������, �������, �������� ��� ������, ��� � ������ ������ � ��� ��� �� ����� ����
        {
            inFirst = inFirst.DeleteNulls();
            inSecond = inSecond.DeleteNulls(); 
            if (inFirst.digits.Length > inSecond.digits.Length) return true;//���� ���� ������� �������, �� ������
            else if (inFirst.digits.Length < inSecond.digits.Length) return false;
            else//���� ������ ������ �����
            {
                int i = 1;
                if (inFirst.digits[0] > inSecond.digits[0]) return true;
                else if (inFirst.digits[0] < inSecond.digits[0]) return false;
                else//���� ������� �����, ���������� �����
                {
                    while ((i < inFirst.digits.Length) && (inFirst.digits[i - 1] == inSecond.digits[i - 1]))//����, ���� ������ �� ��������
                    {
                        {
                            if (inFirst.digits[i] > inSecond.digits[i]) return true;//���������� ������� ��������
                            else if (inFirst.digits[i] < inSecond.digits[i]) return false;//��������
                            else i++;//��������� ������
                        }
                    }

                }
                return false;//� ��������� ������ - �����
            }
        }
        public static bool operator <(LongDigits inFirst, LongDigits inSecond)
        {
            inFirst = inFirst.DeleteNulls();
            inSecond = inSecond.DeleteNulls(); 
            if (inFirst.digits.Length < inSecond.digits.Length) return true;//���� ���� ����� ������ �������
            else if (inFirst.digits.Length > inSecond.digits.Length) return false;
            else
            {
                int i = 1;
                if (inFirst.digits[0] < inSecond.digits[0]) return true;
                else if (inFirst.digits[0] > inSecond.digits[0]) return false;
                else
                    while ((i < inFirst.digits.Length) && (inFirst.digits[i - 1] == inSecond.digits[i - 1]))//���� �� �������� ������ ����� � ������
                    {
                        if (inFirst.digits[i] < inSecond.digits[i]) return true;
                        else if (inFirst.digits[i] > inSecond.digits[i]) return false;
                        else i++;
                    }

                return false;
            }
        }
        public static bool operator ==(LongDigits inFirst, LongDigits inSecond)//��� �� �� �����, ��� � � ������ � ������
        {
            inFirst = inFirst.DeleteNulls();
            inSecond = inSecond.DeleteNulls(); 
            if (inFirst.digits.Length > inSecond.digits.Length) return false;
            else if (inFirst.digits.Length < inSecond.digits.Length) return false;
            else
            {
                int i = 1;
                if (inFirst.digits[0] > inSecond.digits[0]) return false;
                else if (inFirst.digits[0] < inSecond.digits[0]) return false;
                else
                {
                    while ((i < inFirst.digits.Length) && (inFirst.digits[i - 1] == inSecond.digits[i - 1]))
                    {
                        {
                            if (inFirst.digits[i] > inSecond.digits[i]) return false;
                            else if (inFirst.digits[i] < inSecond.digits[i]) return false;
                            else i++;
                        }
                    }
                }
                return true;
            }
        }
        public static bool operator !=(LongDigits inFirst, LongDigits inSecond)
        {
            if (inFirst == inSecond) return false;
            else return true;
        }
        public static bool operator >=(LongDigits inFirst, LongDigits inSecond)
        {
            if ((inFirst > inSecond) || (inSecond == inFirst)) return true;
            else return false;
        }
        public static bool operator <=(LongDigits inFirst, LongDigits inSecond)
        {
            if ((inFirst < inSecond) || (inSecond == inFirst)) return true;
            else return false;

        }
        public static LongDigits operator +(LongDigits inFirst, LongDigits inSecond)//��������
        {
            inFirst = inFirst.DeleteNulls();
            inSecond = inSecond.DeleteNulls(); 
            int[] one = ToArray(inFirst);//������������ ������ 
            int[] two = ToArray(inSecond);//������
            LongDigits result = new LongDigits("");//���������
            int temp = 0;//������� �������
            int l1 = inFirst.digits.Length - 1;//����� �������
            int l2 = inSecond.digits.Length - 1;//����� �������
            while ((l1 != -1) || (l2 != -1))
            {
                if ((l1 != -1) && (l2 != -1))
                {
                    result.digits = ((one[l1] + two[l2] + temp) % 10) + result.digits;//� ������ �������� ������ ������������ ����� � ����������� ������
                    temp = (one[l1] + two[l2] + temp) / 10;//��������� �������
                    l1--;//���������!
                    l2--;
                }
                else if (l1 == -1)
                {
                    result.digits = ((two[l2] + temp) % 10) + result.digits;
                    temp = (two[l2] + temp) / 10;
                    l2--;
                }
                else
                {
                    result.digits = ((one[l1] + temp) % 10) + result.digits;
                    temp = (one[l1] + temp) / 10;
                    l1--;
                }
            }
            if (temp != 0)//���� ���� �������
            {
                result.digits += temp;//����������
            }
            return result;
        }
        public static LongDigits operator -(LongDigits inFirst, LongDigits inSecond)//���������
        {
            inFirst = inFirst.DeleteNulls();
            inSecond = inSecond.DeleteNulls();
            if (inSecond == inFirst) return new LongDigits("0");//���� ������ �����, ���������� 0
            else
            {
                int[] one = ToArray(inFirst);
                int[] two = ToArray(inSecond);
                LongDigits result = new LongDigits("");
                int temp = 0;
                int l1 = inFirst.digits.Length - 1;
                int l2 = inSecond.digits.Length - 1;
                while ((l1 != -1) || (l2 != -1))
                {
                    if (l2 != -1)
                    {
                        if (one[l1] < two[l2])
                        { temp = 10; one[l1 - 1] = one[l1 - 1] - 1; }
                        else temp = 0;
                        result.digits = (one[l1] - two[l2] + temp) + result.digits;
                        l1--;
                        l2--;
                    }
                    else
                    {
                        result.digits = one[l1] + result.digits;
                        l1--;
                    }
                }
                result.DeleteNulls();

                return result;
            }
        }
        public static LongDigits operator *(LongDigits inFirst, LongDigits inSecond)
        {
            inFirst=inFirst.DeleteNulls();
            inSecond = inSecond.DeleteNulls();
            LongDigits result = new LongDigits("");
            int[] one = ToArray(inFirst);
            int[] two = ToArray(inSecond);
            int temp = 0;//�������
            int[] three = new int[inFirst.digits.Length + inSecond.digits.Length];//����� ������ �����, ������� ����� ��������� ���������
            for (int i = 0; i < inFirst.digits.Length; i++)
                for (int j = 0; j < inSecond.digits.Length; j++)
                    three[i + j] += one[i] * two[j];
            for (int i = inFirst.digits.Length + inSecond.digits.Length - 2; i >= 0; i--)
            {
                result.digits = ((three[i] + temp) % 10)+result.digits;
                temp = (three[i] + temp) / 10;
            }
            if (temp != 0)//���� ���-�� ��� ��������
            {
                result.digits = temp+result.digits;
            }
            return result;
        }
        public static string sqrt(string s, int nac)//������
        {

            LongDigits result = new LongDigits("");//�������� � ���� ������ ��� �����
            LongDigits t = new LongDigits(""); //
            string exresult = "";//�������� � ���� ������ � ������
            string doublepart = "";//����� ����� �����
            int i = 0, j = 0,tu;//����������, �� ������ ����� ��� ����� �����
            if (s.IndexOf('.') == -1)
                tu = s.Length;
            else tu = s.IndexOf('.');
            string intpart = s.Substring(0, tu);
            if (s.IndexOf('.') != -1) doublepart = s.Substring(s.IndexOf('.') + 1);//��������� ����� �� ����� � ������� �����
            while (doublepart.Length < nac * 2)//��������� ������ ���� � ������� �����, ����� � ����������� ����� ����, ��� ����������
            {
                doublepart += "0";
            }
            //���� � ����� ����� �������� ���������� ����, �������� ���� ������ �����
            if (intpart.Length % 2 == 1) { t = new LongDigits(Convert.ToString(s[0])); intpart = intpart.Substring(1); }
            //���� ������,�� 2 ������
            else { t = new LongDigits(Convert.ToString(s[0]) + Convert.ToString(s[1])); intpart = intpart.Substring(2); }
            int k = 0;
            if (Convert.ToInt32(t.digits) > 0)
            {
                do { k++; } while (k * k <= Convert.ToInt32(t.digits));//���������� ���������� � ����� �������� ������ �����
                k--;
            }
            result = new LongDigits(Convert.ToString(k));//������ � ���������
            t = t - new LongDigits(Convert.ToString(k * k));//�������� �� �����, ������� ������� ����� ����������
            while (intpart.Length > 0)//���� �� ����������� ����� �����
            {
                t.digits += Convert.ToString(intpart[0]) + Convert.ToString(intpart[1]);//���������� ������ 2 ����� � ��������
                LongDigits b = result * new LongDigits("2");//�������� ��������� �� 2
                LongDigits f;
                int l = 0;
                do
                {
                    l++;//���������� �����, ������� ��� ��������� ��� ���������� �����, ������ ��������� 
                    f = new LongDigits(b.digits + Convert.ToString(l)) * new LongDigits(Convert.ToString(l));
                } while (f <= t);
                l--;
                
                result.digits = result.digits+Convert.ToString(l);//���������� � ��������� ��������� �����
                LongDigits f1 = new LongDigits(b.digits + Convert.ToString(l));
                LongDigits f2 =new LongDigits(Convert.ToString(l));
                f = f1 * f2;
                t = t - f;//�������� �� ����� ������������ ��������� �����
                intpart = intpart.Substring(2);//������� ������ 2 ����� �� ����� �����
            }

            if (nac > 0) exresult = result.digits + '.';//���� ���������� ������ ����� ������� ������ �����, ������ �����
            else exresult = result.digits;
            while (j < nac)//���� �� ����������� ����� ����� �������
            {   //����� ����� �� ��, ��� � ���� � ������ ����� �� ����� ����������
                t.digits += Convert.ToString(doublepart[0]) + Convert.ToString(doublepart[1]);
                LongDigits b = result * new LongDigits("2");
                LongDigits f;
                int l = 0;
                do
                {
                    l++;
                    f = new LongDigits(b.digits + Convert.ToString(l)) * new LongDigits(Convert.ToString(l));
                } while (f <= t);
                l--;
                result.digits = result.digits + Convert.ToString(l);
                exresult += Convert.ToString(l);//���������� ������������ ��� � ��������� ���������
                f= new LongDigits(b.digits + Convert.ToString(l)) * new LongDigits(Convert.ToString(l));
                t = t - f;
                doublepart = doublepart.Substring(2);
                j++;//���������� ������ ����� ������� �������������
            }
            return exresult;
        }
    }
}