using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTUtil
{
    public static class FormUtil
    {
        public static List<TForm> FindForms<TForm>() where TForm : Form
        {
            var forms = new List<TForm>();
            foreach (Form form in Application.OpenForms)
            {
                if (form is TForm) forms.Add((TForm) form);
            }
            return forms;
        }

        public static TForm FindForm<TForm>() where TForm : Form
        {
            return FindForms<TForm>().FirstOrDefault();
        }
    }
}
