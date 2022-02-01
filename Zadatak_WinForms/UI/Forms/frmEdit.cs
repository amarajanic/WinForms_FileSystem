using Library.DataAccess;
using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Forms
{
    public partial class frmEdit : Form
    {
        Osoba person;
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Zadatak.txt";
        TextFileHelper textFileHelper;
        public frmEdit(Osoba p)
        {
            InitializeComponent();
            person = p;
            textFileHelper = new TextFileHelper();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            tbIme.Text = person.Ime;
            tbPrezime.Text = person.Prezime;
            tbJmbg.Text = person.JMBG;
            tbAdresa.Text = person.Adresa;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                List<Osoba> people = new List<Osoba>();

                people = textFileHelper.ReadFromFile(filePath);

                var index = people.FindIndex(x => x.JMBG == person.JMBG);

                people[index] = new Osoba()
                {
                    Ime = tbIme.Text,
                    Prezime = tbPrezime.Text,
                    Adresa = tbAdresa.Text,
                    JMBG = tbJmbg.Text
                };

                textFileHelper.WriteToFile(filePath, people);

                this.Close();
            }
            

          
        }

        private void tbIme_Validating(object sender, CancelEventArgs e)
        {
            if (tbIme.Text == String.Empty)
            {
                e.Cancel = true;
                tbIme.Focus();
                epIme.SetError(tbIme, "Ime can't be empty");
            }
            else
            {
                e.Cancel = false;
                epIme.SetError(tbIme, "");
            }
        }

        private void tbPrezime_Validating(object sender, CancelEventArgs e)
        {
            if (tbPrezime.Text == String.Empty)
            {
                e.Cancel = true;
                tbPrezime.Focus();
                epPrezime.SetError(tbPrezime, "Prezime can't be empty");
            }
            else
            {
                e.Cancel = false;
                epPrezime.SetError(tbPrezime, "");
            }
        }

        private void tbJmbg_Validating(object sender, CancelEventArgs e)
        {
            if (tbJmbg.Text.Length != 13)
            {
                e.Cancel = true;
                tbJmbg.Focus();
                epJmbg.SetError(tbJmbg, "JMBG length must be 13");
            }
            else
            {
                e.Cancel = false;
                epJmbg.SetError(tbJmbg, "");
            }
        }

        private void tbAdresa_Validating(object sender, CancelEventArgs e)
        {
            if (tbAdresa.Text == String.Empty)
            {
                e.Cancel = true;
                tbAdresa.Focus();
                epAdresa.SetError(tbAdresa, "Adresa can't be empty");
            }
            else
            {
                e.Cancel = false;
                epAdresa.SetError(tbAdresa, "");
            }
        }

        private void tbJmbg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
