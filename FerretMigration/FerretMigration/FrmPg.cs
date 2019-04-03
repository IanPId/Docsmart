using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerretMigration
{
    public partial class FrmPg : Form
    {
        public FrmPg()
        {
            InitializeComponent();
        }
        public void SetProgressBarVaule(int pbVaule)
        {
            progressBar1.Value = pbVaule;
        }

    }
}
