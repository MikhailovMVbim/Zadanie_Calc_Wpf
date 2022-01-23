using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using Zadanie_Calc_Wpf.Models;

namespace Zadanie_Calc_Wpf.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private double number1 = 0; // значение вводимого пользователем числа
        public double Number1
        {
            get => number1;
            set
            {
                number1 = value;
                OnPropertyChanged();
            }
        }

        private double number2 = 0; // значение числа в памяти
        public double Number2
        {
            get => number2;
            set
            {
                number2 = value;
                OnPropertyChanged();
            }
        }

        private string textEnter = ""; // текст в поле ввода чисел
        public string TextEnter
        {
            get => textEnter;
            set
            {
                textEnter = value;
                OnPropertyChanged();
            }
        }

        private string textMemory; // текст в поле операции
        public string TextMemory
        {
            get => textMemory;
            set
            {
                textMemory = value;
                OnPropertyChanged();
            }
        }

        private string textOperation; // текущая операция
        public string TextOperation
        {
            get => textOperation;
            set
            {
                textOperation = value;
                OnPropertyChanged();
            }
        }

        bool isMemoryData = true;
        bool isNewInputData = true;
        bool isNumber2 = false;
        int operationCode = 0;

        public ICommand ClearCommand { get; } // очистка всех расчетов

        private void OnClearCommand(object p)
        {
            TextEnter = "";
            TextOperation = "";
            TextMemory = "";
            Number1 = 0;
            Number2 = 0;
            isNewInputData = true;
        }

        public ICommand ClearInputCommand { get; } // очистка поля ввода и операции

        private void OnClearInputCommand(object p)
        {
            TextEnter = "";
            TextOperation = "";
            isNewInputData = true;
        }


        public ICommand AddCommand { get; } // сложение

        private void OnAddCommand(object p)
        {
            TextOperation = "+";
            operationCode = 1;
            GetNumbers();
        }

        public ICommand SubCommand { get; } // вычитание

        private void OnSubCommand(object p)
        {
            TextOperation = "-";
            operationCode = 2;
            GetNumbers();
        }

        public ICommand MultCommand { get; } // умножение

        private void OnMultCommand(object p)
        {
            TextOperation = "*";
            operationCode = 3;
            GetNumbers();
        }

        public ICommand DivCommand { get; } // деление

        private void OnDivCommand(object p)
        {
            TextOperation = "/";
            operationCode = 4;
            GetNumbers();
        }

        public ICommand SqrtCommand { get; } // вычисление квадратного корня

        private void OnSqrtCommand(object p)
        {
            TextOperation = "√";
            if (TextEnter != "")
            {
                Number1 = Convert.ToDouble(TextEnter);
                isMemoryData = false;
            }
            if (isMemoryData)
            {
                TextMemory = "√(" + Number2.ToString() + ")";
                Number2 = Calculations.Sqrt(Number2);
                TextEnter = Number2.ToString();
            }
            else
            {
                TextMemory = "√(" + Number1.ToString() + ")";
                Number2 = Calculations.Sqrt(Number1);
                TextEnter = Number2.ToString();
            }
            isNewInputData = true;
        }

        public ICommand SqrCommand { get; } // возведение в квадрат

        private void OnSqrCommand(object p)
        {
            TextOperation = "x²";
            if (TextEnter != "")
            {
                Number1 = Convert.ToDouble(TextEnter);
                isMemoryData = false;
            }
            if (isMemoryData)
            {
                TextMemory = Number2.ToString() + "²";
                Number2 = Calculations.Sqr(Number2);
                TextEnter = Number2.ToString();
            }
            else
            {
                TextMemory = Number1.ToString() + "²";
                Number2 = Calculations.Sqr(Number1);
                TextEnter = Number2.ToString();
            }
            isNewInputData = true;
        }

        public ICommand QubCommand { get; } // возведение в куб

        private void OnQubCommand(object p)
        {
            TextOperation = "x³";
            if (TextEnter != "")
            {
                Number1 = Convert.ToDouble(TextEnter);
                isMemoryData = false;
            }
            if (isMemoryData)
            {
                Number2 = Calculations.Qube(Number2);
                TextEnter = Number2.ToString();
                TextMemory = Number2.ToString() + "³";
            }
            else
            {
                TextMemory = Number1.ToString() + "³";
                Number2 = Calculations.Qube(Number1);
                TextEnter = Number2.ToString();
            }
            isNewInputData = true;
        }

        public ICommand DivOneCommand { get; } // выполнение операции 1/х

        private void OnDivOneCommand(object p)
        {
            TextOperation = "1/x";
            if (TextEnter != "")
            {
                Number1 = Convert.ToDouble(TextEnter);
                isMemoryData = false;
            }
            if (isMemoryData)
            {
                if (Number2 == 0)
                {
                    TextMemory = "1/0";
                    TextEnter = "Error";
                    return;
                }
                TextMemory = "1/" + Number2.ToString();
                Number2 = Calculations.DivOne(Number2);
                TextEnter = Number2.ToString();
                TextEnter = "";
            }
            else
            {
                TextMemory = "1/" + Number1.ToString();
                Number2 = Calculations.DivOne(Number1);
                TextEnter = Number2.ToString();
            }
            isNewInputData = true;
        }

        public ICommand ResultCommand { get; } // выполнение операции =

        private void OnResultCommand(object p)
        {
            TextOperation = "=";
            if (TextEnter != "" && isNumber2 == true)
            {
                Number2 = Convert.ToDouble(TextEnter);
                isNumber2 = false;
            }

            switch (operationCode)
            {

                case 1:
                    TextEnter = Calculations.Add(Number1, Number2).ToString();
                    TextMemory += Number2.ToString() + TextOperation + TextEnter;
                    isNewInputData = true;
                    break;
                case 2:
                    TextEnter = Calculations.Sub(Number1, Number2).ToString();
                    TextMemory += Number2.ToString() + TextOperation + TextEnter;
                    isNewInputData = true;
                    break;
                case 3:
                    TextEnter = Calculations.Mult(Number1, Number2).ToString();
                    TextMemory += Number2.ToString() + TextOperation + TextEnter;
                    isNewInputData = true;
                    break;
                case 4:
                    if (Number2!=0)
                    {
                    TextEnter = Calculations.Div(Number1, Number2).ToString();
                    TextMemory += Number2.ToString() + TextOperation + TextEnter;
                    isNewInputData = true;
                    break;
                    }
                    else
                    {
                        TextEnter = "Error";
                        TextMemory += Number2.ToString() + TextOperation;
                        break;
                    }
                default:
                    break;
            }
        } 

        void GetNumbers()
        {
            if (TextEnter != "" && isNumber2 == false)
            {
                Number1 = Convert.ToDouble(TextEnter);
                isNumber2 = true;
            }
            TextMemory = Number1.ToString() + " " + TextOperation + " ";
            isNewInputData = true;
        } // получаем данные для вычислений (первое или второе число)

        public ICommand Press1 { get; }
        private void OnPress1(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "";
                TextOperation = "";
                TextEnter += "1";
                isNewInputData = false;
            }
            else
                TextEnter += "1";
        }

        public ICommand Press2 { get; }
        private void OnPress2(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "";
                TextOperation = "";
                TextEnter += "2";
                isNewInputData = false;
            }
            else
                TextEnter += "2";
        }

        public ICommand Press3 { get; }
        private void OnPress3(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "";
                TextOperation = "";
                TextEnter += "3";
                isNewInputData = false;
            }
            else
                TextEnter += "3";
        }

        public ICommand Press4 { get; }
        private void OnPress4(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "";
                TextOperation = "";
                TextEnter += "4";
                isNewInputData = false;
            }
            else
                TextEnter += "4";
        }

        public ICommand Press5 { get; }
        private void OnPress5(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "";
                TextOperation = "";
                TextEnter += "5";
                isNewInputData = false;
            }
            else
                TextEnter += "5";
        }

        public ICommand Press6 { get; }
        private void OnPress6(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "";
                TextOperation = "";
                TextEnter += "6";
                isNewInputData = false;
            }
            else
                TextEnter += "6";
        }

        public ICommand Press7 { get; }
        private void OnPress7(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "";
                TextOperation = "";
                TextEnter += "7";
                isNewInputData = false;
            }
            else
                TextEnter += "7";
        }

        public ICommand Press8 { get; }
        private void OnPress8(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "";
                TextOperation = "";
                TextEnter += "8";
                isNewInputData = false;
            }
            else
                TextEnter += "8";
        }

        public ICommand Press9 { get; }
        private void OnPress9(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "";
                TextOperation = "";
                TextEnter += "9";
                isNewInputData = false;
            }
            else
                TextEnter += "9";
        }

        public ICommand Press0 { get; }
        private void OnPress0(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "";
                TextOperation = "";
                TextEnter += "0";
                isNewInputData = false;
            }
            else
                TextEnter += "0";
        }

        public ICommand PressComma { get; }
        private void OnPressComma(object p)
        {
            if (isNewInputData)
            {
                TextEnter = "0";
                TextOperation = "";
                TextEnter += ",";
                isNewInputData = false;
            }
            else if (TextEnter.Contains(','))
                return;
            else
                TextEnter += ",";
        }

        public ICommand ZnakCommand { get; } // меняем знак

        private void OnZnakCommand(object p)
        {
            TextOperation = "";
            if (TextEnter.StartsWith("-"))
            {
                TextEnter = TextEnter.Substring(1);
            }
            else
                TextEnter = TextEnter.Insert(0, "-");
        }


        public ICommand BackCommand { get; }
        private void OnBackCommand(object p) => TextEnter = (TextEnter != "") ? TextEnter.Remove(0, 1) : "";

        public MainWindowViewModel()
        {
            ClearInputCommand = new RelayCommand(OnClearInputCommand);
            ClearCommand = new RelayCommand(OnClearCommand);
            BackCommand = new RelayCommand(OnBackCommand);
            AddCommand = new RelayCommand(OnAddCommand);
            SubCommand = new RelayCommand(OnSubCommand);
            MultCommand = new RelayCommand(OnMultCommand);
            DivCommand = new RelayCommand(OnDivCommand);
            SqrtCommand = new RelayCommand(OnSqrtCommand);
            SqrCommand = new RelayCommand(OnSqrCommand);
            QubCommand = new RelayCommand(OnQubCommand);
            DivOneCommand = new RelayCommand(OnDivOneCommand);
            ResultCommand = new RelayCommand(OnResultCommand);
            ZnakCommand = new RelayCommand(OnZnakCommand);


            Press1 = new RelayCommand(OnPress1);
            Press2 = new RelayCommand(OnPress2);
            Press3 = new RelayCommand(OnPress3);
            Press4 = new RelayCommand(OnPress4);
            Press5 = new RelayCommand(OnPress5);
            Press6 = new RelayCommand(OnPress6);
            Press7 = new RelayCommand(OnPress7);
            Press8 = new RelayCommand(OnPress8);
            Press9 = new RelayCommand(OnPress9);
            Press0 = new RelayCommand(OnPress0);
            PressComma = new RelayCommand(OnPressComma);
        }
    }
}
