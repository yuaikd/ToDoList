using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace to_do_list
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable todoList = new DataTable();
        bool isEditing = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            todoList.Columns.Add("Done", typeof(bool));
            toDoListView.DataSource = todoList;
            toDoListView.Columns["Done"].HeaderText = "Wykonane";
            toDoListView.Columns["Done"].Width = 60;
            todoList.Columns.Add("Title");
            todoList.Columns.Add("Description");

            
            toDoListView.DataSource = todoList;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void newButton_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                todoList.Rows[toDoListView.CurrentCell.RowIndex]["Title"] = titleTextBox.Text;
                todoList.Rows[toDoListView.CurrentCell.RowIndex]["Description"] = descriptionTextBox.Text;
            }
            else
            {
                todoList.Rows.Add(false,titleTextBox.Text, descriptionTextBox.Text);
            }
            titleTextBox.Text = "";
            descriptionTextBox.Text = "";
            isEditing = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            isEditing = true;
           
            titleTextBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[0].ToString();
            descriptionTextBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[1].ToString();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                todoList.Rows[toDoListView.CurrentCell.RowIndex].Delete();
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
        
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Plik tekstowy (*.txt)|*.txt";
                saveFileDialog.Title = "Zapisz listę zadań";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow row in todoList.Rows)
                    {
                        sb.AppendLine($"{row["Title"]}\t{row["Description"]}");
                    }
                    System.IO.File.WriteAllText(saveFileDialog.FileName, sb.ToString());
                    MessageBox.Show("Lista zadań została zapisana.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void toDoListView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (toDoListView.Columns[e.ColumnIndex].Name == "Done")
            {
                
            }
        }

        private void toDoListView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (toDoListView.IsCurrentCellDirty)
            {
                toDoListView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            
        }
    }
    }

