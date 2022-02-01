using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Library.DataAccess;

namespace UI.Forms
{
    public partial class frmAddNew : Form
    {
        List<Osoba> people;
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Zadatak.txt";
        TextFileHelper textFileHelper;
        public frmAddNew(List<Osoba>p)
        {
            InitializeComponent();
            people = p;
            textFileHelper = new TextFileHelper();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(ValidateChildren(ValidationConstraints.Enabled))
            {         

                Osoba person = new Osoba()
                {
                    Ime = tbIme.Text,
                    Prezime = tbPrezime.Text,
                    JMBG = tbJmbg.Text,
                    Adresa = tbAdresa.Text
                };

                people.Add(person);

                textFileHelper.WriteToFile(filePath, people);


                this.Close();

            }
    
        }

        private void tbJmbg_Validating(object sender, CancelEventArgs e)
        {
            if(tbJmbg.Text.Length != 13)
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

        private void tbJmbg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void tbIme_Validating(object sender, CancelEventArgs e)
        {
            if(tbIme.Text == String.Empty)
            {
                e.Cancel= true;
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
    }
}
