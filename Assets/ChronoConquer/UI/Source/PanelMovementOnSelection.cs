using DevRowInteractive.ChronoConquer.Source.Core;

namespace DevRowInteractive.ChronoConquer.UI.Source
{
    public class PanelMovementOnSelection : PanelMovement
    {
        public override void Awake()
        {
            base.Awake();
            EventManager.OnSelectableSelected += MovePanelIn;
            EventManager.OnSelectableDeSelected += MovePanelOut;
        }
    }
}