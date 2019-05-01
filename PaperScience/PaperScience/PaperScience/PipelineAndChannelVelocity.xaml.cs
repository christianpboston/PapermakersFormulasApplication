using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperScience
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PipelineAndChannelVelocity : ContentPage
    {
        public PipelineAndChannelVelocity()
        {
            InitializeComponent();

            if (App.switched)
            {
                units.IsToggled = true;
            }
            else
            {
                units.IsToggled = false;
            }
        }

        /*
       * Switch changed
       */
        private void Units_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;

            if (isToggled)
            {
                s.Text = "SI";
                label1.Text = "Flow (l/sec)";
                label2.Text = "Pipe inside radius (m)";
                label3.Text = "Pipe cross section area (m\u00B2)";
                label5.Text = "Note: screen to headbox acceptable range is 2.1 to 4.3 m/s";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Flow (gpm)";
                label2.Text = "Pipe inside radius (ft)";
                label3.Text = "Pipe cross section area (in\u00B2)";
                label5.Text = "Note: screen to headbox acceptable range is 7 to 14 ft/s";
                App.switched = false;
                answer.IsVisible = false;
            }
        }

        /*
         * Calculate button pressed
         */
        private void Button_Clicked(object sender, EventArgs e)
        {
            answer.IsVisible = true;
            double num;
            if ((string.IsNullOrWhiteSpace(Q.Text) && (string.IsNullOrWhiteSpace(R.Text) && string.IsNullOrWhiteSpace(A.Text)))
                || (string.IsNullOrWhiteSpace(R.Text) && string.IsNullOrWhiteSpace(A.Text)))
            {
                label4.Text = "Please enter all values.";
            }
            else if ((string.IsNullOrWhiteSpace(Q.Text)))
            {
                label4.Text = "Please enter flow";
            }
            else if ((Double.TryParse(Q.Text, out num) && Double.TryParse(R.Text, out num)) || (Double.TryParse(Q.Text, out num) && Double.TryParse(A.Text, out num)))
            {
                var flow = double.Parse(Q.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    if (R.Text.Length > 0)
                    {
                        if (string.IsNullOrWhiteSpace(A.Text))
                        {
                            var insideRadius = double.Parse(R.Text, CultureInfo.InvariantCulture);
                            var velocity = flow * 0.0007092 / Math.Pow(insideRadius, 2);
                            label4.Text = "Velocity: " + String.Format("{0:n4}", velocity) + " ft/s";
                        }
                        else
                        {
                            label4.Text = "Please enter ONLY 2 values.";
                        }
                    }
                    else
                    {
                        var area = double.Parse(A.Text, CultureInfo.InvariantCulture);
                        var velocity = flow * 0.321 / area;
                        label4.Text = "Velocity: " + String.Format("{0:n4}", velocity) + " ft/s";
                    }
                }
                else
                {
                    if (R.Text.Length > 0)
                    {
                        if (string.IsNullOrWhiteSpace(A.Text))
                        {
                            var insideRadius = double.Parse(R.Text, CultureInfo.InvariantCulture);
                            var velocity = flow * 3142.0 / Math.Pow(insideRadius, 2);
                            label4.Text = "Velocity: " + String.Format("{0:n4}", velocity) + " m/s";

                        }
                        else
                        {
                            label4.Text = "Please enter ONLY 2 values.";
                        }
                    }
                    else
                    {
                        var area = double.Parse(A.Text, CultureInfo.InvariantCulture);
                        var velocity = flow * 0.001 / area;
                        label4.Text = "Velocity: " + String.Format("{0:n4}", velocity) + " m/s";
                    }
                }
            }
            else
            {
                label4.Text = "Please enter a valid number.";
            }
        }
    }
}