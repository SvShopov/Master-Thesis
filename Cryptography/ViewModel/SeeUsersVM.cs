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
    public class SeeUsersVM : ViewModelBase
    {
        #region Definitions
        private List<string> students;
        private string username;
        private string studentNames;
        private string teacherNames;
        private string selectedStudent;
        private List<UsersInfo> usersInfo;
        private List<UsersInfo> usersInfoOrdered;
        private Teacher teacher;
        #endregion

        #region Properties
        public List<string> Students { get { return students; } set { students = value; RaisePropertyChanged("Students"); } }
        public Teacher Teacher { get { return teacher; } set { teacher = value; } }
        public string Username { get { return username; } set { username = value; RaisePropertyChanged("Username"); } }
        public string StudentNames { get { return studentNames; } set { studentNames = value; RaisePropertyChanged("StudentNames"); } }
        public string TeacherNames { get { return teacherNames; } set { teacherNames = value; RaisePropertyChanged("TeacherNames"); } }
        public string SelectedStudent { get { return selectedStudent; } set { selectedStudent = value; RaisePropertyChanged("SelectedStudent"); } }
        public List<UsersInfo> UsersInfoOrdered { get { return usersInfoOrdered; } set { usersInfoOrdered = value; RaisePropertyChanged("UsersInfoOrdered"); } }

        public ICommand Back { get; set; }
        public ICommand SeeUses { get; set; }
        //public ICommand Block { get; set; }
        #endregion

        #region Constructors
        public SeeUsersVM()
        {
            //AddStudents();
            //SeeUsers();

            UsersInfoOrdered = new List<UsersInfo>();
            usersInfo = new List<UsersInfo>();
            SeeUses = new RelayCommand(() => SeePreviousUsesUser());
            //Block = new RelayCommand(() => BlockUser());
            Back = new RelayCommand(() => BackToTeacherMenu());
        }


        #endregion

        #region Methods

        private void SeePreviousUsesUser()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            SeePreviousUsesUserVM spuuvm = ServiceLocator.Current.GetInstance<SeePreviousUsesUserVM>();

            using (CryptographyContext context = new CryptographyContext())
            {
                lock (context)
                {
                    Student selected = (from stud in context.Students
                                        where stud.UserName == selectedStudent
                                        select stud).First();  //I guess it's ok?
                    spuuvm.Student = selected;
                }
            }
            mvm.WindowHeight = 550;
            mvm.WindowWidth = 1020;
            mvm.CurrentViewModel = spuuvm;
            Task show = spuuvm.ShowUses();
        }

        private void BackToTeacherMenu()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            TeacherMenuVM tmvm = ServiceLocator.Current.GetInstance<TeacherMenuVM>();
            mvm.WindowHeight = 320;
            mvm.WindowWidth = 400;
            mvm.CurrentViewModel = tmvm;
        }

        public async Task SeeUsers()
        {
            await Task.Run(() =>
            {
                usersInfo.Clear();
                UsersInfoOrdered.Clear();
                using (CryptographyContext context = new CryptographyContext())
                {
                    lock (context)
                    {
                        List<Student> students = (from st in context.Students
                                                  join tr in context.Teachers on st.TeacherId equals tr.Id
                                                  select st).ToList();

                        foreach (Student st in students)
                        {
                            Teacher teacher = (from tr in context.Teachers
                                               join stud in context.Students on tr.Id equals st.TeacherId
                                               select tr).FirstOrDefault();
                            UsersInfo user = new UsersInfo
                            {
                                Username = st.UserName,
                                StudentNames = st.Name + " " + st.FamilyName,
                                TeacherNames = teacher.Name + " " + teacher.FamilyName
                            };
                            usersInfo.Add(user);
                        }


                        UsersInfoOrdered = usersInfo.OrderBy(p => p.TeacherNames).ThenBy(c => c.StudentNames).ToList();
                        AddStudents();
                    }
                }
            });
        }

        private void AddStudents()
        {           
            using (CryptographyContext context = new CryptographyContext())
            {                
                Teacher t = (from tea in context.Teachers
                             where tea.Id.Equals(Teacher.Id)
                             select tea).First();

                Students = (from st in context.Students
                            where st.TeacherId == t.Id
                            select st.UserName).ToList();



                //Students = (from st in context.Students
                //            select st.UserName).ToList();

            }
        }

        public void ClearControls()
        {
            //SelectedStudent = null;
            // do i keep it and do i add others?            
        }

        #endregion
    }
}
