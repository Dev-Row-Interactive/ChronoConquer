using DevRowInteractive.ChronoConquer.Source.Core;
using DevRowInteractive.SelectionManagement;
using DevRowInteractive.UnitProduction;

namespace DevRowInteractive.ChronoConquer.UI.Source
{
    public class PanelMovementOnProduction : PanelMovement
    {
        public override void Awake()
        {
            base.Awake();
            
            EventManager.OnSelectableSelected += MovePanelIn;
            EventManager.OnSelectableDeSelected += MovePanelOut;
        }

        public override void MovePanelIn(ISelectable selectable = null)
        {
            if (selectable.GetGameObjectReference().TryGetComponent<IProduction>(out var production))
                base.MovePanelIn();
        }
    }
}