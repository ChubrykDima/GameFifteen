using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game15.Model;
using System.Data.Entity;

namespace Game15
{
    public partial class RecordWindow : Form
    {
        UserContext _db;
        public RecordWindow()
        {
            InitializeComponent();
            _db = new UserContext();
            _db.Users.Load();

            dataGridView1.DataSource = _db.Users.Local.ToBindingList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _db = new UserContext();
            
            if (dataGridView1.RowCount>0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                var user = _db.Users.Find(id);
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
            MessageBox.Show("Рекорд обнулен");
            Close();
        }
    }
}
