using LiveCharts.Wpf;
using PS_TEMA3.Controller;
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
    /// Interaction logic for ChartView.xaml
    /// </summary>
    public partial class ChartView : Window
    {
        private ChartController chartController;
        public ChartView()
        {
            InitializeComponent();
            ChartController chartController = new ChartController(this);
        }


        public CartesianChart GetCartesianChart()
        {
            return this.CartesianChart;
        }

        public PieChart GetPieChart()
        {
            return this.PieChart;
        }

        public CartesianChart GetLineChart()
        {
            return this.LineChart;
        }

        private void HideAllCharts()
        {
            CartesianChart.Visibility = Visibility.Hidden;
            PieChart.Visibility = Visibility.Hidden;
            LineChart.Visibility = Visibility.Hidden;
        }

        public void ShowCartesianChart()
        {
            HideAllCharts();
            CartesianChart.Visibility = Visibility.Visible;
        }

        public void ShowPieChart()
        {
            HideAllCharts();
            PieChart.Visibility = Visibility.Visible;
        }

        public void ShowLineChart()
        {
            HideAllCharts();
            LineChart.Visibility = Visibility.Visible;
        }


        public void ShowParticipantsByConferenceChart()
        {           
            ShowCartesianChart();
            chartController = new ChartController(this);
            chartController.SetupParticipantsByConferenceChart();
        }
        public void ShowParticipantsBySectionChart()
        {
            
            ShowCartesianChart();
            chartController = new ChartController(this);
            chartController.SetupParticipantsBySectionChart();
        }
        public void ShowPresentationsByAuthorChart()
        {
           
            ShowCartesianChart();
            chartController = new ChartController(this);
            chartController.SetupPresentationsByAuthorChart();
        }
        public void ShowPresentationsPerDayChart()
        {
            
            ShowCartesianChart();
            chartController = new ChartController(this);
            chartController.SetupPresentationsPerDayChart();
        }
        public void ShowRingChart()
        {
            
            ShowPieChart();
            chartController = new ChartController(this);
            chartController.SetupRingChart();
        }
        public void ShowLineChartDialog()
        {
           
            ShowLineChart();
            chartController = new ChartController(this);
            chartController.SetupLineChart();

        }


    }
}
