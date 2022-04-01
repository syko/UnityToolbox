using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Syko.UnityToolbox.UI
{
  public abstract class PaginatedListItem : MonoBehaviour, IScrollHandler
  {
    private ScrollRect scrollContainer; // Pass scroll events to this container if set

    public void SetScrollContainer(ScrollRect scrollContainer)
    {
      this.scrollContainer = scrollContainer;
    }

    public void OnScroll(PointerEventData pointerEventData)
    {
      if (scrollContainer != null) ExecuteEvents.Execute(scrollContainer.gameObject, pointerEventData, ExecuteEvents.scrollHandler);
    }
  }
}
