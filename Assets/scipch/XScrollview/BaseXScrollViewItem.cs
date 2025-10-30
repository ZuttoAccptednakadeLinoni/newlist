using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace XScrollView
{
    public abstract class BaseXScrollViewItem : MonoBehaviour
    {
        public int index;
        private RectTransform rectTransform;
        private UnityAction<int> btnClickAction;
        [SerializeField]
        private Button button;
        public RectTransform RectTransform 
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = GetComponent<RectTransform>();
                return rectTransform;
            }
        }
        void Awake()
        {
            if (button == null)
            {
                button = GetComponent<Button>();
            }
            button.onClick.AddListener((() =>
            {
                print($"btnOnClick_{{index}}\"");
                btnClickAction?.Invoke(index);
            }));
            
        }

        private void OnDestroy()
        {
            btnClickAction = null;
        }
        public void AddButtonClickListener(UnityAction<int> btnClick)
        {
            btnClickAction += btnClick;
        }
        // 更新位置
        public void UpdateCellPos(Vector2 pos)
        {
            RectTransform.anchoredPosition = pos;
        }
        public virtual void UpdateCellInfo(int _index, IXScrollViewItemData data)
        {
            index = _index;
        }
    }
    
    
}

