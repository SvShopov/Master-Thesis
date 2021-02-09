using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography.Model
{
    public class UsesInfo
    {
        private string timeDate;
        private string methodName;
        private char workMode;
        private string inputArguments;
        private string result;
        
        public string TimeDate { get { return timeDate; } set { timeDate = value; } }
        public string MethodName { get { return methodName; } set { methodName = value; } }
        public char WorkMode { get { return workMode; } set { workMode = value; } }
        public string InputArguments { get { return inputArguments; } set { inputArguments = value; } }
        public string Result { get { return result; } set { result = value; } }
        

        public UsesInfo()
        {

        }

    }
}
