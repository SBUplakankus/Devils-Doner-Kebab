using UnityEngine;

namespace Game
{
    public class LightController : MonoBehaviour
    {
        public Light[] lights;
        public Color32[] colours;
    
        
        /// <summary>
        /// Change the light colours
        /// </summary>
        /// <param name="colourId">0 = Normal | 1 = Red</param>
        public void SetLightColour(int colourId)
        {
            foreach (var shopLight in lights)
            {
                shopLight.color = colours[colourId];
            }
        }
        
        /// <summary>
        /// Flicker a light
        /// </summary>
        /// <param name="lightId">Light to Flicker</param>
        public void FlickerLight(int lightId)
        {
            
        }
    }
}
