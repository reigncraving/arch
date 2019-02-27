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

namespace Arch
{
    public partial class ArchForm1 : Form
    {
        public ArchForm1()
        {
            InitializeComponent();
            ArchRichBox.ForeColor = Color.FromArgb(65, 65, 65);
        }

        public bool isTextChanged = false;
        public string fileName = "";


        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                if (statusBarToolStripMenuItem.Checked == true)
                {
                    statusStrip1.Visible = true;
                }
                else if (statusBarToolStripMenuItem.Checked == false)
                {
                    statusStrip1.Visible = false;
                }
            }
        }
        // about form
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (About AboutFrm = new About())
            {
                AboutFrm.ShowDialog();
            }
        }
        // Creat new doc
        private void newArchFomr()
        {
            using (ArchForm1 newArch = new ArchForm1())
            {
                fileName = "";
                statusFileName.Text = "Untitled.txt";
                newArch.Show();
                ArchRichBox.ForeColor = Color.FromArgb(65, 65, 65);
                ArchRichBox.Font = new Font("Microsoft Sans", 12, FontStyle.Regular);
                ArchRichBox.Clear();
            }
        }
        // New file button
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isTextChanged == true)
            {
                DialogResult result;
                result = MessageBox.Show("You are about to close file, any unsaved data will be lost! Do you want to save your changes?", "New file",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                    newArchFomr();
                }
                else if (result == DialogResult.No)
                {
                    newArchFomr();
                }
            }
        }

        // Open new file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog opnFile = new OpenFileDialog())
                {
                    if (opnFile.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(opnFile.FileName) == true)
                        {
                            fileName = opnFile.FileName;
                            statusFileName.Text = fileName;
                            StreamReader archReader = new StreamReader(opnFile.FileName);
                            string line = archReader.ReadLine();
                            ArchRichBox.Text = line;
                            ArchRichBox.AppendText(archReader.ReadToEnd());
                            archReader.Close();
                        }
                        else
                        {
                            MessageBox.Show("There is no such file!", "Error reading file!", MessageBoxButtons.OK,
                            MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
            catch (FileLoadException)
            {
                MessageBox.Show("Cannot load file..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File missing...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("Oops something went wrong...", "Oops..", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isTextChanged == false)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else if (fileName == "")
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            { 
                try
                {
                    StreamWriter svFile = new StreamWriter(fileName);
                    svFile.WriteLine(ArchRichBox.Text);
                    svFile.Close();
                }
                catch (FileLoadException)
                {
                    MessageBox.Show("Cannot load file..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("File missing...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException)
                {
                    MessageBox.Show("Oops something went wrong...", "Oops..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //Save as
        public void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveAsFile = new SaveFileDialog())
            {
                try
                {
                    saveAsFile.Filter = "text file|*txt";
                    if (saveAsFile.ShowDialog() == DialogResult.OK)
                    {
                        fileName = saveAsFile.FileName;
                        statusFileName.Text = fileName;
                        StreamWriter archWrite = new StreamWriter(saveAsFile.FileName);
                        archWrite.WriteLine(ArchRichBox.Text);
                        archWrite.Close();
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("File not found!", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                }
                catch (IOException)
                {
                    MessageBox.Show("Oops something went wrong...", "Oops..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrintDialog printA = new PrintDialog())
            {
                if (printA.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (isTextChanged == true)
            {
                DialogResult result;
                result = MessageBox.Show("You are about to close file, any unsaved data will be lost! Do you want to save your changes?", "Closing Programme",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }
                if (result == DialogResult.No) 
                {
                    Application.Exit();
                }
            }
            if ( fileName == "" || isTextChanged == false)
            {
                Application.Exit();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchRichBox.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchRichBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchRichBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchRichBox.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchRichBox.SelectedText = "";
        }

        public void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FontDialog fonts = new FontDialog())
            {
                if (fonts.ShowDialog() == DialogResult.OK)
                {
                    ArchRichBox.Font = fonts.Font;
                }
            }
        }
        //redo
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ArchRichBox.Redo();
        }

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog archColor = new ColorDialog())
            {
                if (archColor.ShowDialog() == DialogResult.OK)
                {
                    ArchRichBox.ForeColor = archColor.Color;
                }
            }
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            undoToolStripMenuItem_Click(sender, e);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripMenuItem3_Click(sender, e);
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(sender, e);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender, e);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(sender, e);
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            deleteToolStripMenuItem_Click(sender, e);
        }
        // text was changed event
        private void ArchRichBox_TextChanged(object sender, EventArgs e)
        {
            ArchRichBox.TextChanged += new EventHandler(this.TextWasChanged);
            char chEmpty = ' ';
            int counter = 0;
            for (int i = 0; i <= ArchRichBox.TextLength; i++)
            {
                if (ArchRichBox.Text.Contains(chEmpty))
                {
                    counter = ArchRichBox.Text.Trim().Length / 5;
                }
            }
            StatusWordCount.Text = counter.ToString();
        }

        private void TextWasChanged(object sender, EventArgs e)
        {
            isTextChanged = true;
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void StatusWordCount_Click(object sender, EventArgs e)
        {

        }
        //Cheks if text was changed
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ArchRichBox.Text != "")
            {
                isTextChanged = true;
            }
            else
            {
                isTextChanged = false;
            }
        }



        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ArchForm1_Load(object sender, EventArgs e)
        {

        }
        // close window button ( Master close event ) 
        private void ArchForm1_FormClosed(object sender, FormClosedEventArgs e)
        {
            exitToolStripMenuItem_Click_1(sender, e);
        }
    }
}
