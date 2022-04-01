using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Syko.UnityToolbox.UI
{
  public class Pagination : MonoBehaviour
  {
    public delegate void PageClickEvent(int page);
    public event PageClickEvent onPageClicked;

    private GameObject pageButtonPrefab;
    private int pageCount = 0;
    private int activePage = 0;
    private Color defaultColor = Color.white * 0.6f;
    private Color activeColor = Color.white;

    public void SetPrefab(GameObject prefab)
    {
      pageButtonPrefab = prefab;
    }

    public void SetPageCount(int newPageCount)
    {
      int pagesToAdd = newPageCount - pageCount;
      int pagesToRemove = pageCount - newPageCount;
      for (int i = 0; i < pagesToRemove; i++)
      {
        transform.GetChild(transform.childCount - i - 1).GetComponent<Button>().onClick.RemoveAllListeners();
        Destroy(transform.GetChild(transform.childCount - i - 1).gameObject);
      }
      for (int i = 0; i < pagesToAdd; i++)
      {
        GameObject pageButton = Instantiate(pageButtonPrefab, transform);
        pageButton.GetComponentInChildren<TMP_Text>().color = defaultColor;
        int pageNum = transform.childCount;
        pageButton.transform.GetChild(0).GetComponent<TMP_Text>().text = pageNum.ToString();
        pageButton.GetComponent<Button>().onClick.AddListener(() => OnClickPage(pageNum));
      }
      pageCount = newPageCount;
    }

    private void OnClickPage(int page)
    {
      SetActivePage(page);
      onPageClicked(page);
    }

    public void SetActivePage(int page)
    {
      if (pageCount < 1) return;
      if (activePage > 0 && activePage <= pageCount) transform.GetChild(activePage - 1).GetComponentInChildren<TMP_Text>().color = defaultColor;
      activePage = page;
      if (activePage > 0 && activePage <= pageCount) transform.GetChild(activePage - 1).GetComponentInChildren<TMP_Text>().color = activeColor;
    }
  }
}
