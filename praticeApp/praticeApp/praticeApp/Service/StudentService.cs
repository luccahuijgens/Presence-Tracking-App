using praticeApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace praticeApp.Service
{
    public class StudentService
    {
        private StudentLoader studentLoader = new StudentLoader();

        public String GetStudentNameWithToken(String token)
        {
            return (studentLoader.GetStudentNameWithToken(token));
        }
    }
}