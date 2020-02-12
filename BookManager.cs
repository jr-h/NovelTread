using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NovelTread
{
    class BookManager
    {
        internal Dictionary<string, Book> links;
        public BookManager()
        {
            /*开始索引*/
            string e = "error";
            links = new Dictionary<string, Book>();
            DirectoryInfo folder = new DirectoryInfo(Properties.Settings.Default.真目录);
            foreach (var item in folder.GetFiles("*.txt.ini"))
            {
                string path = Properties.Settings.Default.真目录 + '\\' + item;
                string mapName = OperateIniFile.read("Info", "Name", e, path);
                string auther = OperateIniFile.read("Info", "Author", e, path);
                string displayName = OperateIniFile.read("Info", "DisplayName", e, path);
                string tooltip = OperateIniFile.read("Info", "Tooltip", e, path);
                links.Add(mapName, new Book(mapName, auther, displayName, tooltip));
            }
        }
        public bool MakeBook(string FilePath, string author, string displayName, string tooltip)
        {
            //Copy书本，其实检查应该在获取FilePath时就做好的。
            if (!File.Exists(FilePath))
            {
                System.Windows.MessageBox.Show("未找到文件！" + FilePath, "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
            var mapName = FilePath.Split('\\').Last();
            //是否覆盖呢？
            File.Copy(FilePath, Properties.Settings.Default.真目录 + '\\' + mapName);
            var path = Properties.Settings.Default.真目录 + '\\' + mapName + ".txt.ini";
            File.Create(path);
            //写入
            if(OperateIniFile.write("Info", "Name", mapName, path)&&
                OperateIniFile.write("Info", "Author", author, path)&&
                OperateIniFile.write("Info", "DisplayName", displayName, path)&&
                OperateIniFile.write("Info", "Tooltip", tooltip, path))
                return true;
            System.Windows.MessageBox.Show("写入错误！"); //不够全
            return false;
        }
        public bool DelBook(string MapName)
        {
            var t1 = Properties.Settings.Default.真目录 + '\\' + links[MapName].path;
            File.Delete(t1);
            File.Delete(t1 + ".txt.ini");
            return true;
        }
        //导出为控件，犹豫。
    }
    public class OperateIniFile
    {
        #region API函数声明
        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion
        #region 读Ini文件
        public static string read(string Section, string Key, string defaultText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, defaultText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return defaultText;
            }
        }
        #endregion
        #region 写Ini文件
        public static bool write(string Section, string Key, string Value, string iniFilePath)
        {
            var pat = Path.GetDirectoryName(iniFilePath);
            if (Directory.Exists(pat) == false)
            {
                Directory.CreateDirectory(pat);
            }
            if (File.Exists(iniFilePath) == false)
            {
                File.Create(iniFilePath).Close();
            }
            long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
            if (OpStation == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
