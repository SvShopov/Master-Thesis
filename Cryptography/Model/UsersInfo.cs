using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography.Model
{
    public class UsersInfo
    {
        private string username;
        private string studentNames;
        private string teacherNames;


        public string Username { get { return username; } set { username = value; } }
        public string StudentNames { get { return studentNames; } set { studentNames = value; } }
        public string TeacherNames { get { return teacherNames; } set { teacherNames = value; } }


        public UsersInfo()
        {

        }

    }
}
