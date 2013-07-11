﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ManagedWinapi.Windows;
using WiredIn.Constants;

namespace WiredIn
{
    public partial class SettingForm : Form
    {
        ShowVisualizations m_parent; // Ref to the parent form

        public SettingForm(ShowVisualizations parent)
        {
            InitializeComponent();
            m_parent = parent;
            setUIValues();     
        }

        //set UI values according to global settings
        private void setUIValues()
        {
            switch (Constants.Config.APP_SIZE)
            {
                case AppSize.Full:
                    rbFullScreen.Checked = true;
                    break;
                case AppSize.Small:
                    rbSizeSmall.Checked = true;
                    break;
            }
            switch (Constants.Config.VIS_IMAGE)
            {
                case Visualization.ManyStepImages:
                    rdbFlower.Checked = true;
                    break;
                case Visualization.Progressbar:
                    rdbProgbar.Checked = true;
                    break;
            }

            switch (Constants.Config.CONDITION)
            {
                case OperandCondition.reward:
                    rbReward.Checked = true;
                    break;
                case OperandCondition.punish:
                    rbPunishment.Checked = true;
                    break;
            }
            cbTopMost.Checked = Config.TOPMOST;
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void crosshair1_CrosshairDragged(object sender, EventArgs e)
        {
            update();
        }

        private void crosshair1_CrosshairDragging(object sender, EventArgs e)
        {
            update();
        }

        private void update()
        {
            SystemWindow sw = SystemWindow.FromPointEx(MousePosition.X, MousePosition.Y, false, false);
            lbl_title.Text = sw.Title;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.lbxWhiteList.Items.Add(lbl_title.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selected = this.lbxWhiteList.SelectedIndices;
            foreach (int i in selected)
            {
                this.lbxWhiteList.Items.RemoveAt(i);
            }         
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSizeSmall.Checked)
            {
                Config.APP_SIZE = AppSize.Small;
                m_parent.SwitchAppSize();
            }           
        }        

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFullScreen.Checked)
            {
                Config.APP_SIZE = AppSize.Full;
                m_parent.SwitchAppSize();
            }           
        }

        private void rbReward_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReward.Checked)
            {
                Config.CONDITION = OperandCondition.reward;
                //m_parent.setTransitSpeed();
            }
        }

        private void rbPunishment_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPunishment.Checked)
            {
                Config.CONDITION = OperandCondition.punish;
                //m_parent.setTransitSpeed();
            }
        }

        private void rdbProgbar_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbProgbar.Checked)
            {
                Config.VIS_IMAGE = Visualization.Progressbar;
                m_parent.CreateView();
                m_parent.AttachView();
            }
        }

        private void rdbFlower_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbFlower.Checked)
            {
                Config.VIS_IMAGE = Visualization.ManyStepImages;
                m_parent.CreateView();
                m_parent.AttachView();
            }
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbEmpty.Checked)
            {
                Config.VIS_IMAGE = Visualization.Empty;
                m_parent.CreateView();
                m_parent.AttachView();
            }
        }

    }
}