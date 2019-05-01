using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PaperScience
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /*
         * Formula Page Navigation
         */
        private void ApproximateCriticalSpeedOfRoll(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ApproximateCriticalSpeedOfRoll());
        }
        private void ApproximateSpoutingVelocity(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ApproximateSpoutingVelocity());
        }
        private void BasisWeightConversions(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BasisWeightConversions());
        }
        private void ChangeInCrownOfTwoRolls(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangeInCrownOfTwoRolls());
        }
        private void CommonConversionFactors(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CommonConversionFactors());
        }
        private void ComponentDragLoad(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ComponentDragLoad());
        }
        private void CriticalSpeedOfCalenderRoll(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CriticalSpeedOfCalenderRoll());
        }
        private void DandyRollRotationalSpeed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DandyRollRotationalSpeed());
        }
        private void DeflectionOfRollOverFace(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DeflectionOfRollOverFace());
        }
        private void DragLoadConventional(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DragLoadConventional());
        }
        private void DryerFeltTension(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DryerFeltTension());
        }
        private void DryingRate(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DryingRate());
        }
        private void DryingRateCoated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DryingRateCoated());
        }
        private void FlowsTonsConsistencyRelationship(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FlowsTonsConsistencyRelationship());
        }
        private void FormationBladePulseFrequency(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormationBladePulseFrequency());
        }
        private void FourdrinierFormingLengthGuidelines(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FourdrinierFormingLengthGuidelines());
        }
        private void FourdrinierShakeNumber(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FourdrinierShakeNumber());
        }
        private void GasLaws(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GasLaws());
        }
        private void HeadboxFlowRate(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HeadboxFlowRate());
        }
        private void HeadboxFreeJetLength(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HeadboxFreeJetLength());
        }
        private void HeadboxSliceFlowRate(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HeadboxSliceFlowRate());
        }
        private void HydraulicPumpPower(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HydraulicPumpPower());
        }
        private void InertiaOfARoll(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InertiaOfARoll());
        }
        private void InstantaneousProductionRate(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InstantaneousProductionRate());
        }
        private void LinealPaperOnRoll(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LinealPaperOnRoll());
        }
        private void NaturalFrequency(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NaturalFrequency());
        }
        private void PaperCaliper(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaperCaliper());
        }
        private void PaperWebDraw(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaperWebDraw());
        }
        private void PipelineAndChannelVelocity(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PipelineAndChannelVelocity());
        }
        private void Power(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Power());
        }
        private void PressImpulse(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PressImpulse());
        }
        private void Retention(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Retention());
        }
        private void RimmingSpeed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RimmingSpeed());
        }
        private void RollRotationalSpeed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RollRotationalSpeed());
        }
        private void SizingAndCapacity(object sender, EventArgs e)
        {
            Navigation.PushAsync(new sizingAndCapacity());
        }
        private void StockThickness(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StockThickness());
        }
        private void TensionPower(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TensionPower());
        }
        private void TheoreticalHead(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TheoreticalHead());
        }
        private void TissueCrepe(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TissueCrepe());
        }
        private void TissueHeadboxFlowrate(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TissueHeadboxFlowRate());
        }
        private void Torque(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Torque());
        }
        private void VacuumComponentLineLoad(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VacuumComponentLineLoad());
        }
        private void WeirWaterFlows(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WeirWaterFlows());
        }
        private void WeirWaterFlowsV(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WeirWaterFlowsV());
        }
    }
}



