using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts
{
    public abstract class Building : PlayerObject
    {
        public Transform UnitSpawnPoint;
        public Transform RallyPoint;
        public LineRenderer LineRenderer;

        private void Awake() => RallyPoint = transform.GetChild(0).transform.GetChild(0);
        
        public override void Select()
        {
            base.Select();
            RallyPoint.gameObject.SetActive(true);
        }

        public override void DeSelect()
        {
            base.DeSelect();
            RallyPoint.gameObject.SetActive(false);
        }

        public override bool IsMultiSelect() => false;

        public void SetRallyPoint(Vector3 position)
        {
            RallyPoint.position = position;

            LineRenderer lineRenderer;

            if (!RallyPoint.gameObject.GetComponent<LineRenderer>())
                lineRenderer = RallyPoint.gameObject.AddComponent<LineRenderer>();
            
            else
                lineRenderer = RallyPoint.GetComponent<LineRenderer>();

            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.alignment = LineAlignment.View;


            // Draw line to the Rally Point
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, RallyPoint.position);
        }

        public override void Register()
        {
            base.Register();
            GameManager.Instance.BuildingHandler.RegisterBuilding(this);
        }
    }
}