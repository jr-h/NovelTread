using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
namespace NovelTread
{
    class Book
    {
        internal string path { set; get; }
        internal string Author { set; get; }
        internal string DisplayName { set; get; }
        internal string Tooltip { set; get; }
        //internal string CreateTime { set; get; }
        public Book(string FileName, string author, string displayName, string tooltip)
        {
            path = FileName;
            Author = author;
            DisplayName = displayName;
            Tooltip = tooltip;
        }
    }
}
