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
    public partial class ComponentDragLoad : ContentPage
    {
        public ComponentDragLoad()
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
                label1.Text = "Fabric speed at point n (m/min)";
                label2.Text = "Fabric speed on slack side (m/min)";
                label4.Text = "Elastic modulus at\r\nreference temperature r (kN/m)";
                label5.Text = "Modulus/temperature (kN/m/°C)";
                label6.Text = "Slack side tension (kN/m)";
                label7.Text = "Drag load (kN/m)";
                s.Text = "SI";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "Fabric speed at point n (ft/min)";
                label2.Text = "Fabric speed on slack side (ft/min)";
                label4.Text = "Elastic modulus at\r\nreference temperature r (lbf/in.)";
                label5.Text = "Modulus/temperature (lbf/in./°F)";
                label6.Text = "Slack side tension (lbf/in.)";
                label7.Text = "Drag load (lbf/in.)";
                s.Text = "US";
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
            if (string.IsNullOrWhiteSpace(Vn.Text) || string.IsNullOrWhiteSpace(Vs.Text) || string.IsNullOrWhiteSpace(EMr.Text) || string.IsNullOrWhiteSpace(K.Text) || string.IsNullOrWhiteSpace(Ts.Text))
            {
                label7.Text = "Please enter all values.";
            }
            else if (Double.TryParse(Vn.Text, out num) && Double.TryParse(Vs.Text, out num) && Double.TryParse(EMr.Text, out num) && Double.TryParse(K.Text, out num) && Double.TryParse(Ts.Text, out num))
            {
                var speedN = double.Parse(Vn.Text, CultureInfo.InvariantCulture);
                var speedS = double.Parse(Vs.Text, CultureInfo.InvariantCulture);
                var elasticModr = double.Parse(EMr.Text, CultureInfo.InvariantCulture);
                var constant = double.Parse(K.Text, CultureInfo.InvariantCulture);
                var slackSideTension = double.Parse(Ts.Text, CultureInfo.InvariantCulture);

                var EM = elasticModr - (constant * slackSideTension);
                var load = (speedN / (speedS - 1.0)) * (EM - slackSideTension);

                if (!App.switched)
                {
                    label7.Text = "Drag load: " + String.Format("{0:n4}", load) + " lbf/in.";
                }
                else
                {
                    label7.Text = "Drag load: " + String.Format("{0:n4}", load) + " kN/m";
                }
            }
            else
            {
                label7.Text = "Please enter a valid number.";
            }
        }
    }
}