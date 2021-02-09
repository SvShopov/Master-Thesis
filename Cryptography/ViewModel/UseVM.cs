using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommonServiceLocator;
using Cryptography.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Cryptography.ViewModel
{
    public class UseVM : ViewModelBase
    {
        #region Definitions
        private Teacher teacher;
        private Student student;
        private string selectedMethod;
        private string input;
        private string key;
        private string keyword;
        private string alpha1;
        private string alpha2;
        private int step;
        private int rails;
        private int gridWidth;
        private int gridHeight;
        private string start;
        private string direction;
        private string result;

        private bool encodeMode;
        private bool decodeMode;
        
        private bool modeEnabled;
        private bool keyEnabled;
        private bool stepEnabled;
        private bool railsEnabled;
        private bool alpha1Enabled;
        private bool alpha2Enabled;
        private bool keywordEnabled;
        private bool gridHEnabled;
        private bool gridWEnabled;
        private bool startEnabled;
        private bool dirEnabled;

        #endregion

        #region Properties
        public Teacher Teacher { get { return teacher; } set { teacher = value; } }
        public Student Student { get { return student; } set { student = value; } }
        public string SelectedMethod
        {
            get { return selectedMethod; }

            set
            {
                selectedMethod = value;
                RaisePropertyChanged("SelectedMethod");

                if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Просто многоазбучно заместване")
                {
                    ModeEnabled = true;
                    KeyEnabled = true;
                    StepEnabled = false;
                    RailsEnabled = false;
                    Alpha1Enabled = true;
                    Alpha2Enabled = false;
                    KeywordEnabled = false;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Шифър на Виженер")
                {
                    ModeEnabled = true;
                    KeyEnabled = true;
                    StepEnabled = false;
                    RailsEnabled = false;
                    Alpha1Enabled = true;
                    Alpha2Enabled = false;
                    KeywordEnabled = false;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Нихилистичен шифър")
                {
                    ModeEnabled = true;
                    KeyEnabled = true;
                    StepEnabled = false;
                    RailsEnabled = false;
                    Alpha1Enabled = false;
                    Alpha2Enabled = false;
                    KeywordEnabled = true;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Шифър на Плейфеър")
                {
                    ModeEnabled = true;
                    KeyEnabled = true;
                    StepEnabled = false;
                    RailsEnabled = false;
                    Alpha1Enabled = false;
                    Alpha2Enabled = false;
                    KeywordEnabled = false;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Директно заместване")
                {
                    ModeEnabled = false;
                    KeyEnabled = false;
                    StepEnabled = false;
                    RailsEnabled = false;
                    Alpha1Enabled = true;
                    Alpha2Enabled = true;
                    KeywordEnabled = false;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Шифър на Цезар със стъпка")
                {
                    ModeEnabled = true;
                    KeyEnabled = false;
                    StepEnabled = true;
                    RailsEnabled = false;
                    Alpha1Enabled = true;
                    Alpha2Enabled = false;
                    KeywordEnabled = false;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Шифър на Бофорт")
                {
                    ModeEnabled = true;
                    KeyEnabled = true;
                    StepEnabled = false;
                    RailsEnabled = false;
                    Alpha1Enabled = true;
                    Alpha2Enabled = false;
                    KeywordEnabled = false;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Шифър с автоключ")
                {
                    ModeEnabled = true;
                    KeyEnabled = true;
                    StepEnabled = false;
                    RailsEnabled = false;
                    Alpha1Enabled = true;
                    Alpha2Enabled = false;
                    KeywordEnabled = false;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Транспозиция с блокове")
                {
                    ModeEnabled = true;
                    KeyEnabled = true;
                    StepEnabled = false;
                    RailsEnabled = false;
                    Alpha1Enabled = false;
                    Alpha2Enabled = false;
                    KeywordEnabled = false;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Транспозиция с форматиране")
                {
                    ModeEnabled = true;
                    KeyEnabled = true;
                    StepEnabled = false;
                    RailsEnabled = false;
                    Alpha1Enabled = false;
                    Alpha2Enabled = false;
                    KeywordEnabled = false;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Шифър на релсови огради")
                {
                    ModeEnabled = true;
                    KeyEnabled = false;
                    StepEnabled = false;
                    RailsEnabled = true;
                    Alpha1Enabled = false;
                    Alpha2Enabled = false;
                    KeywordEnabled = false;
                    GridHEnabled = false;
                    GridWEnabled = false;
                    StartEnabled = false;
                    DirEnabled = false;
                }
                else if (SelectedMethod == "System.Windows.Controls.ComboBoxItem: Спирален шифър")
                {
                    ModeEnabled = true;
                    KeyEnabled = false;
                    StepEnabled = false;
                    RailsEnabled = false;
                    Alpha1Enabled = false;
                    Alpha2Enabled = false;
                    KeywordEnabled = false;
                    GridHEnabled = true;
                    GridWEnabled = true;
                    StartEnabled = true;
                    DirEnabled = true;
                }

            }
        }

        public string Input { get { return input; } set { input = value; RaisePropertyChanged("Input"); } }
        public string Key { get { return key; } set { key = value; RaisePropertyChanged("Key"); } }
        public string Keyword { get { return keyword; } set { keyword = value; RaisePropertyChanged("Keyword"); } }
        public string Alpha1 { get { return alpha1; } set { alpha1 = value; RaisePropertyChanged("Alpha1"); } }
        public string Alpha2 { get { return alpha2; } set { alpha2 = value; RaisePropertyChanged("Alpha2"); } }
        public int Step { get { return step; } set { step = value; RaisePropertyChanged("Step"); } }
        public int Rails { get { return rails; } set { rails = value; RaisePropertyChanged("Rails"); } }
        public int GridWidth { get { return gridWidth; } set { gridWidth = value; RaisePropertyChanged("GridWidth"); } }
        public int GridHeight { get { return gridHeight; } set { gridHeight = value; RaisePropertyChanged("GridHeight"); } }
        public string Start { get { return start; } set { start = value; RaisePropertyChanged("Start"); } }
        public string Direction { get { return direction; } set { direction = value; RaisePropertyChanged("Direction"); } }
        public string Result { get { return result; } set { result = value; RaisePropertyChanged("Result"); } }

        public bool EncodeMode { get { return encodeMode; } set { encodeMode = value; RaisePropertyChanged("EncodeMode"); } }
        public bool DecodeMode { get { return decodeMode; } set { decodeMode = value; RaisePropertyChanged("DecodeMode"); } }
        
        public bool ModeEnabled { get { return modeEnabled; } set { modeEnabled = value; RaisePropertyChanged("ModeEnabled"); } }
        public bool KeyEnabled { get { return keyEnabled; } set { keyEnabled = value; RaisePropertyChanged("KeyEnabled"); } }
        public bool StepEnabled { get { return stepEnabled; } set { stepEnabled = value; RaisePropertyChanged("StepEnabled"); } }
        public bool RailsEnabled { get { return railsEnabled; } set { railsEnabled = value; RaisePropertyChanged("RailsEnabled"); } }
        public bool Alpha1Enabled { get { return alpha1Enabled; } set { alpha1Enabled = value; RaisePropertyChanged("Alpha1Enabled"); } }
        public bool Alpha2Enabled { get { return alpha2Enabled; } set { alpha2Enabled = value; RaisePropertyChanged("Alpha2Enabled"); } }
        public bool KeywordEnabled { get { return keywordEnabled; } set { keywordEnabled = value; RaisePropertyChanged("KeywordEnabled"); } }
        public bool GridHEnabled { get { return gridHEnabled; } set { gridHEnabled = value; RaisePropertyChanged("GridHEnabled"); } }
        public bool GridWEnabled { get { return gridWEnabled; } set { gridWEnabled = value; RaisePropertyChanged("GridWEnabled"); } }
        public bool StartEnabled { get { return startEnabled; } set { startEnabled = value; RaisePropertyChanged("StartEnabled"); } }
        public bool DirEnabled { get { return dirEnabled; } set { dirEnabled = value; RaisePropertyChanged("DirEnabled"); } }

        public ICommand Operation { get; set; }
        public ICommand Back { get; set; }
        public ICommand BrowseS { get; set; }
        public ICommand BrowseLoadPlain { get; set; }
        public ICommand BrowseLoadKey { get; set; }
        public ICommand BrowseLoadKeyword { get; set; }
        public ICommand BrowseLoadAlpha1 { get; set; }
        public ICommand BrowseLoadAlpha2 { get; set; }
        #endregion

        #region Constructors
        public UseVM()
        {
            Operation = new RelayCommand(() => Operate());
            Back = new RelayCommand(() => BackToMenu());
            BrowseLoadPlain = new RelayCommand(() => BrowseInput());
            BrowseLoadKey = new RelayCommand(() => BrowseKey());
            BrowseLoadKeyword = new RelayCommand(() => BrowseKeyword());
            BrowseLoadAlpha1 = new RelayCommand(() => BrowseAlpha1());
            BrowseLoadAlpha2 = new RelayCommand(() => BrowseAlpha2());
            BrowseS = new RelayCommand(() => BrowseSaveResult());

            ModeEnabled = true;
            KeyEnabled = true;
            StepEnabled = true;
            RailsEnabled = true;
            Alpha1Enabled = true;
            Alpha2Enabled = true;
            KeywordEnabled = true;
            GridHEnabled = true;
            GridWEnabled = true;
            StartEnabled = true;
            DirEnabled = true;
                       
            
        }

        #endregion

        #region Methods

        public void Operate()
        {
            //string user = "";
            //if ((string)Application.Current.Properties["UserRole"] == "Teacher")
            //{
            //    user = Teacher.UserName;
            //}
            //else
            //{
            //    user = Student.UserName;
            //}
            string time = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            string args = "";
            string methodname = "";
            char mode = 'К';
            if (EncodeMode)
            {
                mode = 'К';
            }
            else if (DecodeMode)
            {
                mode = 'Д';
            }

            bool error = false;
            if (String.IsNullOrEmpty(Input))
            {
                MessageBox.Show("Моля въведете входно съобщение!");
                error = true;
            }
            if (SelectedMethod.ToString()!= "System.Windows.Controls.ComboBoxItem: Директно заместване")
            {
                if (!EncodeMode && !DecodeMode)
                {
                    MessageBox.Show("Моля изберете режим на работа!");
                    error = true;
                }
            }

            try
            {
                switch (SelectedMethod.ToString())
                {
                    case "System.Windows.Controls.ComboBoxItem: Просто многоазбучно заместване":
                        args = "Входен текст: " + Input + ", Ключ: " + Key + ", Азбука: " + Alpha1.Substring(0, 4) + "...";
                        methodname = "Просто заместване";
                        if (EncodeMode)
                        {
                            Result = CryptoVM.SimpleEncode(Input, Key, Alpha1);
                            //DatabaseUpdate(user, time, methodname, 'К', Input, args, Result);
                        }
                        else if (DecodeMode)
                        {
                            Result = CryptoVM.SimpleDecode(Input, Key, Alpha1);
                            //DatabaseUpdate(user, time, methodname, 'Д', Input, args, Result);
                        }
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Шифър на Виженер":
                        args = "Входен текст: " + Input + ", Ключ: " + Key + ", Азбука: " + Alpha1.Substring(0, 4) + "...";
                        methodname = "Шифър на Виженер";
                        if (EncodeMode)
                        {
                            Result = CryptoVM.VigenereEncode(Input, Alpha1, Key);
                        }
                        else if (DecodeMode)
                        {
                            Result = CryptoVM.VigenereDecode(Input, Alpha1, Key);
                        }
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Нихилистичен шифър":
                        args = "Входен текст: " + Input + ", Ключова дума: " + Keyword + ", Ключ: " + Key;
                        methodname = "Нихилистичен шифър";
                        if (EncodeMode)
                        {
                            Result = CryptoVM.NihilistEncode(Input, Keyword, Key);
                        }
                        else if (DecodeMode)
                        {
                            Result = CryptoVM.NihilistDecode(Input, Keyword, Key);
                        }
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Шифър на Плейфеър":
                        args = "Входен текст: " + Input + ", Ключ: " + Key;
                        methodname = "Шифър на Плейфеър";
                        if (EncodeMode)
                        {
                            Result = CryptoVM.PlayfairEncode(Input, Key);
                        }
                        else if (DecodeMode)
                        {
                            Result = CryptoVM.PlayfairDecode(Input, Key);
                        }
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Директно заместване":
                        args = "Входен текст: " + Input + ", Входна азбука: " + Alpha1.Substring(0, 4) + "..."
                            + ", Изходна азбука: " + Alpha2.Substring(0, 4) + "...";
                        methodname = "Директно заместване";
                        Result = CryptoVM.DirectEncode(Input, Alpha1, Alpha2);
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Шифър на Цезар със стъпка":
                        args = "Входен текст: " + Input + ", Стъпка: " + Step + ", Азбука: " + Alpha1.Substring(0, 4) + "...";
                        methodname = "Шифър на Цезар със стъпка";
                        if (EncodeMode)
                        {
                            Result = CryptoVM.CaesarEncode(Input, Alpha1, Step);
                        }
                        else if (DecodeMode)
                        {
                            Result = CryptoVM.CaesarDecode(Input, Alpha1, Step);
                        }
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Шифър на Бофорт":
                        args = "Входен текст: " + Input + ", Ключ: " + Key + ", Азбука: " + Alpha1.Substring(0, 4) + "...";
                        methodname = "Шифър на Бофорт";
                        if (EncodeMode)
                        {
                            Result = CryptoVM.BeaufortEncode(Input, Key, Alpha1);
                        }
                        else if (DecodeMode)
                        {
                            Result = CryptoVM.BeaufortDecode(Input, Key, Alpha1);
                        }
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Шифър с автоключ":
                        args = "Входен текст: " + Input + ", Ключ: " + Key + ", Азбука: " + Alpha1.Substring(0, 4) + "...";
                        methodname = "Шифър с автоключ";
                        if (EncodeMode)
                        {
                            Result = CryptoVM.AutokeyEncode(Input, Key, Alpha1);
                        }
                        else if (DecodeMode)
                        {
                            Result = CryptoVM.AutokeyDecode(Input, Key, Alpha1);
                        }
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Транспозиция с блокове":
                        args = "Входен текст: " + Input + ", Ключ: " + Key;
                        methodname = "Транспозиция с блокове";
                        if (EncodeMode)
                        {
                            Result = CryptoVM.TranspBlockEncode(Input, Key);
                        }
                        else if (DecodeMode)
                        {
                            Result = CryptoVM.TranspBlockDecode(Input, Key);
                        }
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Транспозиция с форматиране":
                        args = "Входен текст: " + Input + ", Ключ: " + Key;
                        methodname = "Транспозиция с форматиране";
                        if (EncodeMode)
                        {
                            Result = CryptoVM.TranspFormatEncode(Input, Key);
                        }
                        else if (DecodeMode)
                        {
                            Result = CryptoVM.TranspFormatDecode(Input, Key);
                        }
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Шифър на релсови огради":
                        args = "Входен текст: " + Input + ", Линии: " + Rails;
                        methodname = "Шифър на релсови огради";
                        if (EncodeMode)
                        {
                            Result = CryptoVM.RailFenceEncode(Input, Rails);
                        }
                        else if (DecodeMode)
                        {
                            Result = CryptoVM.RailFenceDecode(Input, Rails);
                        }
                        break;

                    case "System.Windows.Controls.ComboBoxItem: Спирален шифър":
                        args = "Входен текст: " + Input + ", Ширина на табл.: " + GridWidth + ", Височина на табл.: " + GridHeight +
                            ", Старт. позиция: " + Start + ", Посока: " + Direction;
                        methodname = "Спирален шифър";
                        int st = 0;
                        int dir = 0;

                        if (EncodeMode)
                        {                 
                            if (Start == "System.Windows.Controls.ComboBoxItem: Горе вляво")
                            {
                                st = 1;
                            }
                            else if (Start == "System.Windows.Controls.ComboBoxItem: Горе вдясно")
                            {
                                st = 2;
                            }
                            else if (Start == "System.Windows.Controls.ComboBoxItem: Долу вляво")
                            {
                                st = 3;
                            }
                            else if (Start == "System.Windows.Controls.ComboBoxItem: Долу вдясно")
                            {
                                st = 4;
                            }

                            if (Direction == "System.Windows.Controls.ComboBoxItem: Обратно на часовника")
                            {
                                dir = 1;
                            }
                            else if (Direction == "System.Windows.Controls.ComboBoxItem: По часовника")
                            {
                                dir = 2;
                            }

                            Result = CryptoVM.SpiralEncode(Input, GridHeight, GridWidth, st, dir);
                        }
                        else if (DecodeMode)
                        { 
                            if (Start == "System.Windows.Controls.ComboBoxItem: Горе вляво")
                            {
                                st = 1;
                            }
                            else if (Start == "System.Windows.Controls.ComboBoxItem: Горе вдясно")
                            {
                                st = 2;
                            }
                            else if (Start == "System.Windows.Controls.ComboBoxItem: Долу вляво")
                            {
                                st = 3;
                            }
                            else if (Start == "System.Windows.Controls.ComboBoxItem: Долу вдясно")
                            {
                                st = 4;
                            }

                            if (Direction == "System.Windows.Controls.ComboBoxItem: Обратно на часовника")
                            {
                                dir = 1;
                            }
                            else if (Direction == "System.Windows.Controls.ComboBoxItem: По часовника")
                            {
                                dir = 2;
                            }

                            Result = CryptoVM.SpiralDecode(Input, GridHeight, GridWidth, st, dir);
                        }
                        break;
                    default:
                        break;
                }

                // no DatabaseUpdate(user, time, methodname, mode, Input, args, Result);
                DatabaseUpdate(time, methodname, mode, Input, args, Result);
            }
            catch (Exception e)
            {
                if (!error)
                {
                    MessageBox.Show("Грешка в данните!");
                }                
                //MessageBox.Show(e.Message);
                //throw;
            }
        }

        public void DatabaseUpdate(string time, string mName, char eOrD, string inMessage,
            string inArgs, string outRes)
        {
            using (CryptographyContext context = new Model.CryptographyContext())
            {
                lock (context)
                {

                    if ((string)Application.Current.Properties["UserRole"] == "Teacher")
                    {
                        TeacherCipher cip = new TeacherCipher();
                        cip.ExactTime = time;
                        cip.MethodName = mName;
                        cip.EncodeOrDecode = eOrD;
                        cip.InputMessage = inMessage;
                        cip.InputArgs = inArgs;
                        cip.Result = outRes;
                        
                        Teacher t = (from tea in context.Teachers
                                     where tea.Id.Equals(Teacher.Id)
                                     select tea).First();

                        TeacherCipher exist = new TeacherCipher();
                        if (t.Ciphers is null)
                        {
                            exist = null;
                        }
                        else
                        {
                            exist = (from check in t.Ciphers
                                     where check.TeacherId.Equals(t.Id)
                                     select check).FirstOrDefault();
                        }

                        if (exist is null)
                        {
                            cip.Teacher = t;
                            context.TeacherCiphers.Add(cip);
                            context.SaveChanges();
                        }

                    }
                    else
                    {
                        StudentCipher cip = new StudentCipher();
                        cip.ExactTime = time;
                        cip.MethodName = mName;
                        cip.EncodeOrDecode = eOrD;
                        cip.InputMessage = inMessage;
                        cip.InputArgs = inArgs;
                        cip.Result = outRes;
                       
                        Student s = (from stu in context.Students
                                     where stu.Id == Student.Id
                                     select stu).First();

                        StudentCipher exist = new StudentCipher();
                        if (s.Ciphers is null)
                        {
                            exist = null;
                        }
                        else
                        {
                            exist = (from check in s.Ciphers
                                     where check.StudentId.Equals(s.Id)
                                     select check).FirstOrDefault();
                        }                            

                        if (exist is null)
                        {
                            cip.Student = s;
                            context.StudentCiphers.Add(cip);
                            context.SaveChanges();
                        }
                    }

                }
            }
        }

        public void BrowseInput()
        {
            Input = BrowseLoad();
        }
        public void BrowseKey()
        {
            Key = BrowseLoad();
        }
        public void BrowseKeyword()
        {
            Keyword = BrowseLoad();
        }
        public void BrowseAlpha1()
        {
            Alpha1 = BrowseLoad();
        }
        public void BrowseAlpha2()
        {
            Alpha2 = BrowseLoad();
        }

        public string BrowseLoad()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> show = dlg.ShowDialog();

            string path = "";
            string s = "";

            if (show == true)
            {
                path = dlg.FileName;
                StreamReader sr = new StreamReader(path);
                s = sr.ReadLine();
            }
            else
            {
                MessageBox.Show("Missing file or wrong format!");
            }

            return s;
        }

        public void BrowseSaveResult()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> show = dlg.ShowDialog();

            string path = "";

            if (show == true)
            {
                path = dlg.FileName;
                using (StreamWriter sw = new StreamWriter(path, append: true))
                {
                    sw.WriteLine(Result);
                }
            }
            else
            {
                MessageBox.Show("Missing file or wrong format!");
            }



        }

        private void BackToMenu()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            if ((string)Application.Current.Properties["UserRole"] == "Teacher")
            {
                TeacherMenuVM tvm = ServiceLocator.Current.GetInstance<TeacherMenuVM>();
                mvm.WindowHeight = 320;
                mvm.WindowWidth = 400;
                mvm.CurrentViewModel = tvm;
            }
            else if ((string)Application.Current.Properties["UserRole"] == "Student")
            {
                StudentMenuVM svm = ServiceLocator.Current.GetInstance<StudentMenuVM>();
                mvm.WindowHeight = 320;
                mvm.WindowWidth = 400;
                mvm.CurrentViewModel = svm;
            }
        }

        public void ClearControls()
        {
            //idk if correct

            //SelectedMethod = null;
            Input = null;
            Key = null;
            Keyword = null;
            Alpha1 = null;
            Alpha2 = null;
            Step = 1; //?
            Rails = 3; //?
            GridWidth = 3; //?
            GridHeight = 3; //?
            //Start = null;
            //Direction = null;
            Result = null;
        }


        #endregion
    }
}
