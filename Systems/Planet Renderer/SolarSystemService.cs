using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Spaceworks.Position;

namespace Spaceworks { 

    public class SolarSystemService : FloatingTransform {

        public SolarSystemConfig config;

        public SolarSystem generatedSystem { get; private set;}
        public Planet[] generatedPlanets { get; private set;}

        void Start(){
            SolarSystem s = new SolarSystem (config);
            generatedPlanets = s.RenderOn (this.gameObject);
            generatedSystem = s;

            foreach(Planet p in generatedPlanets) {
                p.ForceUpdateLODs(Camera.main.transform.position);
            }

            this.name = this.name + ": " + config.name;
        }

        void Update(){
            foreach (Planet p in generatedPlanets) {
                p.UpdateLODs(Camera.main.transform.position);
            }
        }

        public override void OnOriginChange(WorldPosition sceneCenter, WorldPosition delta = null)
        {
            base.OnOriginChange(sceneCenter);
            foreach (Planet p in generatedPlanets)
            {
                if (p != null)
                    p.UpdateMaterial(p.topFace.transform.gameObject);
            }
        }
    }
}