using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Intro
{
    public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        //public static event Action<int> OnButtonEvent;
        public Color32[] colors;
        public TMP_Text textBox;

        private void Start()
        {
            ChangeTextColour(0);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeTextColour(1);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeTextColour(0);
        }

        public void ChangeTextColour(int col)
        {
            textBox.color = colors[col];
        }
    }
}
