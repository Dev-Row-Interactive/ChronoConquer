using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DevRowInteractive.EntityManagement
{
    public class CombatManager : MonoBehaviour
    {
        private List<ExampleUnit> allUnits = new List<ExampleUnit>();
        private List<ExampleUnit> aliveUnits = new List<ExampleUnit>();

        private void Start()
        {
            // Populate the units list by searching for ExampleUnit components in the scene
            allUnits = FindObjectsOfType<ExampleUnit>().ToList();
            aliveUnits.AddRange(allUnits);

            // Start the combat loop
            StartCoroutine(CombatLoop());
        }

        private void Update()
        {
            foreach (var unit in allUnits)
            {
                if (unit.IsDead())
                    aliveUnits.Remove(unit);
            }
        }

        private IEnumerator CombatLoop()
        {
            List<ExampleUnit> defeatedUnits = new List<ExampleUnit>(); // Create a separate list for defeated units

            while (aliveUnits.Count > 1)
            {
                // Each unit attacks a random unit

                for (int i = 0; i < aliveUnits.Count; i++)
                {
                    ExampleUnit attacker = aliveUnits[i];
                    ExampleUnit target = GetRandomAliveUnit(attacker);

                    if (target != null && !defeatedUnits.Contains(target))
                    {
                        // Move attacker to target
                        attacker.MakeMovement(target.transform.position);

                        yield return new WaitForSeconds(0.5f);
                        
                        while (!attacker.IsAtDestination())
                        {
                            yield return null;
                        }

                        // Start the loop to repeatedly deal damage with a 1-second delay
                        while (!target.IsDead())
                        {
                            // Wait for a second before attacking again
                            yield return new WaitForSeconds(1f);

                            // Attack the target
                            attacker.DealDamage(target);

                            yield return null;
                        }

                        Debug.Log(attacker.gameObject.name + " has defeated " + target.gameObject.name);
                        // Stop movement of the defeated unit
                        target.StopMovement();
                        // Deactivate the MeshRenderer component of the defeated unit
                        MeshRenderer renderer = target.GetComponent<MeshRenderer>();
                        if (renderer != null)
                        {
                            renderer.enabled = false;
                        }
                    }
                }
                yield return null;
            }
        }

        private ExampleUnit GetRandomAliveUnit(ExampleUnit exception)
        {
            List<ExampleUnit> available = new List<ExampleUnit>();
            available.AddRange(aliveUnits);
            available.Remove(exception);

            if (available.Count > 0)
            {
                // Choose a random alive unit
                int randomIndex = Random.Range(0, available.Count);
                return available[randomIndex];
            }

            return null;
        }
    }
}
