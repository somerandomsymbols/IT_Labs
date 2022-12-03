using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalDB
{
    public partial class Form1 : Form
    {
        List<TabPage> tabPages = new List<TabPage>();
        int mclcs;

        public Form1()
        {
            InitializeComponent();
            this.tabControl1.Controls.Clear();
            DatabaseManager.Instance.Create();
            this.UpdateFormFromDatabase();
        }

        private void UpdateFormFromDatabase()
        {
            foreach (TabPage page in this.tabPages)
            {
                this.tabControl1.Controls.Remove(page);
                page.Dispose();
            }

            this.tabPages.Clear();
            this.toolStripTextBoxTableName.Text = DatabaseManager.Instance.Database.Name;
            this.toolStripTextBoxTableRename.Text = "";
            this.toolStripComboBoxTableDelete.Text = "";
            this.toolStripComboBoxTableProduct1.Text = "";
            this.toolStripComboBoxTableProduct2.Text = "";
            int tabIndex = 0;
            this.toolStripComboBoxTableDelete.Items.Clear();
            this.toolStripComboBoxTableProduct1.Items.Clear();
            this.toolStripComboBoxTableProduct2.Items.Clear();

            foreach (Table table in DatabaseManager.Instance.Database.Tables)
            {
                System.Windows.Forms.TabPage tablePage = new System.Windows.Forms.TabPage();
                System.Windows.Forms.TabControl tableTabControl = new System.Windows.Forms.TabControl();
                System.Windows.Forms.TabPage dataPage = new System.Windows.Forms.TabPage();
                System.Windows.Forms.TabPage templatePage = new System.Windows.Forms.TabPage();
                System.Windows.Forms.DataGridView dataGridViewData = new System.Windows.Forms.DataGridView();
                System.Windows.Forms.DataGridView dataGridViewTemplate = new System.Windows.Forms.DataGridView();

                this.tabPages.Add(tablePage);
                this.tabControl1.Controls.Add(tablePage);
                this.toolStripComboBoxTableDelete.Items.Add(table.Name);
                this.toolStripComboBoxTableProduct1.Items.Add(table.Name);
                this.toolStripComboBoxTableProduct2.Items.Add(table.Name);
                tablePage.Controls.Add(tableTabControl);
                tablePage.Location = new System.Drawing.Point(4, 29);
                tablePage.Name = "tabPage" + DatabaseManager.Instance.Database.Name + table.Name;
                tablePage.Padding = new System.Windows.Forms.Padding(3);
                tablePage.Size = new System.Drawing.Size(792, 389);
                tablePage.TabIndex = tabIndex++;
                tablePage.Text = table.Name;
                tablePage.UseVisualStyleBackColor = true;

                tableTabControl.Controls.Add(dataPage);
                tableTabControl.Controls.Add(templatePage);
                tableTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
                tableTabControl.Location = new System.Drawing.Point(0, 0);
                tableTabControl.Name = "tabControl" + DatabaseManager.Instance.Database.Name + table.Name;
                tableTabControl.SelectedIndex = 0;
                tableTabControl.Size = new System.Drawing.Size(792, 389);
                tableTabControl.TabIndex = 0;

                dataPage.Controls.Add(dataGridViewData);
                dataPage.Location = new System.Drawing.Point(4, 29);
                dataPage.Name = "tabPage" + DatabaseManager.Instance.Database.Name + table.Name + "Data";
                dataPage.Padding = new System.Windows.Forms.Padding(3);
                dataPage.Size = new System.Drawing.Size(784, 356);
                dataPage.TabIndex = 0;
                dataPage.Text = "data";
                dataPage.UseVisualStyleBackColor = true;

                templatePage.Controls.Add(dataGridViewTemplate);
                templatePage.Location = new System.Drawing.Point(4, 29);
                templatePage.Name = "tabPage" + DatabaseManager.Instance.Database.Name + table.Name + "Template";
                templatePage.Padding = new System.Windows.Forms.Padding(3);
                templatePage.Size = new System.Drawing.Size(242, 92);
                templatePage.TabIndex = 1;
                templatePage.Text = "template";
                templatePage.UseVisualStyleBackColor = true;

                DatabaseEventHandler databaseEventHandler = new DatabaseEventHandler(table, dataGridViewData, dataGridViewTemplate, DatabaseManager.Instance.Database, DatabaseManager.Instance.Filepath);
                dataGridViewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridViewData.Dock = System.Windows.Forms.DockStyle.Fill;
                dataGridViewData.Location = new System.Drawing.Point(3, 3);
                dataGridViewData.Name = "dataGridView" + DatabaseManager.Instance.Database.Name + table.Name + "Data";
                dataGridViewData.RowHeadersWidth = 51;
                dataGridViewData.RowTemplate.Height = 29;
                dataGridViewData.Size = new System.Drawing.Size(778, 350);
                dataGridViewData.TabIndex = 0;

                dataGridViewTemplate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridViewTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
                dataGridViewTemplate.Location = new System.Drawing.Point(3, 3);
                dataGridViewTemplate.Name = "dataGridView" + DatabaseManager.Instance.Database.Name + table.Name + "Template";
                dataGridViewTemplate.RowHeadersWidth = 51;
                dataGridViewTemplate.RowTemplate.Height = 29;
                dataGridViewTemplate.Size = new System.Drawing.Size(778, 350);
                dataGridViewTemplate.TabIndex = 0;

                dataGridViewTemplate.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Name",
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });

                var col = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Type",
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };

                foreach (string s in Column.getTypes())
                {
                    col.Items.Add(s);
                }

                dataGridViewTemplate.Columns.Add(col);
                databaseEventHandler.PopulateTemplateFromTable();

                dataGridViewData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(databaseEventHandler.updateData);
                dataGridViewData.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(databaseEventHandler.updateData);
                dataGridViewData.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(databaseEventHandler.updateData);
                dataGridViewTemplate.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(databaseEventHandler.updateTemplate);
                dataGridViewTemplate.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(databaseEventHandler.updateTemplate);
                dataGridViewTemplate.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(databaseEventHandler.updateTemplate);
            }

            /*foreach ()
            
            lcol = jsonObject.Item1;
            ldat = jsonObject.Item2;

            this.dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Name"
            });
            var col = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "Type",
                HeaderText = "Type"
            };

            col.Items.AddRange(Column.getTypes());
            this.dataGridView2.Columns.Add(col);

            foreach (Column c in lcol)
            {
                this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "A",
                    HeaderText = "Заголовок 1"
                });
            }

            this.dataGridView1.DataSource = ldat;
            this.dataGridView2.DataSource = lcol;

            this.dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "A",
                HeaderText = "Заголовок 1"
            });
            this.dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "B",
                HeaderText = "Заголовок 2"
            });
            this.dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "C",
                HeaderText = "Заголовок 3"
            });
            this.data = new List<MyClass>
                           {
                               new MyClass("1", "2", "3"),
                               new MyClass("4", "5", "6"),
                               new MyClass("7", "8", "9")
                           };
            this.dataGridView2.DataSource = data;
            //System.Diagnostics.Debug.WriteLine(this.dataGridView2.Columns[1].ContextMenuStrip.Items);
            this.mclcs = 0;*/
        }

        private void CauseUpdate(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Shit " + ++mclcs);
            //System.Diagnostics.Debug.WriteLine(this.dataGridView2.Rows[0].Cells[0].Value);
        }

        private void MenuItemSaveClick(object sender, EventArgs e)
        {
            if (!DatabaseManager.Instance.Save())
                MenuItemSaveAsClick(sender, e);
        }

        private void MenuItemSaveAsClick(object sender, EventArgs e)
        {
            this.saveFileDialog1.Title = "Save as";
            this.saveFileDialog1.FileName = DatabaseManager.Instance.Database.Name;
            this.saveFileDialog1.DefaultExt = "database";
            this.saveFileDialog1.OverwritePrompt = true;

            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                DatabaseManager.Instance.Save(this.saveFileDialog1.FileName);
        }

        private void MenuItemOpenClick(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Open";

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.Diagnostics.Debug.WriteLine("Open " + this.openFileDialog1.FileName);

                if (DatabaseManager.Instance.Read(this.openFileDialog1.FileName))
                    this.UpdateFormFromDatabase();
            }
        }

        private void MenuItemNewClick(object sender, EventArgs e)
        {
            this.newFileDialog1.Title = "New";
            this.newFileDialog1.FileName = "New database";
            this.newFileDialog1.DefaultExt = "database";
            this.newFileDialog1.OverwritePrompt = true;

            if (this.newFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DatabaseManager.Instance.Create();
                DatabaseManager.Instance.Save(this.newFileDialog1.FileName);
                this.UpdateFormFromDatabase();
            }
        }

        private void ToolStripTableNameTextChanged(object sender, EventArgs e)
        {
            DatabaseManager.Instance.Database.Name = this.toolStripTextBoxTableName.Text;
        }

        private void MenuItemTableNewClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.toolStripTextBoxTableNew.Text))
            {
                foreach (Table table in DatabaseManager.Instance.Database.Tables)
                    if (table.Name.Equals(this.toolStripTextBoxTableNew.Text))
                        return;

                DatabaseManager.Instance.Database.Tables.Add(new Table()
                {
                    Name = this.toolStripTextBoxTableNew.Text
                });

                this.toolStripTextBoxTableNew.Text = "";
                this.UpdateFormFromDatabase();
            }
        }

        private void MenuItemTableRenameClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.toolStripTextBoxTableRename.Text))
            {
                foreach (Table table in DatabaseManager.Instance.Database.Tables)
                    if (table.Name.Equals(this.toolStripTextBoxTableRename.Text))
                        return;

                foreach (Table table in DatabaseManager.Instance.Database.Tables)
                    if (table.Name.Equals(this.tabControl1.SelectedTab.Text))
                    {
                        table.Name = this.toolStripTextBoxTableRename.Text;
                        this.tabControl1.SelectedTab.Text = table.Name;
                        this.toolStripTextBoxTableRename.Text = "";
                        return;
                    }
            }
        }

        private void MenuItemTableDeleteClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.toolStripComboBoxTableDelete.Text))
            {
                Table t = null;

                foreach (Table table in DatabaseManager.Instance.Database.Tables)
                    if (table.Name.Equals(this.toolStripComboBoxTableDelete.Text))
                    {
                        t = table;
                        break;
                    }

                if (t != null)
                {
                    DatabaseManager.Instance.Database.Tables.Remove(t);
                    this.UpdateFormFromDatabase();
                }
            }
        }

        private void MenuItemTableProductClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.toolStripTextBoxTableProductName.Text) ||
                String.IsNullOrWhiteSpace(this.toolStripComboBoxTableProduct1.Text) ||
                String.IsNullOrWhiteSpace(this.toolStripComboBoxTableProduct2.Text))
                return;

            foreach (Table table in DatabaseManager.Instance.Database.Tables)
                if (table.Name.Equals(this.toolStripTextBoxTableProductName.Text))
                    return;

            Table table1 = null;

            foreach (Table table in DatabaseManager.Instance.Database.Tables)
                if (table.Name.Equals(this.toolStripComboBoxTableProduct1.Text))
                {
                    table1 = table;
                    break;
                }

            if (table1 == null)
                return;

            Table table2 = null;

            foreach (Table table in DatabaseManager.Instance.Database.Tables)
                if (table.Name.Equals(this.toolStripComboBoxTableProduct2.Text))
                {
                    table2 = table;
                    break;
                }

            if (table2 == null)
                return;

            Table t = DatabaseManager.CartesianProduct(table1, table2);
            t.Name = this.toolStripTextBoxTableProductName.Text;
            DatabaseManager.Instance.Database.Tables.Add(t);
            this.UpdateFormFromDatabase();
        }
    }
}
