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

        //метод вычисления корня можно писать прям тут
        bool shift;
        bool point;
        bool pointlip;
        /*bool pointcheck;
        bool minuslip;
        int index;*/
        private void ClrButtonClick(object sender, RoutedEventArgs e)
        {
            if (Result.Text != "") Result.Text = "";//чистим окно вывода
        }
        private void txtACC_KeyPressUp(object sender, KeyEventArgs e)
        {
            if ((int)e.Key == 116 || (int)e.Key == 117)
                shift = false;
            if((Accuracy.Text!=null)&&(Accuracy.Text.Length>3))//если пользователь намеревается ввести более тысячи знаков после запятой. Можно, кнчн и больше, но зачем?
            {
                Accuracy.Text = "1000";
            }
        }
        private void txtACC_KeyPressDown(object sender, KeyEventArgs e)//тут всё то же, что и при вводе в Аргумент, только поменьше
        {
            if ((int)e.Key == 116 || (int)e.Key == 117)
                shift = true;

            if (((int)e.Key < 34 || (int)e.Key > 43) && ((int)e.Key < 74 || (int)e.Key > 83) || shift)
                e.Handled = true;
        }
        private void txtPassword_KeyPressUp(object sender, KeyEventArgs e)
        {
            if ((int)e.Key == 116 || (int)e.Key == 117)//если отжали шифт
                shift = false;
            if ((int)e.Key == 144||(int)e.Key == 145 || (int)e.Key == 142)//если введена предположительно точка, проверяем, она ли это
            { 
                pointlip = false;//точка отлипает
                
                if(Argument.Text.IndexOf('.')==-1)//если точки ещё нет
                {
                        point = false;
                    if (Argument.Text[Argument.Text.Length - 1]<'0'|| Argument.Text[Argument.Text.Length - 1] > '9')
                        Argument.Text = Argument.Text.Substring(0, Argument.Text.Length - 1);//удаляем последний символ строки, это должна была быть точка, но это не она
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
        private void txtPassword_KeyPressDown(object sender, KeyEventArgs e)//защита от дурака при вводе числа
        {
            if ((int)e.Key == 116 || (int)e.Key == 117)//чтобы при зажатии шифт нельзя было нажать на цифры
                shift = true;
            //ниже условие, если это не цифра, не первый раз встречающаяся точка, зажат шифт или минус не первым символом
            if (((((int)e.Key < 34 || (int)e.Key > 43)&&((int)e.Key < 74 || (int)e.Key > 83) && ((int)e.Key != 144 && (int)e.Key != 145 && (int)e.Key != 142 || (Argument.Text!=null&&Argument.Text.IndexOf('.') != -1)) || shift || pointlip) && (((int)e.Key != 143 || Argument.SelectionStart!=0)/*||minuslip*/))||(Argument.Text!=null&&Argument.SelectionStart<= Argument.Text.IndexOf('-')))
                e.Handled = true;
            if ((int)e.Key == 144||(int)e.Key == 145 || (int)e.Key == 142)//если это возможные клавиши, записывающие точку
            { pointlip = true;  }//просчитывается, что точка уже использовалась, а если точка удерживается, то просчитывается, чтобы ничего не вводилось, пока точка залипает
        }
        private void CalcButtonClick(object sender, RoutedEventArgs e)
        {
            string arg = Argument.Text;//Аргумент текст
            string acr = Accuracy.Text;//точность текст
            string com = "";
            int acur;//точность в чилсе
            if(arg!=null)
            {
                if((arg.Length>2)||((arg!="-.")&&(arg!=".")&&(arg!="-")))
                {
                if (acr == null)
                    acur = 3;
                else
                    acur = int.Parse(acr);
                if (arg[0]=='-')//если корень из комплексного числа
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
                case 1: //русский
                        Accuracy.Watermark = Russian.ResourceManager.GetString("accuracy");
                    Argument.Watermark = Russian.ResourceManager.GetString("argument");
                        CalcBTN.Content = Russian.ResourceManager.GetString("calculate");
                        Result.Watermark = Russian.ResourceManager.GetString("result");
                        ClrBtn.Content = Russian.ResourceManager.GetString("clear");
                        Help.Text= Russian.ResourceManager.GetString("help")+"\nadveneksanov@gmail.com";
                        ClrBtn.Width = 180;
                        break;
                case 0: //Английский
                        Accuracy.Watermark = English.ResourceManager.GetString("accuracy");
                        Argument.Watermark = English.ResourceManager.GetString("argument");
                        CalcBTN.Content = English.ResourceManager.GetString("calculate");
                        Result.Watermark = English.ResourceManager.GetString("result");
                        ClrBtn.Content = English.ResourceManager.GetString("clear");
                        Help.Text = English.ResourceManager.GetString("help") + "\nadveneksanov@gmail.com";
                        ClrBtn.Width = 160;
                        break; 
                case 2:  //Немецкий
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
        public LongDigits(string digits)//класс для длинной арифметики
        {
            this.digits = digits;
        }
        static int[] ToArray(LongDigits input)//конвертивуем в массив
        {
            int[] result = new int[input.digits.Length];
            for (int i = 0; i < input.digits.Length; i++)
            {
                string tmp = input.digits.Substring(i, 1);
                result[i] = int.Parse(tmp);
            }
            return result;//возвращаем готовый массив чисел
        }
        public LongDigits DeleteNulls()//удаляем лишние нули в начале
        {
            while ((digits[0] == '0')&&(digits.Length>1))
            {
                digits = digits.Substring(1);
            }
            return new LongDigits(digits);
        }
        public static bool operator >(LongDigits inFirst, LongDigits inSecond)//тут и далее ниже идёт переопределение операторов. Если будут какие-то вопросы, задавай, комменты там толкьо, где я считаю нужным и где они до этого были
        {
            inFirst = inFirst.DeleteNulls();
            inSecond = inSecond.DeleteNulls(); 
            if (inFirst.digits.Length > inSecond.digits.Length) return true;//если одно длиннее другого, то больше
            else if (inFirst.digits.Length < inSecond.digits.Length) return false;
            else//если строки равной длины
            {
                int i = 1;
                if (inFirst.digits[0] > inSecond.digits[0]) return true;
                else if (inFirst.digits[0] < inSecond.digits[0]) return false;
                else//если символы равны, продолжаем поиск
                {
                    while ((i < inFirst.digits.Length) && (inFirst.digits[i - 1] == inSecond.digits[i - 1]))//ищем, пока строка не кончится
                    {
                        {
                            if (inFirst.digits[i] > inSecond.digits[i]) return true;//сравниваем текущие разяряды
                            else if (inFirst.digits[i] < inSecond.digits[i]) return false;//наоборот
                            else i++;//следующий разряд
                        }
                    }

                }
                return false;//в противном случае - равны
            }
        }
        public static bool operator <(LongDigits inFirst, LongDigits inSecond)
        {
            inFirst = inFirst.DeleteNulls();
            inSecond = inSecond.DeleteNulls(); 
            if (inFirst.digits.Length < inSecond.digits.Length) return true;//если одно число длинее другого
            else if (inFirst.digits.Length > inSecond.digits.Length) return false;
            else
            {
                int i = 1;
                if (inFirst.digits[0] < inSecond.digits[0]) return true;
                else if (inFirst.digits[0] > inSecond.digits[0]) return false;
                else
                    while ((i < inFirst.digits.Length) && (inFirst.digits[i - 1] == inSecond.digits[i - 1]))//пока не найдутся разные цифры в числах
                    {
                        if (inFirst.digits[i] < inSecond.digits[i]) return true;
                        else if (inFirst.digits[i] > inSecond.digits[i]) return false;
                        else i++;
                    }

                return false;
            }
        }
        public static bool operator ==(LongDigits inFirst, LongDigits inSecond)//тут то же самое, что и с больше и меньше
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
        public static LongDigits operator +(LongDigits inFirst, LongDigits inSecond)//сложение
        {
            inFirst = inFirst.DeleteNulls();
            inSecond = inSecond.DeleteNulls(); 
            int[] one = ToArray(inFirst);//конвертируем первое 
            int[] two = ToArray(inSecond);//второе
            LongDigits result = new LongDigits("");//результат
            int temp = 0;//перенос разряда
            int l1 = inFirst.digits.Length - 1;//длина первого
            int l2 = inSecond.digits.Length - 1;//длина второго
            while ((l1 != -1) || (l2 != -1))
            {
                if ((l1 != -1) && (l2 != -1))
                {
                    result.digits = ((one[l1] + two[l2] + temp) % 10) + result.digits;//в начало итоговой строки записывается сумма и переносимый разряд
                    temp = (one[l1] + two[l2] + temp) / 10;//обновляем перенос
                    l1--;//следующий!
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
            if (temp != 0)//если есть перенос
            {
                result.digits += temp;//дописываем
            }
            return result;
        }
        public static LongDigits operator -(LongDigits inFirst, LongDigits inSecond)//вычитание
        {
            inFirst = inFirst.DeleteNulls();
            inSecond = inSecond.DeleteNulls();
            if (inSecond == inFirst) return new LongDigits("0");//если строки равны, возвращаем 0
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
            int temp = 0;//перенос
            int[] three = new int[inFirst.digits.Length + inSecond.digits.Length];//новый массив чисел, который будет содержать результат
            for (int i = 0; i < inFirst.digits.Length; i++)
                for (int j = 0; j < inSecond.digits.Length; j++)
                    three[i + j] += one[i] * two[j];
            for (int i = inFirst.digits.Length + inSecond.digits.Length - 2; i >= 0; i--)
            {
                result.digits = ((three[i] + temp) % 10)+result.digits;
                temp = (three[i] + temp) / 10;
            }
            if (temp != 0)//если что-то ещё осталось
            {
                result.digits = temp+result.digits;
            }
            return result;
        }
        public static string sqrt(string s, int nac)//корень
        {

            LongDigits result = new LongDigits("");//резльтат в виде строки без точки
            LongDigits t = new LongDigits(""); //
            string exresult = "";//резльтат в виде строки с точкой
            string doublepart = "";//часть после точки
            int i = 0, j = 0,tu;//определяем, до какого места идёт целая часть
            if (s.IndexOf('.') == -1)
                tu = s.Length;
            else tu = s.IndexOf('.');
            string intpart = s.Substring(0, tu);
            if (s.IndexOf('.') != -1) doublepart = s.Substring(s.IndexOf('.') + 1);//разделяем число на целую и дробную части
            while (doublepart.Length < nac * 2)//добавляем лишние нули к дробной части, чтобы в вычислениях далее было, что записывать
            {
                doublepart += "0";
            }
            //если в целой части нечётное количество цифр, выделяем одну первую цифру
            if (intpart.Length % 2 == 1) { t = new LongDigits(Convert.ToString(s[0])); intpart = intpart.Substring(1); }
            //если чётное,то 2 первых
            else { t = new LongDigits(Convert.ToString(s[0]) + Convert.ToString(s[1])); intpart = intpart.Substring(2); }
            int k = 0;
            if (Convert.ToInt32(t.digits) > 0)
            {
                do { k++; } while (k * k <= Convert.ToInt32(t.digits));//нахождение ближайшего к числу квадрата меньше числа
                k--;
            }
            result = new LongDigits(Convert.ToString(k));//запись в результат
            t = t - new LongDigits(Convert.ToString(k * k));//вычитаем из числа, квадрат первого числа результата
            while (intpart.Length > 0)//пока не закончилась целая часть
            {
                t.digits += Convert.ToString(intpart[0]) + Convert.ToString(intpart[1]);//дописываем первые 2 цифры к делимому
                LongDigits b = result * new LongDigits("2");//умножаем результат на 2
                LongDigits f;
                int l = 0;
                do
                {
                    l++;//нахождение числа, которое при умножении даёт наибольшее число, меньше заданного 
                    f = new LongDigits(b.digits + Convert.ToString(l)) * new LongDigits(Convert.ToString(l));
                } while (f <= t);
                l--;
                
                result.digits = result.digits+Convert.ToString(l);//дописываем в результат найденную цийру
                LongDigits f1 = new LongDigits(b.digits + Convert.ToString(l));
                LongDigits f2 =new LongDigits(Convert.ToString(l));
                f = f1 * f2;
                t = t - f;//вычитаем из числа произведение найденных чисел
                intpart = intpart.Substring(2);//убираем первые 2 цифры из целой части
            }

            if (nac > 0) exresult = result.digits + '.';//если количество знаков после запятой больше нудля, ставим точку
            else exresult = result.digits;
            while (j < nac)//пока не закончились знаки после запятой
            {   //здесь почти то же, что и было в первой части за парой исключений
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
                exresult += Convert.ToString(l);//результаты дописываются уже в итооговый результат
                f= new LongDigits(b.digits + Convert.ToString(l)) * new LongDigits(Convert.ToString(l));
                t = t - f;
                doublepart = doublepart.Substring(2);
                j++;//количество знаков после запятой увеличивается
            }
            return exresult;
        }
    }
}