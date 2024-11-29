using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ListView : MonoBehaviour
{
    public UnityAction<ListViewItem> onItemSelected;
    public class ListViewItem : MonoBehaviour, IPointerClickHandler
    {
        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                onSelected(selected);
            }
        }
        public virtual void onSelected(bool selected)
        {
        }

        public ListView owner;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!this.selected)
            {
                this.Selected = true;
            }
            if (owner != null && owner.SelectedItem != this)
            {
                owner.SelectedItem = this;
            }
        }

        public virtual void SetItemInfo(object item)
        {

        }
    }

    public List<ListViewItem> items = new List<ListViewItem>();

    private ListViewItem selectedItem = null;
    public ListViewItem SelectedItem
    {
        get { return selectedItem; }
        set
        {
            if (selectedItem!=null && selectedItem != value)
            {
                selectedItem.Selected = false;
            }
            selectedItem = value;
            if (onItemSelected != null && selectedItem != null)
                onItemSelected.Invoke((ListViewItem)value);
        }
    }

    public void AddItem(ListViewItem item)
    {
        item.owner = this;
        this.items.Add(item);
    }

    public void AddItem<T>(GameObject itemPrefab, object item) where T :ListViewItem
    {
        GameObject go = Instantiate(itemPrefab, this.transform);
        T ui = go.GetComponent<T>();
        ui.SetItemInfo(item);
        this.AddItem(ui);
    }

    public void RemoveAll()
    {
        foreach(var it in items)
        {
            if(it != null)
                Destroy(it.gameObject);
        }
        items.Clear();
    }
}
