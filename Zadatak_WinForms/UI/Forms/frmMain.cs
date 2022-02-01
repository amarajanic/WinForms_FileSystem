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
using Library.Models;
using Library.DataAccess;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace UI.Forms
{
    public partial class frmMain : Form
    {
        List<Osoba> people;
        TextFileHelper textFileHelper;

        public frmMain()
        {
            InitializeComponent();
            people = new List<Osoba>();
            textFileHelper = new TextFileHelper();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if(!File.Exists(textFileHelper.filePath))
            {
                try
                {
                    File.CreateText(textFileHelper.filePath).Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            people = textFileHelper.ReadFromFile();
      
            dgvOsobe.DataSource = people;

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNew frmAddNew = new frmAddNew(people);
            frmAddNew.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            frmAddNew.ShowDialog();
        }

        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {
            dgvOsobe.DataSource = null;
            people = textFileHelper.ReadFromFile();
            dgvOsobe.DataSource = people;
        }

        private void dgvOsobe_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvOsobe.SelectedRows.Count == 1)
            {
                btnEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = dgvOsobe.SelectedRows[0];

            Osoba o = new Osoba()
            {
                Ime = currentRow.Cells[0].Value.ToString(),
                Prezime = currentRow.Cells[1].Value.ToString(),
                JMBG = currentRow.Cells[2].Value.ToString(),
                Adresa = currentRow.Cells[3].Value.ToString()

            };

            frmEdit frmEdit = new frmEdit(o);
            frmEdit.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            frmEdit.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvOsobe.SelectedRows.Count > 0)
             {
                MessageBoxResult confirmResult = System.Windows.MessageBox.Show("Are you sure to delete this item(s) ?", "Delete conformation", MessageBoxButton.YesNo);

                if (confirmResult == MessageBoxResult.Yes)
                {

                    var selectedPeople = new List<Osoba>(dgvOsobe.SelectedRows.Count);

                    for (int i = 0; i < dgvOsobe.SelectedRows.Count; i++)
                    {
                        var selectedRow = dgvOsobe.SelectedRows[i];
                        var osoba = (Osoba)selectedRow.DataBoundItem;

                        selectedPeople.Add(osoba);

                    }

                    people = textFileHelper.ReadFromFile();

                    for (int i = 0; i < selectedPeople.Count; i++)
                    {
                        for (int j = 0; j < people.Count; j++)
                        {
                            if (selectedPeople[i].JMBG == people[j].JMBG)
                            {
                                people.RemoveAt(j);
                            }

                        }
                    }

                    textFileHelper.WriteToFile(people);

                    dgvOsobe.DataSource = null;
                    dgvOsobe.DataSource = people;

                }
            }
         
        }

        private void btnClose_Click(object sender, EventArgs e)
        {        
            List<Osoba> list = new List<Osoba>();

            foreach(DataGridViewRow dr in dgvOsobe.Rows)
            {
                Osoba o = new Osoba();

                foreach (DataGridViewCell dc in dr.Cells)
                {
                    o.Ime = dr.Cells[0].Value.ToString();
                    o.Prezime = dr.Cells[1].Value.ToString();
                    o.JMBG = dr.Cells[2].Value.ToString();
                    o.Adresa = dr.Cells[3].Value.ToString();
                }

                list.Add(o);
            }
     
            textFileHelper.WriteToFile(people);

            this.Close();
        }
    }
}
