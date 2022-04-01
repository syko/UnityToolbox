using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Syko.UnityToolbox.UI
{
  public abstract class PaginatedList<T> : MonoBehaviour
  {
    public GameObject listContainer;
    public Pagination pagination;
    public GameObject itemPrefab;
    public GameObject pageButtonPrefab;
    public Scrollbar horizontalScrollbar;
    public Scrollbar verticalScrollbar;
    public ScrollRect scrollContainer;
    public int itemsPerPage = 100;

    protected int currentPage = 1;
    protected List<T> items;

    void Awake()
    {
      pagination.SetPrefab(pageButtonPrefab);
      pagination.onPageClicked += onClickPage;
    }

    public void SetItems(List<T> items)
    {
      this.items = items;
      pagination.SetPageCount(Mathf.CeilToInt((float)items.Count / (float)itemsPerPage));
      RepopulateItems();
    }

    private void RepopulateItems()
    {
      DeleteAllItems();
      for (int i = (currentPage - 1) * itemsPerPage; i < currentPage * itemsPerPage; i++)
      {
        if (i >= items.Count) break;
        GameObject itemObject = Instantiate(itemPrefab, listContainer.transform);
        InitializeItem(i, itemObject);
        if (scrollContainer) itemObject.GetComponent<PaginatedListItem>().SetScrollContainer(scrollContainer);
      }
      if (horizontalScrollbar) horizontalScrollbar.value = 1;
      if (verticalScrollbar) verticalScrollbar.value = 1;
    }

    private void DeleteAllItems()
    {
      foreach (Transform child in listContainer.transform)
      {
        DeinitializeItem(child.gameObject);
        Destroy(child.gameObject);
      }
      // Detach all children immediately so that GetItemObjectByIndex will work properly without needing another frame
      listContainer.transform.DetachChildren();
    }

    public GameObject GetItemObjectByIndex(int index)
    {
      if (index < (currentPage - 1) * itemsPerPage || index >= currentPage * itemsPerPage) return null;
      return listContainer.transform.GetChild(index % itemsPerPage).gameObject;
    }

    public void onClickPage(int page)
    {
      if (page == currentPage) return;
      SetPage(page);
    }

    public void SetPage(int page)
    {
      currentPage = page;
      pagination.SetActivePage(page);
      RepopulateItems();
    }

    /// <summary>
    /// This method for initializing items for custom functionality
    /// </summary>
    /// <param name="index">The absolute index (not per-page) of the item</param>
    /// <param name="itemObject">The item GameObject</param>
    protected abstract void InitializeItem(int index, GameObject itemObject);
    protected abstract void DeinitializeItem(GameObject itemObject);

  }
}
