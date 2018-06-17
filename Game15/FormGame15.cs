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

namespace Game15
{
    public partial class FormGame15 : Form
    {
        private int _count;
        private Game _game;
        private UserContext context;
        private User user;
        public FormGame15()
        {
            InitializeComponent();
            _game = new Game(4);
            context = new UserContext();
            user = new User();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            _count++;
            int position = Convert.ToInt16(((Button)sender).Tag);
            _game.Shift(position);
            Refresh();
            if (_game.CheckNumbers())
            {
                if (CheckRecord(_count))
                {
                    start_game();
                }
                else
                {
                    MessageBox.Show($"Поздравляю, вы прошли игру за {_count} {_game.CheckStep(_count)}!");
                    start_game();
                }
            }
        }
        private Button Button(int position)
        {
            switch (position)
            {
                case 0: return button0;
                case 1: return button1;
                case 2: return button2;
                case 3: return button3;
                case 4: return button4;
                case 5: return button5;
                case 6: return button6;
                case 7: return button7;
                case 8: return button8;
                case 9: return button9;
                case 10: return button10;
                case 11: return button11;
                case 12: return button12;
                case 13: return button13;
                case 14: return button14;
                case 15: return button15;
                default: return null;
            }
        }

        private void FormGame15_Load(object sender, EventArgs e)
        {
            start_game();
        }

        private void Menu_Start_Game_Click(object sender, EventArgs e)
        {
            start_game();
        }

        private void start_game()
        {
            _count = 0;
            _game.Start();
            for (int i = 0; i < 100; i++)
                _game.ShiftRandom();
            Refresh();
        }

        private void Refresh()
        {
            for (var position = 0; position < 16; position++)
            {
                var nr = _game.GetNumber(position);
                Button(position).Text = nr.ToString();
                Button(position).Visible = (nr > 0);
            }
        }

        private void Menu_records_Click(object sender, EventArgs e)
        {
            var rw = new RecordWindow();
            rw.Show();
        }

        public bool CheckRecord(int step)
        {
            var max = 0;
            try
            {
                max = context.Users.Max(x => x.Step);
            }
            catch (InvalidOperationException e)
            {
                user.Step = step;
                context.Users.Add(user);
                WriteName();
                return true;
            }

            if (step < max && step > 0)
            {
                user.Step = step;
                context.Users.Add(user);
                WriteName();
                return true;
            }
            return false;
        }

        public void WriteName()
        {
            var nf = new NameForm();
            var result = nf.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            if (result == DialogResult.OK)
            {
                user.Name = nf.textBoxName.Text;

                if (String.IsNullOrEmpty(user.Name))
                    MessageBox.Show("Введите пожалуйста свое имя!");
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
