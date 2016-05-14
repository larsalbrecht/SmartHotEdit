using ScintillaNET;
using SmartHotEdit.Abstracts;
using SmartHotEdit.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartHotEdit.View
{
    public partial class ScriptPluginEditor : Form
    {
        private MainController mainController;
        private IList<AScriptPluginController> scriptPluginController;

        public ScriptPluginEditor(MainController mainController)
        {
            InitializeComponent();

            this.mainController = mainController;
            this.scriptPluginController = this.mainController.getPluginController().getPluginControllerList(typeof(AScriptPluginController)).Cast<AScriptPluginController>().ToList();
            this.openScriptDialog.Filter = this.getFilterForOpenDialog();
            this.scriptTypeList.Items.AddRange(this.getItemsForScriptTypeBox());
        }

        private ListViewItem[] getItemsForScriptTypeBox()
        {
            var resultList = new List<ListViewItem>();
            foreach (AScriptPluginController pluginController in this.scriptPluginController)
            {
                var tempItem = new ListViewItem();
                tempItem.Text = pluginController.Type;
                tempItem.Tag = pluginController;
                resultList.Add(tempItem);
            }
            return resultList.ToArray<ListViewItem>();
        }

        private String getFilterForOpenDialog()
        {
            var resultList = new List<string>();
            foreach (AScriptPluginController pluginController in this.scriptPluginController)
            {
                resultList.Add(pluginController.Type + "|" + "*" + pluginController.TypeFileExt);
            }
            return string.Join("|", resultList);
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openScriptDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string scriptFile = this.openScriptDialog.FileName;
                try
                {
                    string text = File.ReadAllText(scriptFile);
                    this.scintilla.Text = text;
                }
                catch (IOException)
                {
                }
            }
        }
    }
}
