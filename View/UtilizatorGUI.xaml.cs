﻿using PS_TEMA3.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PS_TEMA3.View
{
    /// <summary>
    /// Interaction logic for UtilizatorGUI.xaml
    /// </summary>
    public partial class UtilizatorGUI : Window
    {
        public UtilizatorGUI()
        {
            InitializeComponent();
            UtilizatorController utilizatorController = new UtilizatorController(this);
        }

        public Button GetBackButton()
        {
            return this.BackButton;
            //TODO: Implement Method
        }

        public DataGrid GetTabelConferinte()
        {
            return this.ConferenceTable;
        }
    }

}
