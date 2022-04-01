using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Syko.UnityToolbox.UI
{
  public class CursorChanger : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
  {
    [SerializeField] Sprite cursor;
    private Texture2D cursorTexture;
    private bool cursorSet = false;
    private Selectable selectableComponent;

    void Start()
    {
      cursorTexture = MakeTexture2DFromSprite(cursor);
      selectableComponent = GetComponent<Selectable>();
    }

    public void SetCursor(bool enable)
    {
      Cursor.SetCursor(enable ? cursorTexture : null, GetHotspotFromSprite(cursor), CursorMode.Auto);
      cursorSet = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (selectableComponent?.interactable != false) SetCursor(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      SetCursor(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (selectableComponent?.interactable == false) SetCursor(false);
    }

    void OnDisable()
    {
      if (cursorSet) SetCursor(false);
    }

    private Texture2D MakeTexture2DFromSprite(Sprite sprite)
    {
      Texture2D texture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);

#if UNITY_EDITOR
        texture2D.alphaIsTransparency = true;
#endif
      var pixels = sprite.texture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height);
      texture2D.SetPixels(pixels);
      texture2D.Apply();

      return texture2D;
    }

    private Vector2 GetHotspotFromSprite(Sprite sprite)
    {
      return new Vector2(sprite.pivot.x, cursor.rect.height - sprite.pivot.y);
    }
  }
}
