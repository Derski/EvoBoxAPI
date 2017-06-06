using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EvoBoxAPI;

namespace WinForms_BoxApi_Tester
{
    public partial class Form_ClientJobInfo : Form
    {
        IClientJobInfo _clientJobInfo;
        public string JobId { get; set; }
        public string ClientId { get; set; }

        public Form_ClientJobInfo(IClientJobInfo clientJobInfo)
        {
            InitializeComponent();
            _clientJobInfo = clientJobInfo;
        }

        private void Form_ClientJobInfo_Load(object sender, EventArgs e)
        {
            comboBox_Clients.DataSource = _clientJobInfo.GetClientList();
        }

        private void comboBox_Clients_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClientId = comboBox_Clients.SelectedValue.ToString();
            List<string> clientJobs = _clientJobInfo.GetJobListPerClient(ClientId);
            dataGridView_JobIds.Columns.Clear();
            dataGridView_JobIds.Rows.Clear();

            dataGridView_JobIds.Columns.Add("JobId","Job Id");
            dataGridView_JobIds.Columns["JobId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            foreach (string job in clientJobs)
            {
                dataGridView_JobIds.Rows.Add(job);
            }
            dataGridView_JobIds.Refresh();
        }

        private void button_Select_Click(object sender, EventArgs e)
        {
            if(dataGridView_JobIds.SelectedRows!=null && dataGridView_JobIds.SelectedRows.Count==1)
            {
                JobId =  dataGridView_JobIds.SelectedRows[0].Cells[0].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
