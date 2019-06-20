using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace praticeApp.DataAccess
{
    class ConfigLoader
    {
        private readonly String filename;

        public ConfigLoader()
        {
            this.filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "config.txt");
        }

        public bool LoadConfigTokenOutFile(ref String token)
        {
            if (File.Exists(GetFileName()))
            {
                token = File.ReadAllText(GetFileName());
                // token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjBjNjU5OGQ1MmJhYmI3YzQ1ZWU3NDAyNzg4YzNmNWFiY2I3OGJlNDMzMGQxNjZjMGQ0ZTY1MTVhODU4MjM0OWUzZjUwYmQwZjk5MDIzMjc2In0.eyJhdWQiOiIzIiwianRpIjoiMGM2NTk4ZDUyYmFiYjdjNDVlZTc0MDI3ODhjM2Y1YWJjYjc4YmU0MzMwZDE2NmMwZDRlNjUxNWE4NTgyMzQ5ZTNmNTBiZDBmOTkwMjMyNzYiLCJpYXQiOjE1NjEwMjE4NzIsIm5iZiI6MTU2MTAyMTg3MiwiZXhwIjoxNTkyNjQ0MjcyLCJzdWIiOiI4Iiwic2NvcGVzIjpbXX0.i_4P73LbxpvU8IyniGteeNy2fAYRXCBt-drh1C1-4TKhwmQRCQrTjuQ7AJcIIjKLxAJpbzrEcK6aZoLppJ3nSQ";
                return true;
            }

            return false;
        }

        /**public String LoadConfigTokenOutFile()
        {
            if (File.Exists(GetFileName()))
            {
                String token = File.ReadAllText(GetFileName());
                return token;
            }

            return "";

            //return "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjBjNjU5OGQ1MmJhYmI3YzQ1ZWU3NDAyNzg4YzNmNWFiY2I3OGJlNDMzMGQxNjZjMGQ0ZTY1MTVhODU4MjM0OWUzZjUwYmQwZjk5MDIzMjc2In0.eyJhdWQiOiIzIiwianRpIjoiMGM2NTk4ZDUyYmFiYjdjNDVlZTc0MDI3ODhjM2Y1YWJjYjc4YmU0MzMwZDE2NmMwZDRlNjUxNWE4NTgyMzQ5ZTNmNTBiZDBmOTkwMjMyNzYiLCJpYXQiOjE1NjEwMjE4NzIsIm5iZiI6MTU2MTAyMTg3MiwiZXhwIjoxNTkyNjQ0MjcyLCJzdWIiOiI4Iiwic2NvcGVzIjpbXX0.i_4P73LbxpvU8IyniGteeNy2fAYRXCBt-drh1C1-4TKhwmQRCQrTjuQ7AJcIIjKLxAJpbzrEcK6aZoLppJ3nSQ";
        }**/

        public void WriteConfigTokenInFile(String token)
        {
            File.WriteAllText(GetFileName(), token);
        }

        public String GetFileName()
        {
            return filename;
        }
    }
}
