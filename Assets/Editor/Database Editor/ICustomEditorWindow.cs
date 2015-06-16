using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamName.Editors.Database
{
    public interface ICustomEditorWindow
    {
        bool requiresDatabase { get; set; }

        void Focus();
        void Draw();
    }
}
