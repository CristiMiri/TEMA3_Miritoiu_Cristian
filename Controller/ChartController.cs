using LiveCharts;
using LiveCharts.Wpf;
using PS_TEMA3.Model.Repository;
using PS_TEMA3.View;
using System.Linq;

namespace PS_TEMA3.Controller
{
    internal class ChartController
    {
        private StatisticsRepository statisticsRepository;
        private ChartView chartView;

        public ChartController(ChartView chartView)
        {
            this.chartView = chartView;
            statisticsRepository = new StatisticsRepository();

        }

        public void SetupParticipantsByConferenceChart()
        {
            var data = statisticsRepository.GetNumberOfParticipantsByConference();

            var series = new ColumnSeries
            {
                Title = "Participants",
                Values = new ChartValues<int>(data.Values)
            };

            var chart = chartView.GetCartesianChart();
            chart.Series = new SeriesCollection { series };
            chart.AxisX[0].Labels = data.Keys.ToArray();
            chart.AxisY[0].LabelFormatter = value => value.ToString("N");
        }

        public void SetupParticipantsBySectionChart()
        {
            var data = statisticsRepository.GetNumberOfParticipantsBySection();

            var series = new ColumnSeries
            {
                Title = "Participants by Section",
                Values = new ChartValues<int>(data.Values)
            };

            var chart = chartView.GetCartesianChart();
            chart.Series = new SeriesCollection { series };
            chart.AxisX[0].Labels = data.Keys.ToArray();
            chart.AxisY[0].LabelFormatter = value => value.ToString("N");
        }

        public void SetupPresentationsByAuthorChart()
        {
            var data = statisticsRepository.GetNumberOfPresentationsByAuthor();

            var series = new ColumnSeries
            {
                Title = "Presentations by Author",
                Values = new ChartValues<int>(data.Values)
            };

            var chart = chartView.GetCartesianChart();
            chart.Series = new SeriesCollection { series };
            chart.AxisX[0].Labels = data.Keys.ToArray();
            chart.AxisY[0].LabelFormatter = value => value.ToString("N");
        }

        public void SetupPresentationsPerDayChart()
        {
            var data = statisticsRepository.GetNumberOfPresentationsPerDay();

            var series = new LineSeries
            {
                Title = "Presentations per Day",
                Values = new ChartValues<int>(data.Values)
            };

            var chart = chartView.GetCartesianChart();
            chart.Series = new SeriesCollection { series };
            chart.AxisX[0].Labels = data.Keys.Select(date => date.ToString("d")).ToArray();
            chart.AxisY[0].LabelFormatter = value => value.ToString("N");
        }

        public void SetupRingChart()
        {
            var data = statisticsRepository.GetNumberOfParticipantsBySection();

            SeriesCollection seriesCollection = [];
            foreach (var item in data)
            {
                seriesCollection.Add(new PieSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<int> { item.Value },
                    DataLabels = true
                });
            }

            var pieChart = chartView.GetPieChart();

            // Clear existing series to avoid potential issues

            pieChart.Series = new SeriesCollection();
            pieChart.Series.AddRange(seriesCollection);
        }

        public void SetupLineChart()
        {
            var data = statisticsRepository.GetNumberOfPresentationsPerDay();

            var series = new LineSeries
            {
                Title = "Presentations per Day",
                Values = new ChartValues<int>(data.Values)
            };

            var lineChart = chartView.GetLineChart();
            lineChart.Series = new SeriesCollection { series };
            lineChart.AxisX[0].Labels = data.Keys.Select(date => date.ToString("d")).ToArray();
            lineChart.AxisY[0].LabelFormatter = value => value.ToString("N");
        }
    }
}
