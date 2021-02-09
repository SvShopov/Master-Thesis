using CommonServiceLocator;
using Cryptography.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cryptography.ViewModel
{
    public class SeePreviousUsesUserVM : ViewModelBase
    {
        #region Definitions
        //private string studentNames;
        //private string teacherNames;
        private string timeDate;
        private string methodName;
        private char workMode;
        private string inputArguments;
        private string result;
        private List<UsesInfo> usesInfo;
        private List<UsesInfo> usesInfoOrdered;
        private Teacher teacher;
        private Student student;
        #endregion

        #region Properties
        public Teacher Teacher { get { return teacher; } set { teacher = value; } }
        public Student Student { get { return student; } set { student = value; } }
        //public string StudentNames { get { return studentNames; } set { studentNames = value; RaisePropertyChanged("StudentNames"); } }
        //public string TeacherNames { get { return teacherNames; } set { teacherNames = value; RaisePropertyChanged("TeacherNames"); } }

        public string TimeDate { get { return timeDate; } set { timeDate = value; RaisePropertyChanged("TimeDate"); } }
        public string MethodName { get { return methodName; } set { methodName = value; RaisePropertyChanged("MethodName"); } }
        public char WorkMode { get { return workMode; } set { workMode = value; RaisePropertyChanged("WorkMode"); } }
        public string InputArguments { get { return inputArguments; } set { inputArguments = value; RaisePropertyChanged("InputArguments"); } }
        public string Result { get { return result; } set { result = value; RaisePropertyChanged("Result"); } }

        public List<UsesInfo> UsesInfoOrdered { get { return usesInfoOrdered; } set { usesInfoOrdered = value; RaisePropertyChanged("UsesInfoOrdered"); } }

        public ICommand Back { get; set; }
        #endregion

        #region Constructors
        public SeePreviousUsesUserVM()
        {
            UsesInfoOrdered = new List<UsesInfo>();
            usesInfo = new List<UsesInfo>();

            Back = new RelayCommand(() => BackToMenu());
        }
        #endregion

        #region Methods

        private void BackToMenu()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            SeeUsersVM suvm = ServiceLocator.Current.GetInstance<SeeUsersVM>();
            mvm.WindowHeight = 500;
            mvm.WindowWidth = 700;
            mvm.CurrentViewModel = suvm;
        }

        public async Task ShowUses()
        {
            await Task.Run(() =>
            {
                usesInfo.Clear();
                UsesInfoOrdered.Clear();
                using (CryptographyContext context = new CryptographyContext())
                {
                    lock (context)
                    {
                        //List<Cipher> ciphers = (from cph in context.Ciphers
                        //                          join st in context.Students on cph.StudentId equals st.Id
                        //                          select cph).ToList();

                        List<StudentCipher> ciphers = (from cph in context.StudentCiphers
                                                where cph.StudentId == Student.Id
                                                select cph).ToList();  //I guess it's ok?

                        foreach (StudentCipher cph in ciphers)
                        {
                            UsesInfo uses = new UsesInfo
                            {
                                TimeDate = cph.ExactTime,
                                MethodName = cph.MethodName,
                                WorkMode = cph.EncodeOrDecode,
                                InputArguments = cph.InputArgs,
                                Result = cph.Result
                            };
                            usesInfo.Add(uses);


                            UsesInfoOrdered = usesInfo.OrderBy(p => p.TimeDate.Substring(15,4))
                           .ThenBy(p => p.TimeDate.Substring(12, 2))
                           .ThenBy(p => p.TimeDate.Substring(9, 2))
                           .ThenBy(p => p.TimeDate).ToList();
                        }
                    }
                }
            });
        }

        #endregion
    }
}
